//
// Fox UI Model 예제 STEP #4
//
// 다음 예제 코드는 NeoDEEX의 구현 예제 입니다.
// 이 코드는 있는 그대로 제공되며 어떤 보증도 포함하지 않습니다.
// 다음 코드를 무단으로 복사, 배포 할 수 없습니다.
//
using System;
using System.Windows.Forms;
using TheOne.UIModel;
using TheOne.Windows.Forms;

namespace MenuSampleApp
{
    public partial class MainForm : Form, IFoxMenuView
    {
        private FoxMenuViewModel _menuViewModel;

        public MainForm()
        {
            InitializeComponent();

            _menuViewModel = new FoxMenuViewModel(this);
            _menuViewModel.DoMenuItemCreate += CreateMenuItem;
            _menuViewModel.DoViewOpen += OpenView;
            _menuViewModel.DoViewActivate += ActivateView;
        }

        #region IFoxMenuView 구현

        public FoxViewModel CurrentViewModel
        {
            get
            {
                var form = this.ActiveMdiChild as FoxForm;
                return form?.ViewModel;
            }
        }

        public IFoxView GetView(string id)
        {
            foreach (IFoxView view in this.MdiChildren)
            {
                if (view.ViewModel.Id == id)
                {
                    return view;
                }
            }
            return null;
        }

        public int GetViewCount()
        {
            return this.MdiChildren.Length;
        }
        #endregion

        #region FoxMenuViewModel 이벤트 구현

        private void OpenView(object sender, FoxViewEventArgs e)
        {
            var form = (FoxForm)e.ViewModel.View;
            form.Text = e.ViewModel.MenuInfo.DisplayTitle;
            form.MdiParent = this;
            form.Show();
        }

        private void ActivateView(object sender, FoxViewEventArgs e)
        {
            var form = e.ViewModel.View as FoxForm;
            if (form != null)
            {
                form.Activate();
            }
        }

        private void AddChildMenuItem(ToolStripMenuItem parentMenuItem, FoxMenuItem parentMenuInfo)
        {
            foreach (var menuInfo in parentMenuInfo.MenuItems)
            {
                var menuItem = new ToolStripMenuItem();
                menuItem.Text = menuInfo.DisplayTitle;
                menuItem.Tag = menuInfo;
                menuItem.Click += MenuItem_Click;
                parentMenuItem.DropDownItems.Add(menuItem);
                AddChildMenuItem(menuItem, menuInfo);
            }
        }

        private void CreateMenuItem(object sender, FoxMenuItemEventArgs e)
        {
            var topLevelMenuItem = new ToolStripMenuItem();
            topLevelMenuItem.Text = e.MenuInfo.DisplayTitle;
            topLevelMenuItem.Tag = e.MenuInfo;
            menuStrip1.Items.Add(topLevelMenuItem);
            AddChildMenuItem(topLevelMenuItem, e.MenuInfo);
        }

        #endregion

        #region 폼/컨트롤 이벤트 핸들러

        private void MainForm_Shown(object sender, EventArgs e)
        {
            _menuViewModel.MenuManager.LoadMenu("MenuData.xml");
            _menuViewModel.Initialize();
        }

        private void MenuItem_Click(object sender, EventArgs e)
        {
            var menuItem = (ToolStripMenuItem)sender;
            var menuInfo = menuItem?.Tag as FoxMenuItem;
            if (menuInfo != null)
            {
                _menuViewModel.SelectMenu(menuInfo);
            }
        }

        #endregion
    }
}
