namespace SampleClientApp
{
    partial class RawDataForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RawDataForm));
            this.grdProducts = new DevExpress.XtraGrid.GridControl();
            this.grvProducts = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.grdProducts)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvProducts)).BeginInit();
            this.SuspendLayout();
            // 
            // grdProducts
            // 
            this.grdProducts.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdProducts.Location = new System.Drawing.Point(12, 12);
            this.grdProducts.MainView = this.grvProducts;
            this.grdProducts.Name = "grdProducts";
            this.grdProducts.Size = new System.Drawing.Size(764, 328);
            this.grdProducts.TabIndex = 0;
            this.grdProducts.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvProducts});
            // 
            // grvProducts
            // 
            this.grvProducts.GridControl = this.grdProducts;
            this.grvProducts.Name = "grvProducts";
            this.grvProducts.OptionsBehavior.Editable = false;
            this.grvProducts.OptionsView.ShowGroupPanel = false;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(701, 346);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "OK";
            // 
            // RawDataForm
            // 
            this.Appearance.BackColor = System.Drawing.Color.White;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(788, 381);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.grdProducts);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "RawDataForm";
            this.Text = "Raw \'Product\' Table Data";
            this.Load += new System.EventHandler(this.RawDataForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdProducts)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvProducts)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl grdProducts;
        private DevExpress.XtraGrid.Views.Grid.GridView grvProducts;
        private DevExpress.XtraEditors.SimpleButton btnOK;
    }
}