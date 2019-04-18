//
// Fox UI Model 예제 STEP - Bonus
//
// 다음 예제 코드는 NeoDEEX의 구현 예제 입니다.
// 이 코드는 있는 그대로 제공되며 어떤 보증도 포함하지 않습니다.
// 다음 코드를 무단으로 복사, 배포 할 수 없습니다.
//
using DevExpress.XtraBars;
using System;
using System.Diagnostics;
using System.Windows.Forms;
using TheOne.Security;
using TheOne.ServiceModel;
using TheOne.ServiceModel.Biz;
using TheOne.ServiceModel.Web;
using TheOne.UIModel;
using TheOne.Windows.Forms;

namespace MenuSampleApp
{
    public partial class MainForm : DevExpress.XtraBars.Ribbon.RibbonForm, IFoxMenuView
    {
        private FoxMenuViewModel _menuViewModel;

        public MainForm()
        {
            InitializeComponent();

            _menuViewModel = new FoxMenuViewModel(this);
            _menuViewModel.DoMenuItemCreate += CreateMenuItem;
            _menuViewModel.DoViewOpen += OpenView;
            _menuViewModel.DoViewActivate += ActivateView;
            _menuViewModel.DoViewClose += CloseView;
            _menuViewModel.DoMyMenuRender += CreateMyMenuItem;
            _menuViewModel.DoMenuItemCustom += MenuCustomAction;
            _menuViewModel.DoViewStatusChanged += ViewStatusChanged;
            _menuViewModel.ViewStatusFilter = FoxViewStatus.Open | FoxViewStatus.Close;

            var maxCountString = System.Configuration.ConfigurationManager.AppSettings["MaxView"];
            if (String.IsNullOrWhiteSpace(maxCountString) == false)
            {
                int maxCount = _menuViewModel.MaxViewCount;
                if (Int32.TryParse(maxCountString, out maxCount) == true)
                {
                    _menuViewModel.MaxViewCount = maxCount;
                }
            }
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

        private void CloseView(object sender, FoxViewEventArgs e)
        {
            var form = (FoxForm)e.ViewModel.View;
            form.Close();
        }

        private void CreateMenuItem(object sender, FoxMenuItemEventArgs e)
        {
            var menuInfo = e.MenuInfo;
            var node = treeView1.Nodes.Add(menuInfo.DisplayTitle);
            node.Tag = menuInfo;
            node.ImageKey = "folder";
            AddChildTreeNode(node, menuInfo);
            if (menuInfo.Expanded)
            {
                node.Expand();
            }
        }

        private void AddChildTreeNode(TreeNode parentNode, FoxMenuItem parentMenuInfo)
        {
            foreach (var menuInfo in parentMenuInfo.MenuItems)
            {
                var node = parentNode.Nodes.Add(menuInfo.DisplayTitle);
                node.Tag = menuInfo;
                if (menuInfo.HasItems == true)
                {
                    node.ImageKey = "folder";
                    AddChildTreeNode(node, menuInfo);
                }
                else
                {
                    node.ImageKey = "app";
                }
                if (menuInfo.Expanded)
                {
                    node.Expand();
                }
            }
        }

        private void MenuCustomAction(object sender, FoxMenuItemEventArgs e)
        {
            var url = e.MenuInfo.Url;
            Process.Start(url);
        }

        private async void ViewStatusChanged(object sender, FoxViewStatusEventArgs e)
        {
            try
            {
                using (var svc = new FoxRestBizClient("api/bizservice"))
                {
                    var parameters = new FoxServiceParameterCollection();
                    parameters["Action"] = e.Action;
                    parameters["MenuId"] = e.ViewModel.MenuId;
                    parameters["Timestamp"] = DateTime.Now;
                    parameters["UserId"] = FoxUserInfoContext.Current?.UserId;
                    await svc.ExecuteAsync("MenuService", "WriteMenuStatistics", parameters);
                }
            }
            catch
            {
                // 통계 정보 기록 시 발생하는 오류는 무시한다.
            }
        }

        #endregion

        #region 폼/컨트롤 이벤트 핸들러

        private void MainForm_Shown(object sender, EventArgs e)
        {
            _menuViewModel.MenuManager.LoadMenu("MenuData.xml");
            _menuViewModel.MyMenuManager.Location = "MyMenu.xml";
            _menuViewModel.MyMenuAutoSave = true;
            _menuViewModel.Initialize();
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            var menuInfo = e.Node?.Tag as FoxMenuItem;
            if (menuInfo != null)
            {
                _menuViewModel.SelectMenu(menuInfo);
            }
        }

        #endregion

        #region 내 메뉴 관련 기능

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var viewModel = _menuViewModel.CurrentViewModel;
            if (viewModel != null)
            {
                _menuViewModel.AddToMyMenu(null, viewModel.MenuInfo);
            }
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var viewModel = _menuViewModel.CurrentViewModel;
            if (viewModel != null)
            {
                var menuInfo = _menuViewModel.MyMenuManager.GetMenuItem(viewModel.MenuId);
                _menuViewModel.RemoveFromMyMenu(menuInfo);
            }
        }

        private void CreateMyMenuItem(object sender, FoxMenuItemEventArgs e)
        {
            barSubItem1.ItemLinks.Clear();
            foreach (var menuInfo in _menuViewModel.MyMenuManager.RootItems)
            {
                var item = new BarButtonItem();
                item.Caption = menuInfo.DisplayTitle;
                item.Tag = menuInfo;
                item.ItemClick += (s, arg) =>
                {
                    var mi = arg.Item.Tag as FoxMenuItem;
                    _menuViewModel.SelectMenu(mi);
                };
                barSubItem1.ItemLinks.Add(item);
            }
        }

        #endregion
    }
}
