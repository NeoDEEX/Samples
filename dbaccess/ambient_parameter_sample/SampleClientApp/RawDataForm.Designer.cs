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
            grdProducts = new DevExpress.XtraGrid.GridControl();
            grvProducts = new DevExpress.XtraGrid.Views.Grid.GridView();
            btnOK = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)grdProducts).BeginInit();
            ((System.ComponentModel.ISupportInitialize)grvProducts).BeginInit();
            SuspendLayout();
            // 
            // grdProducts
            // 
            grdProducts.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            grdProducts.EmbeddedNavigator.Margin = new Padding(3, 4, 3, 4);
            grdProducts.Location = new Point(12, 14);
            grdProducts.MainView = grvProducts;
            grdProducts.Margin = new Padding(3, 4, 3, 4);
            grdProducts.Name = "grdProducts";
            grdProducts.Size = new Size(764, 383);
            grdProducts.TabIndex = 0;
            grdProducts.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { grvProducts });
            // 
            // grvProducts
            // 
            grvProducts.DetailHeight = 408;
            grvProducts.GridControl = grdProducts;
            grvProducts.Name = "grvProducts";
            grvProducts.OptionsBehavior.Editable = false;
            grvProducts.OptionsView.ShowGroupPanel = false;
            // 
            // btnOK
            // 
            btnOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnOK.DialogResult = DialogResult.OK;
            btnOK.Location = new Point(701, 404);
            btnOK.Margin = new Padding(3, 4, 3, 4);
            btnOK.Name = "btnOK";
            btnOK.Size = new Size(75, 27);
            btnOK.TabIndex = 1;
            btnOK.Text = "OK";
            // 
            // RawDataForm
            // 
            Appearance.BackColor = Color.White;
            Appearance.Options.UseBackColor = true;
            AutoScaleDimensions = new SizeF(7F, 14F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(788, 444);
            Controls.Add(btnOK);
            Controls.Add(grdProducts);
            IconOptions.Icon = (Icon)resources.GetObject("RawDataForm.IconOptions.Icon");
            Margin = new Padding(3, 4, 3, 4);
            Name = "RawDataForm";
            Text = "Raw 'Products' Table Data";
            Load += RawDataForm_Load;
            ((System.ComponentModel.ISupportInitialize)grdProducts).EndInit();
            ((System.ComponentModel.ISupportInitialize)grvProducts).EndInit();
            ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl grdProducts;
        private DevExpress.XtraGrid.Views.Grid.GridView grvProducts;
        private DevExpress.XtraEditors.SimpleButton btnOK;
    }
}