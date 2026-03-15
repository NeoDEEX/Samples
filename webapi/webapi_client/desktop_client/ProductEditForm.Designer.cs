namespace desktop_client
{
    partial class ProductEditForm
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
            ProductsGridView = new DataGridView();
            ProductIdColumn = new DataGridViewTextBoxColumn();
            ProductNameColumn = new DataGridViewTextBoxColumn();
            UnitPriceColumn = new DataGridViewTextBoxColumn();
            SaveButton = new Button();
            RefreshButton = new Button();
            CloseButton = new Button();
            ((System.ComponentModel.ISupportInitialize)ProductsGridView).BeginInit();
            SuspendLayout();
            // 
            // ProductsGridView
            // 
            ProductsGridView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            ProductsGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            ProductsGridView.Columns.AddRange(new DataGridViewColumn[] { ProductIdColumn, ProductNameColumn, UnitPriceColumn });
            ProductsGridView.Location = new Point(12, 12);
            ProductsGridView.Name = "ProductsGridView";
            ProductsGridView.Size = new Size(574, 270);
            ProductsGridView.TabIndex = 0;
            // 
            // ProductIdColumn
            // 
            ProductIdColumn.DataPropertyName = "product_id";
            ProductIdColumn.HeaderText = "Product Id";
            ProductIdColumn.Name = "ProductIdColumn";
            // 
            // ProductNameColumn
            // 
            ProductNameColumn.DataPropertyName = "product_name";
            ProductNameColumn.HeaderText = "Product Name";
            ProductNameColumn.Name = "ProductNameColumn";
            ProductNameColumn.Width = 300;
            // 
            // UnitPriceColumn
            // 
            UnitPriceColumn.DataPropertyName = "unit_price";
            UnitPriceColumn.HeaderText = "Unit Price";
            UnitPriceColumn.Name = "UnitPriceColumn";
            // 
            // SaveButton
            // 
            SaveButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            SaveButton.Location = new Point(93, 288);
            SaveButton.Name = "SaveButton";
            SaveButton.Size = new Size(75, 23);
            SaveButton.TabIndex = 1;
            SaveButton.Text = "&Save";
            SaveButton.UseVisualStyleBackColor = true;
            SaveButton.Click += SaveButton_Click;
            // 
            // RefreshButton
            // 
            RefreshButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            RefreshButton.Location = new Point(12, 288);
            RefreshButton.Name = "RefreshButton";
            RefreshButton.Size = new Size(75, 23);
            RefreshButton.TabIndex = 1;
            RefreshButton.Text = "&Refesh";
            RefreshButton.UseVisualStyleBackColor = true;
            RefreshButton.Click += RefreshButton_Click;
            // 
            // CloseButton
            // 
            CloseButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            CloseButton.Location = new Point(511, 288);
            CloseButton.Name = "CloseButton";
            CloseButton.Size = new Size(75, 23);
            CloseButton.TabIndex = 1;
            CloseButton.Text = "&Close";
            CloseButton.UseVisualStyleBackColor = true;
            CloseButton.Click += CloseButton_Click;
            // 
            // ProductEditForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(598, 323);
            Controls.Add(RefreshButton);
            Controls.Add(CloseButton);
            Controls.Add(SaveButton);
            Controls.Add(ProductsGridView);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "ProductEditForm";
            Text = "Edit Products";
            Load += ProductEditForm_Load;
            ((System.ComponentModel.ISupportInitialize)ProductsGridView).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView ProductsGridView;
        private DataGridViewTextBoxColumn ProductIdColumn;
        private DataGridViewTextBoxColumn ProductNameColumn;
        private DataGridViewTextBoxColumn UnitPriceColumn;
        private Button SaveButton;
        private Button RefreshButton;
        private Button CloseButton;
    }
}
