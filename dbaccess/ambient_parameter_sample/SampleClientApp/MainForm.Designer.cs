namespace SampleClientApp
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
            this.grdProducts = new DevExpress.XtraGrid.GridControl();
            this.grvProducts = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colProductName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSupplierId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUnitPrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPackage = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnQuery = new DevExpress.XtraEditors.SimpleButton();
            this.btnUpdate = new DevExpress.XtraEditors.SimpleButton();
            this.imgcMain = new DevExpress.Utils.ImageCollection(this.components);
            this.lblUser = new DevExpress.XtraEditors.LabelControl();
            this.cbeUsers = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.lnkShowRawData = new DevExpress.XtraEditors.HyperlinkLabelControl();
            this.lnkWriteServerLog = new DevExpress.XtraEditors.HyperlinkLabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.grdProducts)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvProducts)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgcMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbeUsers.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // grdProducts
            // 
            this.grdProducts.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdProducts.Location = new System.Drawing.Point(12, 41);
            this.grdProducts.MainView = this.grvProducts;
            this.grdProducts.Name = "grdProducts";
            this.grdProducts.Size = new System.Drawing.Size(740, 318);
            this.grdProducts.TabIndex = 0;
            this.grdProducts.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvProducts});
            // 
            // grvProducts
            // 
            this.grvProducts.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colProductName,
            this.colSupplierId,
            this.colUnitPrice,
            this.colPackage});
            this.grvProducts.GridControl = this.grdProducts;
            this.grvProducts.Name = "grvProducts";
            this.grvProducts.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.grvProducts.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.grvProducts.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.grvProducts.OptionsView.ColumnAutoWidth = false;
            this.grvProducts.OptionsView.ShowGroupPanel = false;
            this.grvProducts.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.GrvProducts_CustomDrawRowIndicator);
            // 
            // colProductName
            // 
            this.colProductName.Caption = "Product Name";
            this.colProductName.FieldName = "ProductName";
            this.colProductName.Name = "colProductName";
            this.colProductName.Visible = true;
            this.colProductName.VisibleIndex = 0;
            this.colProductName.Width = 275;
            // 
            // colSupplierId
            // 
            this.colSupplierId.Caption = "Supplier Id";
            this.colSupplierId.FieldName = "SupplierId";
            this.colSupplierId.Name = "colSupplierId";
            this.colSupplierId.Visible = true;
            this.colSupplierId.VisibleIndex = 1;
            this.colSupplierId.Width = 118;
            // 
            // colUnitPrice
            // 
            this.colUnitPrice.Caption = "Unit Price";
            this.colUnitPrice.FieldName = "UnitPrice";
            this.colUnitPrice.Name = "colUnitPrice";
            this.colUnitPrice.Visible = true;
            this.colUnitPrice.VisibleIndex = 2;
            this.colUnitPrice.Width = 116;
            // 
            // colPackage
            // 
            this.colPackage.Caption = "Package";
            this.colPackage.FieldName = "Package";
            this.colPackage.Name = "colPackage";
            this.colPackage.Visible = true;
            this.colPackage.VisibleIndex = 3;
            this.colPackage.Width = 168;
            // 
            // btnQuery
            // 
            this.btnQuery.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnQuery.ImageOptions.SvgImage")));
            this.btnQuery.ImageOptions.SvgImageSize = new System.Drawing.Size(16, 16);
            this.btnQuery.Location = new System.Drawing.Point(12, 12);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(101, 23);
            this.btnQuery.TabIndex = 1;
            this.btnQuery.Text = "Query";
            this.btnQuery.Click += new System.EventHandler(this.BtnQuery_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnUpdate.ImageOptions.SvgImage")));
            this.btnUpdate.ImageOptions.SvgImageSize = new System.Drawing.Size(16, 16);
            this.btnUpdate.Location = new System.Drawing.Point(119, 12);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(101, 23);
            this.btnUpdate.TabIndex = 2;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.Click += new System.EventHandler(this.BtnUpdate_Click);
            // 
            // imgcMain
            // 
            this.imgcMain.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imgcMain.ImageStream")));
            this.imgcMain.Images.SetKeyName(0, "Modified");
            this.imgcMain.Images.SetKeyName(1, "User1");
            this.imgcMain.Images.SetKeyName(2, "User2");
            this.imgcMain.Images.SetKeyName(3, "User3");
            // 
            // lblUser
            // 
            this.lblUser.AllowHtmlString = true;
            this.lblUser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblUser.HtmlImages = this.imgcMain;
            this.lblUser.Location = new System.Drawing.Point(504, 17);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(65, 12);
            this.lblUser.TabIndex = 3;
            this.lblUser.Text = "Select User";
            // 
            // cbeUsers
            // 
            this.cbeUsers.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbeUsers.EditValue = "TestUser1";
            this.cbeUsers.Location = new System.Drawing.Point(575, 14);
            this.cbeUsers.Name = "cbeUsers";
            this.cbeUsers.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbeUsers.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("TestUser1", "TestUser1", 1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("TestUser2", "TestUser2", 2),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("TestUser3", "TestUser3", 3)});
            this.cbeUsers.Properties.SmallImages = this.imgcMain;
            this.cbeUsers.Size = new System.Drawing.Size(177, 18);
            this.cbeUsers.TabIndex = 4;
            this.cbeUsers.SelectedIndexChanged += new System.EventHandler(this.CbeUsers_SelectedIndexChanged);
            // 
            // lnkShowRawData
            // 
            this.lnkShowRawData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lnkShowRawData.Location = new System.Drawing.Point(12, 365);
            this.lnkShowRawData.Name = "lnkShowRawData";
            this.lnkShowRawData.Size = new System.Drawing.Size(85, 12);
            this.lnkShowRawData.TabIndex = 5;
            this.lnkShowRawData.Text = "Show raw data";
            this.lnkShowRawData.Click += new System.EventHandler(this.LnkShowRawData_Click);
            // 
            // lnkWriteServerLog
            // 
            this.lnkWriteServerLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lnkWriteServerLog.Location = new System.Drawing.Point(660, 365);
            this.lnkWriteServerLog.Name = "lnkWriteServerLog";
            this.lnkWriteServerLog.Size = new System.Drawing.Size(92, 12);
            this.lnkWriteServerLog.TabIndex = 6;
            this.lnkWriteServerLog.Text = "Write Server Log";
            this.lnkWriteServerLog.Click += new System.EventHandler(this.LnkWriteServerLog_Click);
            // 
            // MainForm
            // 
            this.Appearance.BackColor = System.Drawing.Color.White;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(764, 384);
            this.Controls.Add(this.lnkWriteServerLog);
            this.Controls.Add(this.lnkShowRawData);
            this.Controls.Add(this.cbeUsers);
            this.Controls.Add(this.lblUser);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnQuery);
            this.Controls.Add(this.grdProducts);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "Simple Data Update Example using Data Service";
            ((System.ComponentModel.ISupportInitialize)(this.grdProducts)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvProducts)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgcMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbeUsers.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraGrid.GridControl grdProducts;
        private DevExpress.XtraGrid.Views.Grid.GridView grvProducts;
        private DevExpress.XtraEditors.SimpleButton btnQuery;
        private DevExpress.XtraEditors.SimpleButton btnUpdate;
        private DevExpress.XtraGrid.Columns.GridColumn colProductName;
        private DevExpress.XtraGrid.Columns.GridColumn colSupplierId;
        private DevExpress.XtraGrid.Columns.GridColumn colUnitPrice;
        private DevExpress.XtraGrid.Columns.GridColumn colPackage;
        private DevExpress.Utils.ImageCollection imgcMain;
        private DevExpress.XtraEditors.LabelControl lblUser;
        private DevExpress.XtraEditors.ImageComboBoxEdit cbeUsers;
        private DevExpress.XtraEditors.HyperlinkLabelControl lnkShowRawData;
        private DevExpress.XtraEditors.HyperlinkLabelControl lnkWriteServerLog;
    }
}

