using Microsoft.Extensions.Configuration;
using NeoDEEX.Data;

namespace commlib;

public static class Utils
{
    public static void SetupTest<T>()
    {
        // user secrets 에서 연결 문자열을 읽어 오도록 구성 설정 조정.
        var assembly = typeof(T).Assembly;
        var config = new ConfigurationBuilder().AddUserSecrets(assembly).Build();
        FoxDatabaseConfig.ExternalConfiguration = config;

        // 테스트를 위한 기존 데이터 삭제
        using FoxDbAccess dbAccess = FoxDbAccess.CreateDbAccess();
        dbAccess.DeleteTestData();
    }
}
