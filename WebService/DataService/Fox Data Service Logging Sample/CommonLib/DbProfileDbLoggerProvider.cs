using TheOne.Diagnostics;
using TheOne.Diagnostics.Loggers;

namespace CommonLib
{
    //
    // FoxDbLoggerProviderBase를 기반으로 로거 프로바이더 구현 (FoxDbLogger 스타일)
    // (logger 속성 파싱을 FoxDbLoggerProviderBase.SetProperties 메서드 호출을 통해 처리 가능하다)
    //
    public class DbProfileDbLoggerProvider : FoxDbLoggerProviderBase
    {
        #region 프로바이더 표준 생성자들
        //
        // FoxLoggerProviderBase에서 파생된 프로바이더들은 다음 세 생성자를 표준으로
        // 제공해야 한다.
        //
        public DbProfileDbLoggerProvider()
        {
        }

        public DbProfileDbLoggerProvider(string name)
            : base(name)
        {
        }

        public DbProfileDbLoggerProvider(string name, FoxLoggerPropertyCollection properties)
            : base(name, properties)
        {
        }

        #endregion

        // FoxLoggerProviderBase 구현
        protected override IFoxLog OnCreateLogger(string loggerName,
            FoxLoggerPropertyCollection properties)
        {
            var logger = new DbProfileDbLogger(loggerName);
            base.SetProperties(logger, properties);
            return logger;
        }
    }
}
