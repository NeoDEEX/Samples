using DevExpress.XtraBars.Docking2010.Views;
using DevExpress.XtraBars.Navigation;
using DevExpress.XtraSplashScreen;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TheOne.UIModel;
using TheOne.Windows.Forms;

namespace MenuApp
{
    public partial class MainForm : DevExpress.XtraEditors.XtraForm, IFoxMenuView
    {
        private FoxMenuViewModel _menuViewModel;
        // 자동으로 오픈할 메뉴 ID 목록
        private string[] _startupMenus = new string[] { "1010", "1020", "1031", "1032" };

        #region 생성자 및 초기화

        public MainForm()
        {
            InitializeComponent();

            _menuViewModel = new FoxMenuViewModel(this);
            _menuViewModel.DoMenuItemCreate += RenderMenuItem;
            _menuViewModel.DoViewOpen += OpenView;
            _menuViewModel.DoViewActivate += ActivateView;

            tabbedView1.UseLoadingIndicator = DevExpress.Utils.DefaultBoolean.False;
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            _menuViewModel.MenuManager.LoadMenu("MenuData.xml");
            _menuViewModel.Initialize();
        }

        #endregion

        #region IFoxMenuView 구현

        public FoxViewModel CurrentViewModel
        {
            get
            {
                // Floating Document를 고려하여 현재 뷰를 반환한다.
                FoxViewModel viewModel = null;
                // ActiveDocument 속성은 Floating Document의 활성 여부에 따라 null로 리셋되지 않기 때문에
                // ActiveFloatingDocument 속성을 먼저 고려해야 한다.
                BaseDocument doc = tabbedView1.ActiveFloatDocument;
                if (doc == null)
                {
                    doc = tabbedView1.ActiveDocument;
                }
                if (doc != null)
                {
                    var view = doc.Control as IFoxView;
                    viewModel = view?.ViewModel;
                }
                return viewModel;
            }
        }

        public IFoxView GetView(string id)
        {
            // Tab으로 열려있는 화면들에서 주어진 Id를 검색한다.
            foreach (var doc in tabbedView1.Documents)
            {
                var view = doc.Control as IFoxView;
                if (view != null && view.ViewModel.Id == id)
                {
                    return view;
                }
            }
            // Floating 상태로 열려있는 화면들에서 주어진 Id를 검색한다.
            foreach (var doc in tabbedView1.FloatDocuments)
            {
                var view = doc.Control as IFoxView;
                if (view != null && view.ViewModel.Id == id)
                {
                    return view;
                }
            }
            return null;
        }

        public int GetViewCount()
        {
            // Tab에 열려있는 뷰와 Floating 뷰를 모두 고려 해야 한다.
            return tabbedView1.Documents.Count + tabbedView1.FloatDocuments.Count;
        }

        #endregion

        #region 메뉴 선택 등 메뉴 액션 관련 구현

        // FoxMenuView.ViewOpen 이벤트 핸들러
        private void OpenView(object sender, FoxViewEventArgs e)
        {
            var form = (Form)e.ViewModel.View;
            form.MdiParent = this;

            var handle = SplashScreenManager.ShowOverlayForm(this);
            try
            {
                BaseDocument doc;
                if (e.ViewModel.MenuInfo.ExtraInfo == "Float")
                {
                    doc = tabbedView1.AddFloatDocument(form, new Point(0, 0));
                }
                else
                {
                    doc = tabbedView1.AddDocument(form);
                }
                doc.ImageOptions.Image = imageCollection1.Images["menu_item"];
                doc.Caption = e.ViewModel.MenuInfo.DisplayTitle;
                doc.Tag = e.ViewModel;

                form.Visible = true;
            }
            finally
            {
                SplashScreenManager.CloseOverlayForm(handle);
            }
        }

        // FoxMenuView.ViewActivate 이벤트 핸들러
        private void ActivateView(object sender, FoxViewEventArgs e)
        {
            var form = e.ViewModel.View as Form;
            documentManager1.View.ActivateDocument(form);
        }

        // 햄버거 일반 메뉴 선택 시 처리 핸들러
        private void AccordionControl1_ElementClick(object sender, ElementClickEventArgs e)
        {
            var menuInfo = e.Element.Tag as FoxMenuItem;
            if (menuInfo != null)
            {
                _menuViewModel.SelectMenu(menuInfo);
            }
        }

        #endregion

        #region 메뉴 렌더링

        private AccordionControlElement CreateAccordianControlElement(AccordionControlElementCollection elements, FoxMenuItem menuInfo)
        {
            var element = new AccordionControlElement();
            element.Text = menuInfo.DisplayTitle;
            element.Tag = menuInfo;
            if (menuInfo.HasViewModel)
            {
                element.Style = ElementStyle.Item;
                element.Image = imageCollection1.Images["menu_item"];
            }
            else
            {
                element.Style = ElementStyle.Group;
                element.Image = imageCollection1.Images["menu_group"];
            }
            elements.Add(element);
            if (menuInfo.MenuItems.Count > 0)
            {
                element.Expanded = true;
            }
            return element;
        }

        private void AddChildMenuItem(AccordionControlElement parentElement, FoxMenuItem parentMenuInfo)
        {
            foreach (var menuInfo in parentMenuInfo.MenuItems)
            {
                var element = CreateAccordianControlElement(parentElement.Elements, menuInfo);
                AddChildMenuItem(element, menuInfo);
            }
        }

        private void RenderMenuItem(object sender, FoxMenuItemEventArgs e)
        {
            var menuInfo = e.MenuInfo;
            var element = CreateAccordianControlElement(accordionControl1.Elements, menuInfo);
            AddChildMenuItem(element, e.MenuInfo);
        }

        #endregion

        #region 대량 메뉴 오픈 테스트를 위한 메뉴 핸들러

        // SelectMenu API를 순차적으로 호출하여 여러 개의 메뉴를 Open 한다.
        // 각 메뉴를 로드하는데 필요한 시간만큼 시간이 소요된다.
        private void AccordionControlElement2_Click(object sender, EventArgs e)
        {
            foreach (var menuId in _startupMenus)
            {
                _menuViewModel.SelectMenu(menuId);
            }
            ActivateFirstMenu();
        }

        // DevExpress의 DocumentManager가 제공하는 Deferred Load 기능을 사용하여 여러 개의 메뉴를 Open 한다.
        // DocumentManager의 Document 객체만 추가하고 실제 화면은 해당 탭이 최초로 Activate 될 때 (혹은 Floating 시)
        // NeoDEEX 4.5.4에 새로이 추가된 CreateViewModel API를 사용하여 화면을 로드 한다.
        private void AccordionControlElement3_Click(object sender, EventArgs e)
        {
            var menuList = new List<string>();
            foreach (var menuId in _startupMenus)
            {
                var view = GetView(menuId);
                // GetView를 통해 열려 있는 뷰를 찾는 것만으로는 중복으로 화면이 열릴 수 있다.
                // Deferred Load에 의해 아직 뷰가 생성되지 않은 Docuemnt 들도 검사를 해야만 한다.
                if (view == null && !documentManager1.View.Documents.Exists(doc => ((doc.Tag as FoxMenuItem)?.Id == menuId)))
                {
                    var menuInfo = _menuViewModel.MenuManager.GetMenuItem(menuId);
                    if (menuInfo != null)
                    {
                        BaseDocument doc;
                        // Control을 포함하지 않는 AddDocument 호출을 통해 Deferred Load를 활성화 한다.
                        // 해당 Document가 활성화(혹은 Floating)될 때 QueryControl 이벤트가 발생하며
                        // 이 때 CreateViewModel API를 호출하여 뷰를 생성하고 표시하게 된다.
                        doc = tabbedView1.AddDocument(menuInfo.DisplayTitle, menuId);
                        doc.ImageOptions.Image = imageCollection1.Images["menu_item"];
                        doc.Tag = menuInfo;
                        if (menuInfo.ExtraInfo == "Float")
                        {
                            tabbedView1.Controller.Float(doc);
                        }
                    }
                }
            }
            ActivateFirstMenu();
        }

        // 메뉴 목록 중 첫번째 메뉴를 활성화 한다.
        private void ActivateFirstMenu()
        {
            var view = GetView(_startupMenus[0]);
            tabbedView1.ActivateDocument(view as Control);
        }

        // 아직 로드 되지 않은 뷰를 가진 문서 포함하여 열려있는 모든 문서를 닫는다.
        private void AccordionControlElement4_Click(object sender, EventArgs e)
        {
            documentManager1.View.Controller.CloseAll();
        }

        #endregion

        #region 지연 로드(Deferred Load) 구현

        IOverlaySplashScreenHandle _overlayHandle;
        private void TabbedView1_QueryControl(object sender, DevExpress.XtraBars.Docking2010.Views.QueryControlEventArgs e)
        {
            var menuInfo = e.Document.Tag as FoxMenuItem;
            if (menuInfo != null)
            {
                try
                {
                    var viewModel = _menuViewModel.CreateViewModel(menuInfo, null, true, false);
                    e.Control = viewModel.View as Control;
                    _overlayHandle = SplashScreenManager.ShowOverlayForm(this);
                }
                catch (Exception ex)
                {
                    var errorForm = new DeferredLoadErrorForm();
                    errorForm.ErrorTitle = $"Cannot load the specified menu, [{menuInfo.DisplayTitle}] [id={menuInfo.Id}]";
                    errorForm.ErrorMessage = ex.Message;
                    errorForm.ErrorDetail = ex.StackTrace;
                    e.Control = errorForm;
                }
            }
        }

        private void TabbedView1_ControlShown(object sender, DevExpress.XtraBars.Docking2010.Views.DeferredControlLoadEventArgs e)
        {
            if (_overlayHandle != null)
            {
                SplashScreenManager.CloseOverlayForm(_overlayHandle);
                _overlayHandle = null;
            }
        }

        #endregion
    }
}
