using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheOne.Diagnostics;

namespace CommonLib
{
    public class PerformanceInfoDbLoggerProvider : FoxLoggerProviderBase
    {
        // Configuration을 통해 지정되는 프로퍼티 이름 상수들
        private const string ConnectionStringNamePropertyName = "ConnectionStringName";
        private const string ActivityQueryIdPropertyName = "ActivityQueryId";
        private const string ContextQueryIdPropertyName = "ContextQueryId";
        private const string DebugPropertyName = "Debug";

        #region 프로바이더 표준 생성자들

        //
        // FoxLoggerProviderBase에서 파생된 프로바이더들은 다음 세 생성자를 표준으로
        // 제공해야 한다.
        //
        public PerformanceInfoDbLoggerProvider()
        {
        }

        public PerformanceInfoDbLoggerProvider(string name)
            : base(name)
        {
        }

        public PerformanceInfoDbLoggerProvider(string name, FoxLoggerPropertyCollection properties)
            : base(name, properties)
        {
        }

        #endregion

        // FoxLoggerProviderBase 구현
        protected override IFoxLog OnCreateLogger(string loggerName,
            FoxLoggerPropertyCollection properties)
        {
            var logger = new PerformanceInfoDbLogger(loggerName);
            StringComparison cmp = StringComparison.OrdinalIgnoreCase;

            foreach (var name in properties)
            {
                var value = properties[name];

                if (ConnectionStringNamePropertyName.Equals(name, cmp))
                {
                    // 연결 문자열 이름 설정
                    logger.ConnectionStringName = value;
                }
                else if (ActivityQueryIdPropertyName.Equals(name, cmp))
                {
                    // FoxPerformanceActivityInfo 기록을 위한 Fox Query Id
                    logger.ActivityQueryId = value;
                }
                else if (ContextQueryIdPropertyName.Equals(name, cmp))
                {
                    // FoxPerformanceContextInfo 기록을 위한 Fox Query Id
                    logger.ContextQueryId = value;
                }
                else if (DebugPropertyName.Equals(name, cmp))
                {
                    // PerformanceInfoDbLogger 디버깅(예외 발생 여부 등) 플래그
                    if (Enum.TryParse(value, out bool debug) == false)
                    {
                        FoxLoggerProviderBase.ThrowPropertyValueException(logger, name, value);
                    }
                    logger.Debug = debug;
                }
                else
                {
                    FoxLoggerProviderBase.ThrowPropertyNameException(logger, name);
                }
            }

            return logger;
        }
    }
}
