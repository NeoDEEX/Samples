using MongoDB.Driver;
using NeoDEEX.Configuration;
using NeoDEEX.Data;
using NeoDEEX.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace NeoDEEX.Extensions.Mongodb;

/// <summary>
/// DbProfileLogger 에 대한 프로바이더 구현 입니다. 로거 속성을 파싱하여 DbProfileLogger 객체를 초기화 하여 로거로 등록합니다.
/// </summary>
public class DbProfileLoggerProvider : FoxLoggerProviderBase
{
    private readonly MongoClientCache _cache;

    public DbProfileLoggerProvider(string providerName, FoxLoggerPropertyCollection properties)
        : this(providerName, properties, MongoClientCache.Instance)
    {
    }

    // 테스트를 위해 cache 객체를 주입받을 수 있도록 내부 생성자 제공
    internal DbProfileLoggerProvider(string providerName, FoxLoggerPropertyCollection properties, MongoClientCache cache)
        : base(providerName, properties)
    {
        _cache = cache;
    }

    /// <summary>
    /// 로거 속성을 파싱하고 설정하여 DbProfileLogger 객체를 생성하여 반환합니다.
    /// </summary>
    /// <param name="loggerName">로거 이름</param>
    /// <param name="properties">로거 속성 컬렉션</param>
    /// <returns>DbProfileLogger 객체</returns>
    protected override IFoxLog OnCreateLogger(string loggerName, FoxLoggerPropertyCollection properties)
    {
        string? connectionString = null;
        string? databaseName = null;
        string? collectionName = "dbprofile";
        bool useUserSecret = false;
        bool diagnostics = false;
        int flushInterval = 2000; // msec

        var propertyHandlers = new Dictionary<string, Action<string>>(StringComparer.OrdinalIgnoreCase)
        {
            { "connectionstring", value => connectionString = value },
            { "database", value => databaseName = value },
            { "collection", value => collectionName = value },
            { "usersecrets", value => { if (!bool.TryParse(value, out useUserSecret)) ThrowPropertyValueException(loggerName, "usersecrets", value); } },
            { "diagnostics", value => { if (!bool.TryParse(value, out diagnostics)) ThrowPropertyValueException(loggerName, "diagnostics", value); } },
            { "flushinterval", value => { if (!int.TryParse(value, out flushInterval)) ThrowPropertyValueException(loggerName, "flushinterval", value); } }
        };

        foreach (var propertyName in properties)
        {
            if (propertyHandlers.TryGetValue(propertyName, out var handler))
            {
                handler(properties[propertyName]);
            }
            else
            {
                // 알 수 없는 속성 이름 오류 처리
                ThrowPropertyNameException(loggerName, propertyName);
            }
        }
        // user-secrets 을 사용하는 연결 문자열 처리.
        if (useUserSecret == true)
        {
            if (FoxDatabaseConfig.ExternalConfiguration == null
                || String.IsNullOrWhiteSpace(connectionString) == true)
            {
                throw new FoxInvalidConfigurationException("Cannot use user-secret for connection string.");
            }
            connectionString = FoxDatabaseConfig.ExternalConfiguration[connectionString];
        }
        // 빈 연결 문자열, 데이터베이스 이름, 컬렉션 이름 처리
        ThrowIfNullOrWhiteSpace(connectionString, "Connection string is not specified.");
        ThrowIfNullOrWhiteSpace(databaseName, "Database name is not specified.");
        ThrowIfNullOrWhiteSpace(collectionName, "Collection name is not specified.");
        // MongoDB 컬렉션 연결 및 로거 생성
        var collection = GetCollection(connectionString, databaseName, collectionName);
        DbProfileLogger logger = new(loggerName, collection)
        {
            Diagnostics = diagnostics,
            FlushInterval = flushInterval,
        };
        return logger;
    }

    // 주어진 연결 문자열을 사용하여 MongoDB 에 접속하고 주어진 database, collection 에 대한
    // IMongoCollection<DbProfile> 컬렉션 객체를 반환합니다.
    private IMongoCollection<DbProfileInfo> GetCollection(string connectionString, string databaseName, string collectionName)
    {
        MongoClient client = _cache.GetClient(connectionString);
        IMongoCollection<DbProfileInfo> collection = client.GetDatabase(databaseName)
            .GetCollection<DbProfileInfo>(collectionName);
        return collection;
    }

    // 주어진 문자열이 null, Empty 혹은 공백 문자열인 경우 예외를 발생합니다.
    private static void ThrowIfNullOrWhiteSpace([NotNull] string? value, string message)
    {
        if (String.IsNullOrWhiteSpace(value) == true)
        {
            throw new FoxInvalidConfigurationException(message);
        }
    }
}
