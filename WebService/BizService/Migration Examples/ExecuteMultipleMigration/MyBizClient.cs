using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheOne.ServiceModel.Biz;
using TheOne.ServiceModel.Web;

namespace ExecuteMultipleMigration
{
    public class MyBizClient : IDisposable
    {
        public FoxRestBizClient Client { get; private set; }
        public string ClassId { get; set; }

        public MyBizClient(string classId)
        {
            this.Client = new FoxRestBizClient();
            this.Client.ServiceUrl = "http://localhost:12345/test/rest/bizservice";
            this.ClassId = classId;
        }

        void IDisposable.Dispose()
        {
            this.Client.Dispose();
        }

        // 각 Reqeust에 ClassId를 설정합니다.
        private void SetClassId(ExecutionCollection executions)
        {
            foreach (var request in executions.Requests)
            {
                request.ClassId = this.ClassId;
                // 이외에 필요한 작업이 있다면 수행합니다.
            }
        }

        // 각 호출의 수행 결과에 대해 Action을 수행합니다.
        private void ProcessResult(ExecutionCollection executions, IList<FoxBizResponse> responses)
        {
            for (int i = 0; i < responses.Count; i++)
            {
                var response = responses[i];
                executions.ResultActions[i]?.Invoke(response.DataSet);
            }
        }

        // ExecutionCollection을 사용하여 ExecuteMutiple을 수행합니다.
        public void ExecuteMultiple(ExecutionCollection executions)
        {
            SetClassId(executions);
            // ExeucteMultiple을 호출 합니다.
            var responses = this.Client.ExecuteMultiple(executions.Requests);
            ProcessResult(executions, responses);
        }

        // ExecutionCollection을 사용하여 ExecuteParallel을 수행합니다.
        public void ExecuteParallel(ExecutionCollection executions)
        {
            SetClassId(executions);
            var responses = this.Client.ExecuteParallel(executions.Requests);
            ProcessResult(executions, responses);
        }
    }
}
