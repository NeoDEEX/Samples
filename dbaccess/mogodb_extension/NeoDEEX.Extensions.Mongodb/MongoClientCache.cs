using MongoDB.Driver;
using System.Collections.Concurrent;

namespace NeoDEEX.Extensions.Mongodb;

/// <summary>
/// MongoClient 객체에 대한 캐시를 제공합니다
/// </summary>
public sealed class MongoClientCache
{
    private readonly ConcurrentDictionary<string, MongoClient> _clientCache = new();

    internal static MongoClientCache Instance { get; } = new MongoClientCache();

    internal MongoClientCache()
    {
    }

    /// <summary>
    /// 지정된 연결 문자열에 대한 <see cref="MongoClient"/> 인스턴스를 반환합니다.
    /// </summary>
    /// <param name="connectionString">MongoDB 서버에 연결하는 데 사용되는 연결 문자열입니다.</param>
    /// <returns>
    /// 지정된 연결 문자열에 연결된 <see cref="MongoClient"/> 인스턴스를 반환합니다.
    /// </returns>
    public MongoClient GetClient(string connectionString)
    {
        MongoClient client = _clientCache.GetOrAdd(connectionString, new MongoClient(connectionString));
        return client;
    }

    /// <summary>
    /// 지정된 연결 문자열에 대한 <see cref="MongoClient"/> 인스턴스를 캐시에서 반환합니다.
    /// </summary>
    /// <param name="connectionString">MongoDB 서버에 연결하는 데 사용되는 연결 문자열입니다.</param>
    /// <returns>
    /// 지정된 연결 문자열에 연결된 <see cref="MongoClient"/> 인스턴스를 반환합니다.
    /// 해당 연결 문자열에 대한 클라이언트가 이미 캐시되어 있으면 캐시된 인스턴스를 반환하고,
    /// 그렇지 않으면 새 인스턴스를 생성하여 캐시에 저장한 후 반환합니다.
    /// </returns>
    public static MongoClient GetClientFromCache(string connectionString) => Instance.GetClient(connectionString);
}
