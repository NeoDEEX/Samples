namespace DataServiceClient
{
    partial class PerformanceLogViewerForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PerformanceLogViewerForm));
            this.btnQuery = new DevExpress.XtraEditors.SimpleButton();
            this.tlstContextInfo = new DevExpress.XtraTreeList.TreeList();
            this.colTimestamp = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colContextName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colInclusiveTime = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colExclusive = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.bsContextInfo = new System.Windows.Forms.BindingSource(this.components);
            this.grdActivityInfo = new DevExpress.XtraGrid.GridControl();
            this.grvActivityInfo = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcolTimestamp = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcolCategory = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcolActivityName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcolRootContxt = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcolElapsedTime = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcolMachine = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcolProcessId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.bsActivityInfo = new System.Windows.Forms.BindingSource(this.components);
            this.dateFrom = new DevExpress.XtraEditors.DateEdit();
            this.dateTo = new DevExpress.XtraEditors.DateEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.btnRecentHour = new System.Windows.Forms.LinkLabel();
            this.btnToday = new System.Windows.Forms.LinkLabel();
            this.btnDeleteAll = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.tlstContextInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsContextInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdActivityInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvActivityInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsActivityInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateFrom.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateFrom.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTo.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTo.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnQuery
            // 
            this.btnQuery.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnQuery.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnQuery.ImageOptions.SvgImage")));
            this.btnQuery.ImageOptions.SvgImageSize = new System.Drawing.Size(16, 16);
            this.btnQuery.Location = new System.Drawing.Point(745, 12);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(102, 23);
            this.btnQuery.TabIndex = 0;
            this.btnQuery.Text = "조회";
            this.btnQuery.Click += new System.EventHandler(this.BtnQuery_Click);
            // 
            // tlstContextInfo
            // 
            this.tlstContextInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tlstContextInfo.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.colTimestamp,
            this.colContextName,
            this.colInclusiveTime,
            this.colExclusive});
            this.tlstContextInfo.Cursor = System.Windows.Forms.Cursors.Default;
            this.tlstContextInfo.KeyFieldName = "ContextId";
            this.tlstContextInfo.Location = new System.Drawing.Point(12, 264);
            this.tlstContextInfo.Name = "tlstContextInfo";
            this.tlstContextInfo.OptionsBehavior.Editable = false;
            this.tlstContextInfo.OptionsBehavior.ReadOnly = true;
            this.tlstContextInfo.OptionsView.AutoWidth = false;
            this.tlstContextInfo.ParentFieldName = "ParentContextId";
            this.tlstContextInfo.Size = new System.Drawing.Size(835, 197);
            this.tlstContextInfo.TabIndex = 1;
            this.tlstContextInfo.NodeCellStyle += new DevExpress.XtraTreeList.GetCustomNodeCellStyleEventHandler(this.TlstContextInfo_NodeCellStyle);
            this.tlstContextInfo.DoubleClick += new System.EventHandler(this.TlstContextInfo_DoubleClick);
            this.tlstContextInfo.MouseMove += new System.Windows.Forms.MouseEventHandler(this.TlstContextInfo_MouseMove);
            // 
            // colTimestamp
            // 
            this.colTimestamp.Caption = "Timestamp";
            this.colTimestamp.FieldName = "LogTimestamp";
            this.colTimestamp.Format.FormatString = "yyyy-MM-dd hh:mm:ss.fff";
            this.colTimestamp.Format.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colTimestamp.Name = "colTimestamp";
            this.colTimestamp.Visible = true;
            this.colTimestamp.VisibleIndex = 0;
            this.colTimestamp.Width = 200;
            // 
            // colContextName
            // 
            this.colContextName.Caption = "Context Name";
            this.colContextName.FieldName = "ContextName";
            this.colContextName.Name = "colContextName";
            this.colContextName.Visible = true;
            this.colContextName.VisibleIndex = 1;
            this.colContextName.Width = 180;
            // 
            // colInclusiveTime
            // 
            this.colInclusiveTime.Caption = "Inclusive(ms)";
            this.colInclusiveTime.FieldName = "InclusiveTime";
            this.colInclusiveTime.Format.FormatString = "N2";
            this.colInclusiveTime.Format.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colInclusiveTime.Name = "colInclusiveTime";
            this.colInclusiveTime.Visible = true;
            this.colInclusiveTime.VisibleIndex = 2;
            this.colInclusiveTime.Width = 100;
            // 
            // colExclusive
            // 
            this.colExclusive.Caption = "Exclusive(ms)";
            this.colExclusive.FieldName = "ExclusiveTime";
            this.colExclusive.Format.FormatString = "N2";
            this.colExclusive.Format.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colExclusive.Name = "colExclusive";
            this.colExclusive.Visible = true;
            this.colExclusive.VisibleIndex = 3;
            this.colExclusive.Width = 100;
            // 
            // grdActivityInfo
            // 
            this.grdActivityInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdActivityInfo.Location = new System.Drawing.Point(12, 41);
            this.grdActivityInfo.MainView = this.grvActivityInfo;
            this.grdActivityInfo.Name = "grdActivityInfo";
            this.grdActivityInfo.ShowOnlyPredefinedDetails = true;
            this.grdActivityInfo.Size = new System.Drawing.Size(835, 217);
            this.grdActivityInfo.TabIndex = 2;
            this.grdActivityInfo.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvActivityInfo});
            // 
            // grvActivityInfo
            // 
            this.grvActivityInfo.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcolTimestamp,
            this.gcolCategory,
            this.gcolActivityName,
            this.gcolRootContxt,
            this.gcolElapsedTime,
            this.gcolMachine,
            this.gcolProcessId});
            this.grvActivityInfo.GridControl = this.grdActivityInfo;
            this.grvActivityInfo.Name = "grvActivityInfo";
            this.grvActivityInfo.OptionsBehavior.Editable = false;
            this.grvActivityInfo.OptionsBehavior.ReadOnly = true;
            this.grvActivityInfo.OptionsView.ColumnAutoWidth = false;
            this.grvActivityInfo.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.GrvActivityInfo_FocusedRowChanged);
            // 
            // gcolTimestamp
            // 
            this.gcolTimestamp.Caption = "Timestamp";
            this.gcolTimestamp.DisplayFormat.FormatString = "yyyy-MM-dd hh:mm:ss.fff";
            this.gcolTimestamp.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.gcolTimestamp.FieldName = "LogTimestamp";
            this.gcolTimestamp.Name = "gcolTimestamp";
            this.gcolTimestamp.Visible = true;
            this.gcolTimestamp.VisibleIndex = 0;
            this.gcolTimestamp.Width = 200;
            // 
            // gcolCategory
            // 
            this.gcolCategory.Caption = "Category";
            this.gcolCategory.FieldName = "Category";
            this.gcolCategory.Name = "gcolCategory";
            this.gcolCategory.Visible = true;
            this.gcolCategory.VisibleIndex = 1;
            this.gcolCategory.Width = 120;
            // 
            // gcolActivityName
            // 
            this.gcolActivityName.Caption = "ActivityName";
            this.gcolActivityName.FieldName = "ActivityName";
            this.gcolActivityName.Name = "gcolActivityName";
            this.gcolActivityName.Visible = true;
            this.gcolActivityName.VisibleIndex = 2;
            this.gcolActivityName.Width = 150;
            // 
            // gcolRootContxt
            // 
            this.gcolRootContxt.Caption = "Root Context ";
            this.gcolRootContxt.FieldName = "ContextName";
            this.gcolRootContxt.Name = "gcolRootContxt";
            this.gcolRootContxt.Visible = true;
            this.gcolRootContxt.VisibleIndex = 3;
            this.gcolRootContxt.Width = 120;
            // 
            // gcolElapsedTime
            // 
            this.gcolElapsedTime.Caption = "Elapsed (ms)";
            this.gcolElapsedTime.DisplayFormat.FormatString = "N2";
            this.gcolElapsedTime.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gcolElapsedTime.FieldName = "ElapsedTime";
            this.gcolElapsedTime.Name = "gcolElapsedTime";
            this.gcolElapsedTime.Visible = true;
            this.gcolElapsedTime.VisibleIndex = 4;
            this.gcolElapsedTime.Width = 120;
            // 
            // gcolMachine
            // 
            this.gcolMachine.Caption = "Host";
            this.gcolMachine.FieldName = "MachineName";
            this.gcolMachine.Name = "gcolMachine";
            this.gcolMachine.Visible = true;
            this.gcolMachine.VisibleIndex = 5;
            // 
            // gcolProcessId
            // 
            this.gcolProcessId.Caption = "Process Id";
            this.gcolProcessId.FieldName = "ProcessId";
            this.gcolProcessId.Name = "gcolProcessId";
            this.gcolProcessId.Visible = true;
            this.gcolProcessId.VisibleIndex = 6;
            // 
            // dateFrom
            // 
            this.dateFrom.EditValue = null;
            this.dateFrom.Location = new System.Drawing.Point(49, 14);
            this.dateFrom.Name = "dateFrom";
            this.dateFrom.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateFrom.Properties.CalendarTimeEditing = DevExpress.Utils.DefaultBoolean.True;
            this.dateFrom.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateFrom.Properties.DisplayFormat.FormatString = "yyyy-MM-dd HH:mm";
            this.dateFrom.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateFrom.Properties.EditFormat.FormatString = "yyyy-MM-dd HH:mm";
            this.dateFrom.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateFrom.Properties.Mask.EditMask = "yyyy-MM-dd HH:mm";
            this.dateFrom.Size = new System.Drawing.Size(170, 18);
            this.dateFrom.TabIndex = 3;
            // 
            // dateTo
            // 
            this.dateTo.EditValue = null;
            this.dateTo.Location = new System.Drawing.Point(243, 14);
            this.dateTo.Name = "dateTo";
            this.dateTo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateTo.Properties.CalendarTimeEditing = DevExpress.Utils.DefaultBoolean.True;
            this.dateTo.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateTo.Properties.DisplayFormat.FormatString = "yyyy-MM-dd HH:mm";
            this.dateTo.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateTo.Properties.EditFormat.FormatString = "yyyy-MM-dd HH:mm";
            this.dateTo.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateTo.Properties.Mask.EditMask = "yyyy-MM-dd HH:mm";
            this.dateTo.Size = new System.Drawing.Size(170, 18);
            this.dateTo.TabIndex = 4;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(13, 17);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(24, 12);
            this.labelControl1.TabIndex = 5;
            this.labelControl1.Text = "기간";
            // 
            // btnRecentHour
            // 
            this.btnRecentHour.AutoSize = true;
            this.btnRecentHour.Location = new System.Drawing.Point(419, 17);
            this.btnRecentHour.Name = "btnRecentHour";
            this.btnRecentHour.Size = new System.Drawing.Size(91, 12);
            this.btnRecentHour.TabIndex = 6;
            this.btnRecentHour.TabStop = true;
            this.btnRecentHour.Text = "최근 1시간 설정";
            this.btnRecentHour.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.BtnRecentHour_LinkClicked);
            // 
            // btnToday
            // 
            this.btnToday.AutoSize = true;
            this.btnToday.Location = new System.Drawing.Point(516, 17);
            this.btnToday.Name = "btnToday";
            this.btnToday.Size = new System.Drawing.Size(57, 12);
            this.btnToday.TabIndex = 7;
            this.btnToday.TabStop = true;
            this.btnToday.Text = "오늘 설정";
            this.btnToday.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.BtnToday_LinkClicked);
            // 
            // btnDeleteAll
            // 
            this.btnDeleteAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeleteAll.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnDeleteAll.ImageOptions.Image")));
            this.btnDeleteAll.ImageOptions.SvgImageSize = new System.Drawing.Size(16, 16);
            this.btnDeleteAll.Location = new System.Drawing.Point(637, 12);
            this.btnDeleteAll.Name = "btnDeleteAll";
            this.btnDeleteAll.Size = new System.Drawing.Size(102, 23);
            this.btnDeleteAll.TabIndex = 8;
            this.btnDeleteAll.Text = "전체 삭제";
            this.btnDeleteAll.Click += new System.EventHandler(this.BtnDeleteAll_Click);
            // 
            // PerformanceLogViewerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(859, 473);
            this.Controls.Add(this.btnDeleteAll);
            this.Controls.Add(this.btnToday);
            this.Controls.Add(this.btnRecentHour);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.dateTo);
            this.Controls.Add(this.dateFrom);
            this.Controls.Add(this.grdActivityInfo);
            this.Controls.Add(this.tlstContextInfo);
            this.Controls.Add(this.btnQuery);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PerformanceLogViewerForm";
            this.Text = "성능 로그 뷰어";
            this.Load += new System.EventHandler(this.PerformanceLogViewerForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tlstContextInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsContextInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdActivityInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvActivityInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsActivityInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateFrom.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateFrom.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTo.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTo.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnQuery;
        private DevExpress.XtraTreeList.TreeList tlstContextInfo;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colTimestamp;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colContextName;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colInclusiveTime;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colExclusive;
        private DevExpress.XtraGrid.GridControl grdActivityInfo;
        private DevExpress.XtraGrid.Views.Grid.GridView grvActivityInfo;
        private DevExpress.XtraGrid.Columns.GridColumn gcolTimestamp;
        private DevExpress.XtraGrid.Columns.GridColumn gcolActivityName;
        private DevExpress.XtraGrid.Columns.GridColumn gcolCategory;
        private DevExpress.XtraGrid.Columns.GridColumn gcolElapsedTime;
        private DevExpress.XtraGrid.Columns.GridColumn gcolMachine;
        private DevExpress.XtraGrid.Columns.GridColumn gcolProcessId;
        private System.Windows.Forms.BindingSource bsActivityInfo;
        private System.Windows.Forms.BindingSource bsContextInfo;
        private DevExpress.XtraGrid.Columns.GridColumn gcolRootContxt;
        private DevExpress.XtraEditors.DateEdit dateFrom;
        private DevExpress.XtraEditors.DateEdit dateTo;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private System.Windows.Forms.LinkLabel btnRecentHour;
        private System.Windows.Forms.LinkLabel btnToday;
        private DevExpress.XtraEditors.SimpleButton btnDeleteAll;
    }
}