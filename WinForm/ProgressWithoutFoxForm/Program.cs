//
// NeoDEEX 예제 코드
//
// 이 코드는 NeoDEEX를 사용하는 예제 코드로서 제공되며
// 어떤 보증도 포함하고 있지 않습니다.
//
using System;
using System.Windows.Forms;

namespace ProgressWithoutFoxForm
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new SimpleShowProgressForm());
        }
    }
}
