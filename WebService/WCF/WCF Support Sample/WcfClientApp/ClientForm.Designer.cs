namespace WcfClientApp
{
    partial class ClientForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ClientForm));
            this.grdProducts = new DevExpress.XtraGrid.GridControl();
            this.grvProducts = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.btnQuery = new DevExpress.XtraEditors.SimpleButton();
            this.btnTest1 = new DevExpress.XtraEditors.SimpleButton();
            this.btnTest2 = new DevExpress.XtraEditors.SimpleButton();
            this.btnTest3 = new DevExpress.XtraEditors.SimpleButton();
            this.btnTest4 = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.grdProducts)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvProducts)).BeginInit();
            this.SuspendLayout();
            // 
            // grdProducts
            // 
            this.grdProducts.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdProducts.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.grdProducts.Location = new System.Drawing.Point(12, 51);
            this.grdProducts.MainView = this.grvProducts;
            this.grdProducts.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.grdProducts.Name = "grdProducts";
            this.grdProducts.Size = new System.Drawing.Size(782, 359);
            this.grdProducts.TabIndex = 0;
            this.grdProducts.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvProducts});
            // 
            // grvProducts
            // 
            this.grvProducts.DetailHeight = 437;
            this.grvProducts.GridControl = this.grdProducts;
            this.grvProducts.Name = "grvProducts";
            this.grvProducts.OptionsBehavior.Editable = false;
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(12, 15);
            this.btnQuery.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(75, 29);
            this.btnQuery.TabIndex = 2;
            this.btnQuery.Text = "조회";
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // btnTest1
            // 
            this.btnTest1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTest1.Location = new System.Drawing.Point(456, 15);
            this.btnTest1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnTest1.Name = "btnTest1";
            this.btnTest1.Size = new System.Drawing.Size(80, 29);
            this.btnTest1.TabIndex = 3;
            this.btnTest1.Text = "테스트 #1";
            this.btnTest1.ToolTip = "바인딩 맵 사용 예제";
            this.btnTest1.Click += new System.EventHandler(this.btnTest1_Click);
            // 
            // btnTest2
            // 
            this.btnTest2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTest2.Location = new System.Drawing.Point(542, 15);
            this.btnTest2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnTest2.Name = "btnTest2";
            this.btnTest2.Size = new System.Drawing.Size(80, 29);
            this.btnTest2.TabIndex = 2;
            this.btnTest2.Text = "테스트 #2";
            this.btnTest2.ToolTip = "인증 정보 전달 예제";
            this.btnTest2.Click += new System.EventHandler(this.btnTest2_Click);
            // 
            // btnTest3
            // 
            this.btnTest3.Location = new System.Drawing.Point(628, 15);
            this.btnTest3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnTest3.Name = "btnTest3";
            this.btnTest3.Size = new System.Drawing.Size(80, 29);
            this.btnTest3.TabIndex = 4;
            this.btnTest3.Text = "테스트 #3";
            this.btnTest3.ToolTip = "주소 맵이나 바인딩 맵을 사용하지 않는 예제";
            this.btnTest3.Click += new System.EventHandler(this.BtnTest3_Click);
            // 
            // btnTest4
            // 
            this.btnTest4.Location = new System.Drawing.Point(714, 15);
            this.btnTest4.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnTest4.Name = "btnTest4";
            this.btnTest4.Size = new System.Drawing.Size(80, 29);
            this.btnTest4.TabIndex = 5;
            this.btnTest4.Text = "테스트 #4";
            this.btnTest4.ToolTip = "압축이 적용된 서비스 호출 예제";
            this.btnTest4.Click += new System.EventHandler(this.BtnTest4_Click);
            // 
            // ClientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(806, 425);
            this.Controls.Add(this.btnTest4);
            this.Controls.Add(this.btnTest3);
            this.Controls.Add(this.btnTest1);
            this.Controls.Add(this.btnTest2);
            this.Controls.Add(this.btnQuery);
            this.Controls.Add(this.grdProducts);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "ClientForm";
            this.Text = "NeoDEEX based WCF Client";
            ((System.ComponentModel.ISupportInitialize)(this.grdProducts)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvProducts)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl grdProducts;
        private DevExpress.XtraGrid.Views.Grid.GridView grvProducts;
        private DevExpress.XtraEditors.SimpleButton btnQuery;
        private DevExpress.XtraEditors.SimpleButton btnTest1;
        private DevExpress.XtraEditors.SimpleButton btnTest2;
        private DevExpress.XtraEditors.SimpleButton btnTest3;
        private DevExpress.XtraEditors.SimpleButton btnTest4;
    }
}

