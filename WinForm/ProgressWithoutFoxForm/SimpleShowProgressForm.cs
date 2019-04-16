//
// NeoDEEX 예제 코드
//
// 이 코드는 NeoDEEX를 사용하는 예제 코드로서 제공되며
// 어떤 보증도 포함하고 있지 않습니다.
//
using System;
using System.Windows.Forms;
using TheOne.Windows.Forms;

namespace ProgressWithoutFoxForm
{
    //
    // FoxForm에서 파생되지 않은 Form에서 FoxAsyncProxy 혹은 FoxProgressScope 클래스를 사용하여
    // 프로그래스를 표시하는 방법을 보여주는 예제 입니다.
    // Form (혹은 Control에서 파생된 임의의 클래스도 가능) 클래스가 IFoxSupportProgress 인터페이스를 구현하면
    // FoxAsyncProxy/FoxProgressScope 객체를 사용할 때 프로그래스가 나타나도록 할 수 있습니다.
    //
    // 표시되는 프로그래스는 IFoxSupportProgress.GetProgressDialog 메서드가 반환하는 IFoxProgressDialog 객체가
    // 어떤 구현을 하고 있는가에 따라 달라집니다. Fox WinForm에서는 FoxProgressDialog 클래스를 제공하며
    // 이 클래스를 통해 NeoDEEX의 기본 프로그래스를 표시할 수 있습니다.
    // 다른 형태의 프로그래스를 표시하기 위해서는 IFoxProgresDialog를 구현하는 객체를 직접 작성해야 합니다.
    // 커스텀 프로그래스 구현 내용은 이 예제에 포함되어 있지 않습니다.
    //
    public partial class SimpleShowProgressForm : Form, IFoxSupportProgress
    {
        public SimpleShowProgressForm()
        {
            InitializeComponent();
        }

        #region IFoxSupportProgress 인터페이스 구현

        FoxProgressDialog _progressDialog;

        // FoxAsyncProxy(혹은 FoxTaskProxy)가 프로그래스를 표시해야 할 때
        // IFoxSupportProgress.GetProgressDialog 메서드가 호출됩니다.
        // 이 메서드는 IFoxProgressDialog 인터페이스를 구현하는 객체를 반환하면 됩니다.
        public IFoxProgressDialog GetProgressDialog()
        {
            if (_progressDialog == null)
            {
                // FoxProgressDialog 객체를 사용하여 프로그래스를 표시할 수 있습니다.
                // FoxProgressDialog는 UserControl로써 Form의 자식으로 추가되어야 합니다.
                _progressDialog = new FoxProgressDialog();
                this.Controls.Add(_progressDialog);
            }
            return _progressDialog;
        }

        #endregion

        // FoxAsyncProxy를 사용하여 비동기 작업을 수행하고 그동안 프로그래스를 표시하고자
        // 하는 경우의 예제 입니다.
        private async void button1_Click(object sender, EventArgs e)
        {
            var proxy = new FoxAsyncProxy(this);
            await proxy.ExecuteAsync(() =>
            {
                // 비동기 작업(서비스 호출) 수행.
                // (간단한 예제이므로 Thread.Speep을 호출하여 2초 동안 지연하는 것으로 대신 합니다.)
                System.Threading.Thread.Sleep(2000);
            });
            label2.Text = DateTime.Now.ToString();
        }

        // FoxProgressScope를 사용하여 비동기 작업을 수행하고 그동안 프로그래스를 표시하고자
        // 하는 경우의 예제 입니다.
        private async void button2_Click(object sender, EventArgs e)
        {
            using (var scope = new FoxProgressScope(this))
            {
                // 비동기 작업(서비스 호출) 수행.
                // (간단한 예제이므로 Task.Delay를 호출하여 4초 동안 지연하는 것으로 대신 합니다.)
                await System.Threading.Tasks.Task.Delay(4000);
            }
            label3.Text = DateTime.Now.ToString();
        }
    }
}
