namespace MenuApp
{
    partial class DeferredLoadErrorForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DeferredLoadErrorForm));
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.memoEdit1 = new DevExpress.XtraEditors.MemoEdit();
            this.memoEdit2 = new DevExpress.XtraEditors.MemoEdit();
            this.pictureEdit1 = new DevExpress.XtraEditors.PictureEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.memoEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoEdit2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panelControl1.Appearance.BackColor = System.Drawing.Color.White;
            this.panelControl1.Appearance.Options.UseBackColor = true;
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Controls.Add(this.pictureEdit1);
            this.panelControl1.Controls.Add(this.memoEdit2);
            this.panelControl1.Controls.Add(this.memoEdit1);
            this.panelControl1.Location = new System.Drawing.Point(154, 94);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(386, 261);
            this.panelControl1.TabIndex = 0;
            // 
            // memoEdit1
            // 
            this.memoEdit1.Location = new System.Drawing.Point(56, 37);
            this.memoEdit1.Name = "memoEdit1";
            this.memoEdit1.Size = new System.Drawing.Size(325, 49);
            this.memoEdit1.TabIndex = 0;
            // 
            // memoEdit2
            // 
            this.memoEdit2.Location = new System.Drawing.Point(4, 92);
            this.memoEdit2.Name = "memoEdit2";
            this.memoEdit2.Size = new System.Drawing.Size(376, 163);
            this.memoEdit2.TabIndex = 1;
            // 
            // pictureEdit1
            // 
            this.pictureEdit1.EditValue = ((object)(resources.GetObject("pictureEdit1.EditValue")));
            this.pictureEdit1.Location = new System.Drawing.Point(5, 38);
            this.pictureEdit1.Name = "pictureEdit1";
            this.pictureEdit1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pictureEdit1.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.pictureEdit1.Size = new System.Drawing.Size(45, 48);
            this.pictureEdit1.TabIndex = 2;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl1.Location = new System.Drawing.Point(4, 5);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(376, 16);
            this.labelControl1.TabIndex = 3;
            this.labelControl1.Text = "labelControl1";
            // 
            // DeferredLoadErrorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(686, 416);
            this.Controls.Add(this.panelControl1);
            this.Name = "DeferredLoadErrorForm";
            this.Text = "DeferredLoadErrorForm";
            this.Load += new System.EventHandler(this.DeferredLoadErrorForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.memoEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoEdit2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.PictureEdit pictureEdit1;
        private DevExpress.XtraEditors.MemoEdit memoEdit2;
        private DevExpress.XtraEditors.MemoEdit memoEdit1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
    }
}