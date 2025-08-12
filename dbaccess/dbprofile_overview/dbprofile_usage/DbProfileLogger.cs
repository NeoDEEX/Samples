using NeoDEEX.Data;
using NeoDEEX.Data.Diagnostics;
using NeoDEEX.Data.NpgsqlClient;
using NeoDEEX.Diagnostics;
using NpgsqlTypes;
using System.Transactions;

namespace dbprofile_usage;

public class DbProfileLogger(string name) : FoxLoggerBase(name)
{
    public string ConnectionStringName { get; set; } = "LoggingDb";

    protected override void WriteLog(string source, FoxLogLevel level, object? data)
    {
        if (data is not FoxDbProfileInfo info)
        {
            throw new ArgumentException("Log entry data is not of type FoxDbProfileInfo.", nameof(data));
        }
        //
        // Npgsql 전용 코드
        // 이식성은 떨어지지만 Jsonb 타입을 명시적으로 설정할 수 있어서 성능상 더 우수합니다.
        //
        FoxNpgsqlDbAccess dbAccess = FoxDbAccess.CreateDbAccess<FoxNpgsqlDbAccess>(this.ConnectionStringName);
        // 로그를 발생시킨 코드가 암시적 환경 트랜잭션(ambient transaction)을 사용할 수도 있습니다.
        // 어떠한 이유로 이 트랜잭션이 롤백되면 로그 기록도 같이 롤백될 수 있기 때문에 Supress 옵션을 사용하여
        // 현재 트랜잭션(Transaction.Current)에 포함되지 않도록 합니다.
        using TransactionScope scope = new(TransactionScopeOption.Suppress);
        // 쿼리 및 매개변수 설정
        string query = "INSERT INTO dbProfile VALUES (:p0, :p1, :p2)";
        FoxNpgsqlParameterCollection parameters = dbAccess.CreateNpgsqlParamCollection();
        parameters.AddWithValue("p0", NpgsqlDbType.Varchar, info.InfoId);
        parameters.AddWithValue("p1", NpgsqlDbType.Timestamp, info.Timestamp.ToLocalTime());
        // Jsonb 타입을 명시하지 않는 경우 형변환 오류가 발생할 수 있습니다.
        string json = info.ToJson();
        parameters.AddWithValue("p2", NpgsqlDbType.Jsonb, json);
        // SQL 명령 실행
        dbAccess.ExecuteSqlNonQuery(query, parameters);

        //
        // DB 독립적인 코드
        // PostgreSQL 의 jsonb 타입을 코드에서 명시할 수 없기 때문에 SQL 문장에서 형변환(::jsonb)을 해야 합니다.
        // 다수의 로그 항목에 대해 매번 형변환이 수행되면 DB 에 부담을 줄 수 있으므로 권장하지 않습니다.
        //
        /*
        FoxDbAccess dbAccess = FoxDbAccess.CreateDbAccess("LoggingDb");
        using TransactionScope scope = new(TransactionScopeOption.Suppress);
        string query = "INSERT INTO dbProfile VALUES (:p0, :p1, :p2::jsonb)";
        FoxDbParameterCollection parameters = dbAccess.CreateParamCollection();
        parameters.AddWithValue("p0", info.InfoId);
        parameters.AddWithValue("p1", info.Timestamp.ToLocalTime());
        string json = info.ToJson();
        parameters.AddWithValue("p2", json);
        dbAccess.ExecuteSqlNonQuery(query, parameters);
        */
    }

    protected override void WriteLogMessage(string source, FoxLogLevel level, string? message)
    {
        // 이 로거는 문자열을 출력하지 않기 때문에 이 메서드가 호출될 일은 없다.
        throw new InvalidOperationException();
    }
}
