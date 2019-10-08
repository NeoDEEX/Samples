namespace DataServiceClient
{
    partial class AccessBizServiceForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AccessBizServiceForm));
            this.btnQuery = new DevExpress.XtraEditors.SimpleButton();
            this.grdProducts = new DevExpress.XtraGrid.GridControl();
            this.grvProducts = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.lookupSuppliers = new DevExpress.XtraEditors.LookUpEdit();
            this.lookupCategories = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.btnGenerateError = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.grdProducts)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvProducts)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookupSuppliers.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookupCategories.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnQuery
            // 
            this.btnQuery.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnQuery.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnQuery.ImageOptions.SvgImage")));
            this.btnQuery.ImageOptions.SvgImageSize = new System.Drawing.Size(16, 16);
            this.btnQuery.Location = new System.Drawing.Point(747, 31);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(100, 23);
            this.btnQuery.TabIndex = 0;
            this.btnQuery.Text = "데이터 조회";
            this.btnQuery.Click += new System.EventHandler(this.BtnQuery_Click);
            // 
            // grdProducts
            // 
            this.grdProducts.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdProducts.Location = new System.Drawing.Point(12, 75);
            this.grdProducts.MainView = this.grvProducts;
            this.grdProducts.Name = "grdProducts";
            this.grdProducts.Size = new System.Drawing.Size(852, 426);
            this.grdProducts.TabIndex = 1;
            this.grdProducts.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvProducts});
            // 
            // grvProducts
            // 
            this.grvProducts.GridControl = this.grdProducts;
            this.grvProducts.Name = "grvProducts";
            this.grvProducts.OptionsCustomization.AllowGroup = false;
            this.grvProducts.OptionsView.ShowGroupPanel = false;
            // 
            // lookupSuppliers
            // 
            this.lookupSuppliers.Location = new System.Drawing.Point(317, 33);
            this.lookupSuppliers.Name = "lookupSuppliers";
            this.lookupSuppliers.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;
            this.lookupSuppliers.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup;
            this.lookupSuppliers.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookupSuppliers.Properties.DisplayMember = "CompanyName";
            this.lookupSuppliers.Properties.NullText = "All Suppliers";
            this.lookupSuppliers.Properties.ValueMember = "SupplierID";
            this.lookupSuppliers.Size = new System.Drawing.Size(161, 18);
            this.lookupSuppliers.TabIndex = 2;
            // 
            // lookupCategories
            // 
            this.lookupCategories.Location = new System.Drawing.Point(66, 33);
            this.lookupCategories.Name = "lookupCategories";
            this.lookupCategories.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;
            this.lookupCategories.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup;
            this.lookupCategories.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookupCategories.Properties.DisplayMember = "CategoryName";
            this.lookupCategories.Properties.NullText = "All Categories";
            this.lookupCategories.Properties.ValueMember = "CategoryID";
            this.lookupCategories.Size = new System.Drawing.Size(161, 18);
            this.lookupCategories.TabIndex = 3;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(9, 36);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(51, 12);
            this.labelControl1.TabIndex = 4;
            this.labelControl1.Text = "Category";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(265, 36);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(46, 12);
            this.labelControl2.TabIndex = 5;
            this.labelControl2.Text = "Supplier";
            // 
            // groupControl1
            // 
            this.groupControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControl1.Controls.Add(this.btnGenerateError);
            this.groupControl1.Controls.Add(this.lookupSuppliers);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.btnQuery);
            this.groupControl1.Controls.Add(this.lookupCategories);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Location = new System.Drawing.Point(12, 10);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(852, 59);
            this.groupControl1.TabIndex = 6;
            this.groupControl1.Text = "조회 조건";
            // 
            // btnGenerateError
            // 
            this.btnGenerateError.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGenerateError.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnGenerateError.ImageOptions.SvgImage")));
            this.btnGenerateError.ImageOptions.SvgImageSize = new System.Drawing.Size(16, 16);
            this.btnGenerateError.Location = new System.Drawing.Point(641, 31);
            this.btnGenerateError.Name = "btnGenerateError";
            this.btnGenerateError.Size = new System.Drawing.Size(100, 23);
            this.btnGenerateError.TabIndex = 6;
            this.btnGenerateError.Text = "오류 발생!";
            this.btnGenerateError.Click += new System.EventHandler(this.BtnGenerateError_Click);
            // 
            // AccessBizServiceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(876, 513);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.grdProducts);
            this.Name = "AccessBizServiceForm";
            this.Text = "비즈 서비스 호출 화면(로그 생성)";
            this.Load += new System.EventHandler(this.AccessBizServiceForm_Load);
            this.Controls.SetChildIndex(this.grdProducts, 0);
            this.Controls.SetChildIndex(this.groupControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.grdProducts)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvProducts)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookupSuppliers.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookupCategories.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnQuery;
        private DevExpress.XtraGrid.GridControl grdProducts;
        private DevExpress.XtraGrid.Views.Grid.GridView grvProducts;
        private DevExpress.XtraEditors.LookUpEdit lookupSuppliers;
        private DevExpress.XtraEditors.LookUpEdit lookupCategories;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.SimpleButton btnGenerateError;
    }
}