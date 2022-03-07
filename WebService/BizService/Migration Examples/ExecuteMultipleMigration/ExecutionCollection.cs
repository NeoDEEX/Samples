using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheOne.ServiceModel.Biz;

namespace ExecuteMultipleMigration
{
    public class ExecutionCollection
    {
        public FoxBizRequestCollection Requests { get; private set; } = new FoxBizRequestCollection();
        public List<Action<DataSet>> ResultActions { get; private set; } = new List<Action<DataSet>>();

        public void AddExecute(string methodId, Action<DataSet> action, params object[] args)
        {
            var request = new FoxBizRequest(String.Empty, methodId);
            request.MethodId = methodId;
            if (args == null || args.Length == 0)
            {
                for (int i = 0; i < args.Length; i++)
                {
                    request.Parameters.Add("p" + i, args[i]);
                }
            }
            this.Requests.Add(request);
            this.ResultActions.Add(action);
        }
    }
}
