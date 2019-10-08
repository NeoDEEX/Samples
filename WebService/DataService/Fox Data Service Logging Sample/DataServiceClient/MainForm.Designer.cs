namespace DataServiceClient
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.docmgrMain = new DevExpress.XtraBars.Docking2010.DocumentManager(this.components);
            this.imgcMain = new DevExpress.Utils.ImageCollection(this.components);
            this.tabbedView1 = new DevExpress.XtraBars.Docking2010.Views.Tabbed.TabbedView(this.components);
            this.toolbarFormControl1 = new DevExpress.XtraBars.ToolbarForm.ToolbarFormControl();
            this.barmgrMain = new DevExpress.XtraBars.ToolbarForm.ToolbarFormManager(this.components);
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.btnAccessDataService = new DevExpress.XtraBars.BarButtonItem();
            this.btnViewPerformanceLog = new DevExpress.XtraBars.BarButtonItem();
            this.btnViewDbProfile = new DevExpress.XtraBars.BarButtonItem();
            this.btnAccessBizService = new DevExpress.XtraBars.BarButtonItem();
            ((System.ComponentModel.ISupportInitialize)(this.docmgrMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgcMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabbedView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.toolbarFormControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barmgrMain)).BeginInit();
            this.SuspendLayout();
            // 
            // docmgrMain
            // 
            this.docmgrMain.Images = this.imgcMain;
            this.docmgrMain.MdiParent = this;
            this.docmgrMain.View = this.tabbedView1;
            this.docmgrMain.ViewCollection.AddRange(new DevExpress.XtraBars.Docking2010.Views.BaseView[] {
            this.tabbedView1});
            // 
            // imgcMain
            // 
            this.imgcMain.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imgcMain.ImageStream")));
            this.imgcMain.Images.SetKeyName(0, "AccessDataService");
            this.imgcMain.Images.SetKeyName(1, "AccessBizService");
            this.imgcMain.Images.SetKeyName(2, "ViewPerformanceLog");
            this.imgcMain.Images.SetKeyName(3, "ViewDbProfileLog");
            // 
            // toolbarFormControl1
            // 
            this.toolbarFormControl1.Location = new System.Drawing.Point(0, 0);
            this.toolbarFormControl1.Manager = this.barmgrMain;
            this.toolbarFormControl1.Name = "toolbarFormControl1";
            this.toolbarFormControl1.Size = new System.Drawing.Size(1024, 30);
            this.toolbarFormControl1.TabIndex = 2;
            this.toolbarFormControl1.TabStop = false;
            this.toolbarFormControl1.TitleItemLinks.Add(this.btnAccessDataService);
            this.toolbarFormControl1.TitleItemLinks.Add(this.btnAccessBizService);
            this.toolbarFormControl1.TitleItemLinks.Add(this.btnViewPerformanceLog);
            this.toolbarFormControl1.TitleItemLinks.Add(this.btnViewDbProfile);
            this.toolbarFormControl1.ToolbarForm = this;
            // 
            // barmgrMain
            // 
            this.barmgrMain.DockControls.Add(this.barDockControlTop);
            this.barmgrMain.DockControls.Add(this.barDockControlBottom);
            this.barmgrMain.DockControls.Add(this.barDockControlLeft);
            this.barmgrMain.DockControls.Add(this.barDockControlRight);
            this.barmgrMain.Form = this;
            this.barmgrMain.Images = this.imgcMain;
            this.barmgrMain.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.btnAccessDataService,
            this.btnViewPerformanceLog,
            this.btnViewDbProfile,
            this.btnAccessBizService});
            this.barmgrMain.MaxItemId = 4;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 30);
            this.barDockControlTop.Manager = this.barmgrMain;
            this.barDockControlTop.Size = new System.Drawing.Size(1024, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 580);
            this.barDockControlBottom.Manager = this.barmgrMain;
            this.barDockControlBottom.Size = new System.Drawing.Size(1024, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 30);
            this.barDockControlLeft.Manager = this.barmgrMain;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 550);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1024, 30);
            this.barDockControlRight.Manager = this.barmgrMain;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 550);
            // 
            // btnAccessDataService
            // 
            this.btnAccessDataService.Caption = "Access Data Service";
            this.btnAccessDataService.Id = 0;
            this.btnAccessDataService.ImageOptions.ImageIndex = 0;
            this.btnAccessDataService.Name = "btnAccessDataService";
            this.btnAccessDataService.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.btnAccessDataService.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BtnAccessDataService_ItemClick);
            // 
            // btnViewPerformanceLog
            // 
            this.btnViewPerformanceLog.Caption = "View Performance Log";
            this.btnViewPerformanceLog.Id = 1;
            this.btnViewPerformanceLog.ImageOptions.ImageIndex = 2;
            this.btnViewPerformanceLog.Name = "btnViewPerformanceLog";
            this.btnViewPerformanceLog.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.btnViewPerformanceLog.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BtnViewPerformanceLog_ItemClick);
            // 
            // btnViewDbProfile
            // 
            this.btnViewDbProfile.Caption = "View DB Profile log";
            this.btnViewDbProfile.Id = 2;
            this.btnViewDbProfile.ImageOptions.ImageIndex = 3;
            this.btnViewDbProfile.Name = "btnViewDbProfile";
            this.btnViewDbProfile.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.btnViewDbProfile.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BtnViewDbProfile_ItemClick);
            // 
            // btnAccessBizService
            // 
            this.btnAccessBizService.Caption = "Access Biz Service";
            this.btnAccessBizService.Id = 3;
            this.btnAccessBizService.ImageOptions.ImageIndex = 1;
            this.btnAccessBizService.Name = "btnAccessBizService";
            this.btnAccessBizService.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.btnAccessBizService.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BtnAccessBizService_ItemClick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1024, 580);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Controls.Add(this.toolbarFormControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MinimumSize = new System.Drawing.Size(1024, 560);
            this.Name = "MainForm";
            this.Text = "Fox Biz/Data Service 로깅 데모";
            this.ToolbarFormControl = this.toolbarFormControl1;
            ((System.ComponentModel.ISupportInitialize)(this.docmgrMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgcMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabbedView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.toolbarFormControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barmgrMain)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.Docking2010.DocumentManager docmgrMain;
        private DevExpress.XtraBars.Docking2010.Views.Tabbed.TabbedView tabbedView1;
        private DevExpress.XtraBars.ToolbarForm.ToolbarFormControl toolbarFormControl1;
        private DevExpress.XtraBars.ToolbarForm.ToolbarFormManager barmgrMain;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem btnAccessDataService;
        private DevExpress.XtraBars.BarButtonItem btnViewPerformanceLog;
        private DevExpress.XtraBars.BarButtonItem btnViewDbProfile;
        private DevExpress.Utils.ImageCollection imgcMain;
        private DevExpress.XtraBars.BarButtonItem btnAccessBizService;
    }
}

