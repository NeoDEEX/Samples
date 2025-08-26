using MongoDB.Bson;
using NeoDEEX.Data.Diagnostics;
using NeoDEEX.Security;

namespace NeoDEEX.Extensions.Mongodb;

/// <summary>
/// MongoDB 와 관련된 헬퍼 메서들을 제공하는 정적 클래스 입니다. 
/// </summary>
public static class FoxMongoDbExtensions
{
    /// <summary>
    /// FoxDbProfileInfo 객체를 BsonDocument 객체로 변환하여 반환합니다.
    /// </summary>
    /// <param name="info">FoxDbProfileInfo 객체</param>
    /// <returns>변환된 BsonDocument 객체</returns>
    public static BsonDocument ToBsonDocument(this FoxDbProfileInfo info)
    {
        ArgumentNullException.ThrowIfNull(info, nameof(info));
        // DbExceptionInfo 객체가 null이 아닐 경우에만 변환
        BsonValue exceptionInfoValue = BsonNull.Value;
        if (info.DbExceptionInfo != null)
        {
            exceptionInfoValue = new BsonDocument
            {
                ["errorCode"] = info.DbExceptionInfo.ErrorCode,
                ["message"] = info.DbExceptionInfo.Message != null ? info.DbExceptionInfo.Message : BsonNull.Value,
                ["exceptionType"] = info.DbExceptionInfo.ExceptionType != null ? info.DbExceptionInfo.ExceptionType : BsonNull.Value
            };
        }
        // ParameterInfos 딕셔너리가 null이 아닐 경우에만 변환
        BsonValue parameterInfosValue = BsonNull.Value;
        if (info.ParameterInfos != null)
        {
            parameterInfosValue = new BsonDocument();
            foreach (var pair in info.ParameterInfos)
            {
                parameterInfosValue[pair.Key] = pair.Value != null ? pair.Value : BsonNull.Value;
            }
        }
        // FoxDbProfileInfo 객체의 각 속성을 BsonDocument에 매핑
        // 주의: BsonDocument 는 단순한 null 을 허용하지 않고 BsonNull.Value 를 사용해야 한다.
        //      ADO.NET 에서 DBNull.Value 을 사용하는 것과 비슷하다. 
        BsonDocument doc = new()
        {
            ["infoId"] = info.InfoId,
            ["timestamp"] = info.Timestamp,
            ["executionType"] = info.ExecutionType,
            ["commandText"] = info.CommandText != null ? info.CommandText : BsonNull.Value,
            ["parametersString"] = info.ParametersString != null ? info.ParametersString : BsonNull.Value,
            ["resultString"] = info.ResultString != null ? info.ResultString : BsonNull.Value,
            ["executionTime"] = info.ExecutionTime,
            ["queryString"] = info.QueryString != null ? info.QueryString : BsonNull.Value,
            ["userInfo"] = info.UserInfo != null ? FoxUserInfoContext.VersionIndependentSerialize(info.UserInfo) : BsonNull.Value,
            ["userId"] = info.UserId != null ? info.UserId : BsonNull.Value,
            ["callerName"] = info.CallerName != null ? info.CallerName : BsonNull.Value,
            ["queryId"] = info.QueryId != null ? info.QueryId : BsonNull.Value,
            ["queryFileName"] = info.QueryFileName != null ? info.QueryFileName : BsonNull.Value,
            ["dbExceptionInfo"] = exceptionInfoValue,
            ["parameterInfos"] = parameterInfosValue
        };
        return doc;
    }

    /// <summary>
    /// FoxDbProfileInfo JSON 을 담고 있는 BsonDocument 객체를 FoxDbProfileInfo 객체로 변환합니다.
    /// 주의) Bson 직렬화와 JSON 역직렬화를 수행하므로 오버헤드가 발생합니다.
    /// </summary>
    /// <param name="doc">BsonDocument 객체</param>
    /// <param name="removeIdField">FoxDbProfileInfo 객체 변환 시 오류를 유발할 수 있는 _id 필드를 BsonDocument 가 포함하는 경우 제거할 것인지 여부</param>
    /// <returns>FoxDbProfileInfo 객체</returns>
    public static FoxDbProfileInfo? ToFoxDbProfileInfo(this BsonDocument doc)
    {
        if (doc.Contains("_id"))
        {
            doc.Remove("_id");
        }
        doc["timestamp"] = doc["timestamp"].AsUniversalTime.ToString("o");
        return FoxDbProfileInfo.FromJson(doc.ToJson());
    }
}
