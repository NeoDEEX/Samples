namespace DataServiceClient
{
    partial class DbProfileViewForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DbProfileViewForm));
            this.btnToday = new System.Windows.Forms.LinkLabel();
            this.btnRecentHour = new System.Windows.Forms.LinkLabel();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.dateTo = new DevExpress.XtraEditors.DateEdit();
            this.dateFrom = new DevExpress.XtraEditors.DateEdit();
            this.btnQuery = new DevExpress.XtraEditors.SimpleButton();
            this.grdDbProfile = new DevExpress.XtraGrid.GridControl();
            this.grvDbProfile = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colLogTimestamp = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colExecutionType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colExecutionTime = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFoxQueryId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colQueryText = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemMemoEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.behaviorManager1 = new DevExpress.Utils.Behaviors.BehaviorManager(this.components);
            this.btnDeleteAll = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.dateTo.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateFrom.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateFrom.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdDbProfile)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvDbProfile)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.behaviorManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnToday
            // 
            this.btnToday.AutoSize = true;
            this.btnToday.Location = new System.Drawing.Point(513, 15);
            this.btnToday.Name = "btnToday";
            this.btnToday.Size = new System.Drawing.Size(57, 12);
            this.btnToday.TabIndex = 13;
            this.btnToday.TabStop = true;
            this.btnToday.Text = "오늘 설정";
            this.btnToday.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.BtnToday_LinkClicked);
            // 
            // btnRecentHour
            // 
            this.btnRecentHour.AutoSize = true;
            this.btnRecentHour.Location = new System.Drawing.Point(416, 15);
            this.btnRecentHour.Name = "btnRecentHour";
            this.btnRecentHour.Size = new System.Drawing.Size(91, 12);
            this.btnRecentHour.TabIndex = 12;
            this.btnRecentHour.TabStop = true;
            this.btnRecentHour.Text = "최근 1시간 설정";
            this.btnRecentHour.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.BtnRecentHour_LinkClicked);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(10, 15);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(24, 12);
            this.labelControl1.TabIndex = 11;
            this.labelControl1.Text = "기간";
            // 
            // dateTo
            // 
            this.dateTo.EditValue = null;
            this.dateTo.Location = new System.Drawing.Point(240, 12);
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
            this.dateTo.TabIndex = 10;
            // 
            // dateFrom
            // 
            this.dateFrom.EditValue = null;
            this.dateFrom.Location = new System.Drawing.Point(46, 12);
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
            this.dateFrom.TabIndex = 9;
            // 
            // btnQuery
            // 
            this.btnQuery.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnQuery.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnQuery.ImageOptions.SvgImage")));
            this.btnQuery.ImageOptions.SvgImageSize = new System.Drawing.Size(16, 16);
            this.btnQuery.Location = new System.Drawing.Point(773, 7);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(102, 23);
            this.btnQuery.TabIndex = 8;
            this.btnQuery.Text = "조회";
            this.btnQuery.Click += new System.EventHandler(this.BtnQuery_Click);
            // 
            // grdDbProfile
            // 
            this.grdDbProfile.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdDbProfile.Location = new System.Drawing.Point(10, 36);
            this.grdDbProfile.MainView = this.grvDbProfile;
            this.grdDbProfile.Name = "grdDbProfile";
            this.grdDbProfile.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemMemoEdit1});
            this.grdDbProfile.Size = new System.Drawing.Size(865, 402);
            this.grdDbProfile.TabIndex = 14;
            this.grdDbProfile.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvDbProfile});
            // 
            // grvDbProfile
            // 
            this.grvDbProfile.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colLogTimestamp,
            this.colExecutionType,
            this.colExecutionTime,
            this.colFoxQueryId,
            this.colQueryText});
            this.grvDbProfile.GridControl = this.grdDbProfile;
            this.grvDbProfile.Name = "grvDbProfile";
            this.grvDbProfile.OptionsBehavior.Editable = false;
            this.grvDbProfile.OptionsBehavior.ReadOnly = true;
            this.grvDbProfile.OptionsView.ColumnAutoWidth = false;
            this.grvDbProfile.DoubleClick += new System.EventHandler(this.GrvDbProfile_DoubleClick);
            // 
            // colLogTimestamp
            // 
            this.colLogTimestamp.Caption = "로그 발생 시간";
            this.colLogTimestamp.DisplayFormat.FormatString = "yyyy-MM-dd HH:mm:ss.fff";
            this.colLogTimestamp.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colLogTimestamp.FieldName = "LogTimestamp";
            this.colLogTimestamp.Name = "colLogTimestamp";
            this.colLogTimestamp.Visible = true;
            this.colLogTimestamp.VisibleIndex = 0;
            this.colLogTimestamp.Width = 150;
            // 
            // colExecutionType
            // 
            this.colExecutionType.Caption = "DB 액세스 타입";
            this.colExecutionType.FieldName = "ExecutionType";
            this.colExecutionType.Name = "colExecutionType";
            this.colExecutionType.Visible = true;
            this.colExecutionType.VisibleIndex = 1;
            this.colExecutionType.Width = 100;
            // 
            // colExecutionTime
            // 
            this.colExecutionTime.Caption = "수행 시간(ms)";
            this.colExecutionTime.DisplayFormat.FormatString = "N3";
            this.colExecutionTime.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colExecutionTime.FieldName = "ExecutionTime";
            this.colExecutionTime.Name = "colExecutionTime";
            this.colExecutionTime.Visible = true;
            this.colExecutionTime.VisibleIndex = 2;
            this.colExecutionTime.Width = 100;
            // 
            // colFoxQueryId
            // 
            this.colFoxQueryId.Caption = "Fox Query ID";
            this.colFoxQueryId.FieldName = "FoxQueryId";
            this.colFoxQueryId.Name = "colFoxQueryId";
            this.colFoxQueryId.Visible = true;
            this.colFoxQueryId.VisibleIndex = 3;
            this.colFoxQueryId.Width = 140;
            // 
            // colQueryText
            // 
            this.colQueryText.AppearanceCell.FontStyleDelta = System.Drawing.FontStyle.Bold;
            this.colQueryText.AppearanceCell.ForeColor = System.Drawing.Color.Blue;
            this.colQueryText.AppearanceCell.Options.UseFont = true;
            this.colQueryText.AppearanceCell.Options.UseForeColor = true;
            this.colQueryText.Caption = "쿼리 문장";
            this.colQueryText.FieldName = "QueryText";
            this.colQueryText.Name = "colQueryText";
            this.colQueryText.Visible = true;
            this.colQueryText.VisibleIndex = 4;
            this.colQueryText.Width = 300;
            // 
            // repositoryItemMemoEdit1
            // 
            this.repositoryItemMemoEdit1.Name = "repositoryItemMemoEdit1";
            // 
            // btnDeleteAll
            // 
            this.btnDeleteAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeleteAll.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnDeleteAll.ImageOptions.Image")));
            this.btnDeleteAll.ImageOptions.SvgImageSize = new System.Drawing.Size(16, 16);
            this.btnDeleteAll.Location = new System.Drawing.Point(665, 7);
            this.btnDeleteAll.Name = "btnDeleteAll";
            this.btnDeleteAll.Size = new System.Drawing.Size(102, 23);
            this.btnDeleteAll.TabIndex = 15;
            this.btnDeleteAll.Text = "전체 삭제";
            this.btnDeleteAll.Click += new System.EventHandler(this.BtnDeleteAll_Click);
            // 
            // DbProfileViewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(887, 450);
            this.Controls.Add(this.btnDeleteAll);
            this.Controls.Add(this.grdDbProfile);
            this.Controls.Add(this.btnToday);
            this.Controls.Add(this.btnRecentHour);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.dateTo);
            this.Controls.Add(this.dateFrom);
            this.Controls.Add(this.btnQuery);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DbProfileViewForm";
            this.Text = "DB Profile 뷰어";
            this.Load += new System.EventHandler(this.DbProfileViewForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dateTo.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateFrom.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateFrom.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdDbProfile)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvDbProfile)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.behaviorManager1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.LinkLabel btnToday;
        private System.Windows.Forms.LinkLabel btnRecentHour;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.DateEdit dateTo;
        private DevExpress.XtraEditors.DateEdit dateFrom;
        private DevExpress.XtraEditors.SimpleButton btnQuery;
        private DevExpress.XtraGrid.GridControl grdDbProfile;
        private DevExpress.XtraGrid.Views.Grid.GridView grvDbProfile;
        private DevExpress.XtraGrid.Columns.GridColumn colLogTimestamp;
        private DevExpress.XtraGrid.Columns.GridColumn colExecutionType;
        private DevExpress.XtraGrid.Columns.GridColumn colExecutionTime;
        private DevExpress.XtraGrid.Columns.GridColumn colFoxQueryId;
        private DevExpress.XtraGrid.Columns.GridColumn colQueryText;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit1;
        private DevExpress.Utils.Behaviors.BehaviorManager behaviorManager1;
        private DevExpress.XtraEditors.SimpleButton btnDeleteAll;
    }
}