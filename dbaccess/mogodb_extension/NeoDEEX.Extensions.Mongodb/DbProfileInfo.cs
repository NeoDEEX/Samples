using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using NeoDEEX.Data;
using NeoDEEX.Data.Diagnostics;
using NeoDEEX.Security;
using System.Text.Json.Serialization;

namespace NeoDEEX.Extensions.Mongodb;

//
// MongoDB 와 데이터를 읽고 기록하는데 사용하는 DTO 객체
//
// NOTE: FoxDbProfileInfo 객체를 mongodb 에 기록할 수 있지만 읽어온 데이터로
// FoxDbProfileInfo 로 역직렬화하는데 비효율적이므로 별도의 DTO 객체를 정의하여 사용한다.
// FoxDbProfileInfo 객체를 임의로 생성하고 속성에 기록할 수 있다면 IBsonSerializer 를 구현하여
// 직접 mongodb 와 인터페이스에 사용할 수 있다.
//

/// <summary>
/// MongoDB 와 데이터를 읽고 기록하는데 사용하는 DTO 객체 입니다.
/// </summary>
public class DbProfileInfo
{
    // MongoDB 문서를 위한 고유 식별자 필드 입니다.
    // System.Text.Json 직렬화에서는 제외 합니다.
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    [JsonIgnore]
    public string? Id { get; set; }

    [BsonElement("infoId")]
    [JsonPropertyName("infoId")]
    public string InfoId { get; set; } = string.Empty;

    [BsonElement("timestamp")]
    [JsonPropertyName("timestamp")]
    public DateTime Timestamp { get; set; }

    [BsonElement("executionType")]
    [JsonPropertyName("executionType")]
    public FoxDbExecutionType ExecutionType { get; set; }

    [BsonElement("commandText")]
    [JsonPropertyName("commandText")]
    public string? CommandText { get; set; }

    [BsonElement("parametersString")]
    [JsonPropertyName("parametersString")]
    public string? ParametersString { get; set; }

    [BsonElement("resultString")]
    [JsonPropertyName("resultString")]
    public string? ResultString { get; set; }

    [BsonElement("executionTime")]
    [JsonPropertyName("executionTime")]
    public double ExecutionTime { get; set; }

    [BsonElement("queryString")]
    [JsonPropertyName("queryString")]
    public string? QueryString { get; set; }

    [BsonElement("userInfo")]
    [JsonPropertyName("userInfo")]
    public FoxUserInfoContext? UserInfo { get; set; }

    [BsonElement("userId")]
    [JsonPropertyName("userId")]
    public string? UserId { get; set; }

    [BsonElement("callerName")]
    [JsonPropertyName("callerName")]
    public string? CallerName { get; set; }

    [BsonElement("queryId")]
    [JsonPropertyName("queryId")]
    public string? QueryId { get; set; }

    [BsonElement("queryFileName")]
    [JsonPropertyName("queryFileName")]
    public string? QueryFileName { get; set; }

    [BsonElement("dbExceptionInfo")]
    [JsonPropertyName("dbExceptionInfo")]
    public DbExceptionInfo? DbExceptionInfo { get; set; }

    [BsonElement("parameterInfos")]
    [JsonPropertyName("parameterInfos")]
    public Dictionary<string, string>? ParameterInfos { get; set; }

    public DbProfileInfo(FoxDbProfileInfo info)
    {
        ArgumentNullException.ThrowIfNull(info);
        this.InfoId = info.InfoId;
        this.Timestamp = info.Timestamp;
        this.ExecutionType = info.ExecutionType;
        this.CommandText = info.CommandText;
        this.ParametersString = info.ParametersString;
        this.ResultString = info.ResultString;
        this.ExecutionTime = info.ExecutionTime;
        this.QueryString = info.QueryString;
        this.UserInfo = info.UserInfo;
        this.UserId = info.UserId;
        this.CallerName = info.CallerName;
        this.QueryId = info.QueryId;
        this.QueryFileName = info.QueryFileName;
        this.ParameterInfos = info.ParameterInfos;
        if (info.DbExceptionInfo != null)
        {
            this.DbExceptionInfo = new DbExceptionInfo
            {
                ErrorCode = info.DbExceptionInfo.ErrorCode,
                Message = info.DbExceptionInfo.Message,
                ExceptionType = info.DbExceptionInfo.ExceptionType
            };
        }
    }

    public FoxDbProfileInfo ToFoxDbProfileInfo()
    {
        return this.ToBsonDocument().ToFoxDbProfileInfo()!;
    }
}

public class DbExceptionInfo
{
    [BsonElement("errorCode")]
    [JsonPropertyName("errorCode")]
    public int ErrorCode { get; set; }
    [BsonElement("message")]
    [JsonPropertyName("message")]
    public string? Message { get; set; }
    [BsonElement("exceptionType")]
    [JsonPropertyName("exceptionType")]
    public string? ExceptionType { get; set; }
}
