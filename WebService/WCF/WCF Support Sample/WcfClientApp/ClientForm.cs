using DevExpress.XtraEditors;
using InterfaceLib;
using System;
using System.ServiceModel;
using System.Windows.Forms;
using TheOne.Security;
using TheOne.ServiceModel;

namespace WcfClientApp
{
    public partial class ClientForm : XtraForm
    {
        public ClientForm()
        {
            InitializeComponent();

            // 실제 어플리케이션은 로그인 과정을 거쳐야 하지만 간단한 예제 이므로
            // 하드 코드를 사용하여 사용자 로그인을 에뮬레이션 한다.
            var ctx = new FoxUserInfoContext("TestUser");
            ctx["DeptId"] = "TestDept";
            ctx.SetCallContext();
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            // FoxClientFactory를 사용하여 WCF 서비스 호출
            using (var svc = FoxClientFactory.CreateChannel<IWcfService>("WcfService.svc"))
            {
                var ds = svc.GetAllProducts();
                grdProducts.DataSource = ds.Tables[0];
            }

            // 추가적인 WCF 서비스 호출
            using (var svc = FoxClientFactory.CreateChannel<IWcfService2>("WcfService2.svc"))
            {
                svc.DoWork();
            }
        }

        private void btnTest1_Click(object sender, EventArgs e)
        {
            // 다른 구성 설정(netHttp)으로 WCF 서비스 호출
            // WcfService1.svc는 .svc 파일을 사용하지 않고 web.config에서 구성된 서비스 예제 이다.
            // web.config 에서 FoxServiceHostFactory를 사용하는 경우에서 바인딩 맵 지정이 가능하다.
            using (var svc = FoxClientFactory.CreateChannel<IWcfService>("", "WcfService1.svc", "netHttp"))
            {
                var ds = svc.GetAllProducts();
                grdProducts.DataSource = ds.Tables[0];
            }

            // 다른 구성 설정(netHttp)으로 추가적인 WCF 서비스 호출
            // WcfService3.svc는 .svc 파일에서 바인딩 맵을 지정한 예제이다.
            using (var svc = FoxClientFactory.CreateChannel<IWcfService3>("", "WcfService3.svc", "netHttp"))
            {
                svc.DoWork();
            }
        }

        private void btnTest2_Click(object sender, EventArgs e)
        {
            // 사용자 정보 조회
            using (var svc = FoxClientFactory.CreateChannel<IWcfService4>("WcfService4.svc"))
            {
                var result = svc.EchoUserInfo();
                MessageBox.Show(this, result, "Result");
            }
        }

        private void BtnTest3_Click(object sender, EventArgs e)
        {
            // 주소 맵이나 바인딩 맵을 사용하지 않는 서비스 호출 코드 #1
            var url1 = "http://localhost:32900/wcfservice.svc";
            var binding = new BasicHttpBinding();
            using (var svc = FoxClientFactory.CreateChannel<IWcfService>(url1, binding))
            {
                var ds = svc.GetAllProducts();
                grdProducts.DataSource = ds.Tables[0];
            }

            // 주소 맵이나 바인딩 맵을 사용하지 않는 서비스 호출 코드 #2
            var url2 = "http://localhost:32900/login.svc";
            var ep = new EndpointAddress(url2);
            var bindingName = "defaultBinding";
            using (var svc = FoxClientFactory.CreateChannel<ILogin>(ep, bindingName, null))
            {
                if (svc.LoginUser("tester", "passport") == false)
                {
                    MessageBox.Show(this, "Login failuer", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BtnTest4_Click(object sender, EventArgs e)
        {
            // 메시지 압축 인코더를 사용하여 압축이 적용된 WCF 서비스 호출
            using (var svc = FoxClientFactory.CreateChannel<IWcfServiceWithCompress>(String.Empty, "WcfServiceWithCompress.svc", "customCompress"))
            {
                var ds = svc.GetAllProducts();
                grdProducts.DataSource = ds.Tables[0];
            }
        }
    }
}
