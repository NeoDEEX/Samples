using System;
using System.Threading;
using System.Threading.Tasks;
using TheOne.Security;
using TheOne.ServiceModel.Biz;

namespace ServiceWebApp.Biz
{
    //
    // 작업이 주어진 시간 이상 동작하지 않도록 하며, 트랜잭션을 사용하도록 하기 위해서는
    // Biz 컴포넌트를 직접 호출할 수 없다. (트랜잭션은 여러 스레드에 분산될 수 없음)
    // 따라서 POCO 비즈 서비스 클래스를 작성하여 클라이언트는 이 클래스를 호출한다.
    // POCO 비즈 메서드는 새로운 스레드를 시작하여 이 스레드에서
    // Biz 컴포넌트를 호출하고 이 스레드가 트랜잭션 하에서 수행되도록 하며
    // 스레드 종료를 타임아웃을 적용하여 기다린다.
    //
    [FoxBizClass]
    public class BatchFront
    {
        private static int MAX_TIMEOUT = 20 * 1000;

        [FoxBizMethod]
        public FoxBizResponse BatchLogic(FoxBizRequest request)
        {
            // 배치 타임아웃 시간을 추출해 낸다.
            int timeout = (int)request["timeout"];
            request.Parameters.Remove("timeout");

            // 주어진 타임아웃 후 취소가 되는 토큰 생성
            var cts = new CancellationTokenSource(timeout);
            // 스레드 의존적 데이터 캡처
            var userInfo = FoxUserInfoContext.Current;
            var bizContext = FoxBizServiceContext.Current;
            // 트랜잭션이 수행되도록 별도의 스레드에서 Biz 컴포넌트를 호출한다.
            var task = Task.Run(() =>
            {
                // 새로운 스레드에서 수행될 수 있으므로 UserInfoContext를 설정해 주어야 한다.
                using (FoxUserInfoContext.CreateScope(userInfo))
                {
                    using (var biz = new BizComp())
                    {
                        // 주) FoxBizServiceContext는 비즈 서비스에 대한 다양한 정보를 담고 있으며
                        //     이 객체 역시 스레드에 의존적이다. 아직 스레드 설정 API가 제공되지 않으므로
                        //     매개변수로 전달한다.
                        //     FoxBizServiceContext 클래스는 최신 버전의 NeoDEEX에서 제공되는 기능이므로
                        //     하위 버전을 사용하는 경우 이 매개변수를 제거해야 한다.
                        biz.DoBatchJob(request, cts.Token, bizContext);
                    }
                }
            });
            var response = new FoxBizResponse();
            try
            {
                // 작업 완료를 기다린다.
                // 주) Wait에서 최대 대기 시간을 사용하여 매개변수로 전달된 timeout 이 아주 긴
                //    시간을 갖더라도 수행을 중단하도록 한다.
                if (task.Wait(MAX_TIMEOUT) == false)
                {
                    // timeout 값이 MAX_TIMEOUT 보다 긴 상황이므로 명시적으로 취소를 수행한다.
                    cts.Cancel();
                    task.Wait(MAX_TIMEOUT);
                }
            }
            catch (AggregateException ex)
            {
                // 단순히 오류 메시지를 로그에 찍고 실패임을 반환한다.
                // 필요하다면 여기에서 예외를 throw 할 수도 있다.
                bizContext.WriteLog(TheOne.Diagnostics.FoxLogLevel.Error, ex.Flatten().InnerExceptions[0].Message);
                response.Success = false;
            }
            return response;
        }
    }
}