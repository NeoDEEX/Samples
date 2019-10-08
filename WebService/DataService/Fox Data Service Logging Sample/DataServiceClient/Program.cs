using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataServiceClient
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Init();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var mainForm = new MainForm();
            //var mainForm = new AccessDataServiceForm();
            Application.Run(mainForm);
        }

        static void Init()
        {
            // 전역 예외 핸들러 등록
            TheOne.Windows.Forms.FoxExceptionHandler.Register();

            // 데이터 클라이언트(RESTful) 팩터리 등록
            TheOne.ServiceModel.FoxClientFactory.DataClientFactory = new DataClientFactory();
            // 비즈 클라이언트(RESTful) 팩터리 등록
            TheOne.ServiceModel.FoxClientFactory.BizClientFactory = new BizClientFactory();
        }
    }
}
