namespace adv_client
{
    partial class AdvProductEditForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdvProductEditForm));
            ProductsGrid = new DevExpress.XtraGrid.GridControl();
            ProductsGridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            ProductIdColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            ProductNameColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            UnitPriceColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            RefreshButton = new DevExpress.XtraEditors.SimpleButton();
            SaveButton = new DevExpress.XtraEditors.SimpleButton();
            CloseButton = new DevExpress.XtraEditors.SimpleButton();
            GridImages = new DevExpress.Utils.ImageCollection(components);
            DeleteButton = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)ProductsGrid).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ProductsGridView).BeginInit();
            ((System.ComponentModel.ISupportInitialize)GridImages).BeginInit();
            SuspendLayout();
            // 
            // ProductsGrid
            // 
            ProductsGrid.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            ProductsGrid.Location = new Point(12, 12);
            ProductsGrid.MainView = ProductsGridView;
            ProductsGrid.Name = "ProductsGrid";
            ProductsGrid.Size = new Size(614, 266);
            ProductsGrid.TabIndex = 0;
            ProductsGrid.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { ProductsGridView });
            // 
            // ProductsGridView
            // 
            ProductsGridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { ProductIdColumn, ProductNameColumn, UnitPriceColumn });
            ProductsGridView.GridControl = ProductsGrid;
            ProductsGridView.Name = "ProductsGridView";
            ProductsGridView.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.True;
            ProductsGridView.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
            ProductsGridView.OptionsView.ShowGroupPanel = false;
            ProductsGridView.CustomDrawRowIndicator += ProductsGridView_CustomDrawRowIndicator;
            ProductsGridView.RowStyle += ProductsGridView_RowStyle;
            ProductsGridView.ShowingEditor += ProductsGridView_ShowingEditor;
            // 
            // ProductIdColumn
            // 
            ProductIdColumn.AppearanceCell.ForeColor = Color.Gray;
            ProductIdColumn.AppearanceCell.Options.UseForeColor = true;
            ProductIdColumn.Caption = "Product Id";
            ProductIdColumn.FieldName = "product_id";
            ProductIdColumn.ImageOptions.Image = (Image)resources.GetObject("ProductIdColumn.ImageOptions.Image");
            ProductIdColumn.Name = "ProductIdColumn";
            ProductIdColumn.Visible = true;
            ProductIdColumn.VisibleIndex = 0;
            ProductIdColumn.Width = 142;
            // 
            // ProductNameColumn
            // 
            ProductNameColumn.Caption = "Product Name";
            ProductNameColumn.FieldName = "product_name";
            ProductNameColumn.Name = "ProductNameColumn";
            ProductNameColumn.Visible = true;
            ProductNameColumn.VisibleIndex = 1;
            ProductNameColumn.Width = 383;
            // 
            // UnitPriceColumn
            // 
            UnitPriceColumn.Caption = "Unit Price";
            UnitPriceColumn.FieldName = "unit_price";
            UnitPriceColumn.Name = "UnitPriceColumn";
            UnitPriceColumn.Visible = true;
            UnitPriceColumn.VisibleIndex = 2;
            UnitPriceColumn.Width = 160;
            // 
            // RefreshButton
            // 
            RefreshButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            RefreshButton.ImageOptions.Image = (Image)resources.GetObject("RefreshButton.ImageOptions.Image");
            RefreshButton.Location = new Point(12, 284);
            RefreshButton.Name = "RefreshButton";
            RefreshButton.Size = new Size(92, 23);
            RefreshButton.TabIndex = 1;
            RefreshButton.Text = "&Refresh";
            RefreshButton.Click += RefreshButton_Click;
            // 
            // SaveButton
            // 
            SaveButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            SaveButton.ImageOptions.Image = (Image)resources.GetObject("SaveButton.ImageOptions.Image");
            SaveButton.Location = new Point(208, 284);
            SaveButton.Name = "SaveButton";
            SaveButton.Size = new Size(92, 23);
            SaveButton.TabIndex = 2;
            SaveButton.Text = "&Save";
            SaveButton.Click += SaveButton_Click;
            // 
            // CloseButton
            // 
            CloseButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            CloseButton.ImageOptions.Image = (Image)resources.GetObject("CloseButton.ImageOptions.Image");
            CloseButton.Location = new Point(534, 284);
            CloseButton.Name = "CloseButton";
            CloseButton.Size = new Size(92, 23);
            CloseButton.TabIndex = 3;
            CloseButton.Text = "&Close";
            CloseButton.Click += CloseButton_Click;
            // 
            // GridImages
            // 
            GridImages.ImageStream = (DevExpress.Utils.ImageCollectionStreamer)resources.GetObject("GridImages.ImageStream");
            GridImages.Images.SetKeyName(0, "Modified");
            GridImages.Images.SetKeyName(1, "Added");
            GridImages.Images.SetKeyName(2, "Deleted");
            // 
            // DeleteButton
            // 
            DeleteButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            DeleteButton.ImageOptions.Image = (Image)resources.GetObject("DeleteButton.ImageOptions.Image");
            DeleteButton.Location = new Point(110, 284);
            DeleteButton.Name = "DeleteButton";
            DeleteButton.Size = new Size(92, 23);
            DeleteButton.TabIndex = 4;
            DeleteButton.Text = "&Delete";
            DeleteButton.Click += DeleteButton_Click;
            // 
            // AdvProductEditForm
            // 
            AutoScaleDimensions = new SizeF(7F, 14F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(638, 319);
            Controls.Add(DeleteButton);
            Controls.Add(CloseButton);
            Controls.Add(SaveButton);
            Controls.Add(RefreshButton);
            Controls.Add(ProductsGrid);
            IconOptions.Icon = (Icon)resources.GetObject("AdvProductEditForm.IconOptions.Icon");
            MaximizeBox = false;
            MinimumSize = new Size(640, 280);
            Name = "AdvProductEditForm";
            Text = "Edit Products";
            Load += AdvProductEditForm_Load;
            ((System.ComponentModel.ISupportInitialize)ProductsGrid).EndInit();
            ((System.ComponentModel.ISupportInitialize)ProductsGridView).EndInit();
            ((System.ComponentModel.ISupportInitialize)GridImages).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DevExpress.XtraGrid.GridControl ProductsGrid;
        private DevExpress.XtraGrid.Views.Grid.GridView ProductsGridView;
        private DevExpress.XtraGrid.Columns.GridColumn ProductIdColumn;
        private DevExpress.XtraGrid.Columns.GridColumn ProductNameColumn;
        private DevExpress.XtraGrid.Columns.GridColumn UnitPriceColumn;
        private DevExpress.XtraEditors.SimpleButton RefreshButton;
        private DevExpress.XtraEditors.SimpleButton SaveButton;
        private DevExpress.XtraEditors.SimpleButton CloseButton;
        private DevExpress.Utils.ImageCollection GridImages;
        private DevExpress.XtraEditors.SimpleButton DeleteButton;
    }
}
