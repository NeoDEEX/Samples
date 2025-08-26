using MongoDB.Driver;
using NeoDEEX.Data.Diagnostics;
using NeoDEEX.Diagnostics;
using System.Collections.Concurrent;

namespace NeoDEEX.Extensions.Mongodb;

/// <summary>
/// Fox DB Profile 을 MongoDB 에 기록하는 로거 입니다.
/// </summary>
/// <param name="name">로거 이름</param>
/// <param name="collection">Mongodb 컬렉션 객체</param>
public class DbProfileLogger(string name, IMongoCollection<DbProfileInfo> collection) : FoxLoggerBase(name), IDisposable
{
    private IMongoCollection<DbProfileInfo> Collection { get; } = collection;
    private ConcurrentQueue<DbProfileInfo> Queue { get; } = new ConcurrentQueue<DbProfileInfo>();
    private int _insertedItemCount = 0;
    private Timer? FlushTimer { get; set; }

    internal bool Diagnostics { get; set; }
    internal int FlushInterval { get; set; } = 2000; // msec

    /// <summary>
    /// 성공적으로 기록된 DB 프로파일 항목의 수를 반환합니다.
    /// </summary>
    public int InsertedCount => _insertedItemCount;
    /// <summary>
    /// 현재 기록을 기다리며 대기열에 있는 DB 프로파일 항목의 수를 반환합니다.
    /// </summary>
    public int QueuedCount => this.Queue.Count;

    protected override void WriteLog(string source, FoxLogLevel level, object? data)
    {
        if (data is not FoxDbProfileInfo info)
        {
            throw new ArgumentException("Data must be FoxDbProfileInfo type.", nameof(data));
        }
        // FoxDbProfileInfo 객체를 DTO 객체(DbProfileInfo) 로 변환
        DbProfileInfo dbProfile = new(info);
        // 진단 모드인 경우 동기적으로 곧바로 기록하고 그렇지 않은 경우에는
        // n초마다 수행되는 타이머에 의해 비동기적으로 기록한다.
        if (Diagnostics == true)
        {
            this.Collection.InsertOne(dbProfile);
        }
        else
        {
            this.Queue.Enqueue(dbProfile);
            EnsureTimer();
        }
    }

    private void EnsureTimer()
    {
        if (this.FlushTimer == null)
        {
            lock (this)
            {
                this.FlushTimer ??= new Timer(TimerCallback, null, FlushInterval, FlushInterval);
            }
        }
    }

    private void ResetTimer()
    {
        if (this.FlushTimer != null)
        {
            lock (this)
            {
                this.FlushTimer?.Change(FlushInterval, FlushInterval);
            }
        }
    }

    private void TimerCallback(object? state)
    {
        Flush();
    }

    private void Flush()
    {
        if (this.Queue.IsEmpty)
        {
            return;
        }
        List<DbProfileInfo> list = [];
        while (this.Queue.TryDequeue(out var info))
        {
            list.Add(info);
        }
        if (list.Count > 0)
        {
            this.Collection.InsertMany(list);
            Interlocked.Add(ref _insertedItemCount, list.Count);
        }
    }

    void IDisposable.Dispose()
    {
        this.FlushTimer?.Dispose();
        Flush(); // Ensure any remaining items in the queue are flushed before disposing.
        GC.SuppressFinalize(this);
    }

    public void FlushQueue()
    {
        Flush();
        if (this.Queue.IsEmpty == true)
        {
            ResetTimer();
        }
    }

    // 문자열 로깅은 사용하지 않으며 호출되지도 않음.
    protected override void WriteLogMessage(string source, FoxLogLevel level, string? message)
    {
        throw new NotSupportedException();
    }
}
