using NeoDEEX.ServiceModel.Client.Data;
using NeoDEEX.ServiceModel.Data;
using System.Data;

namespace desktop_client
{
    public partial class ProductEditForm : Form
    {
        public ProductEditForm()
        {
            InitializeComponent();
        }

        private FoxDataServiceClient CreateDataClient()
        {
            return new FoxDataServiceClient("/api/dataservice");
        }

        private async Task LoadProductsAsync()
        {
            using var client = CreateDataClient();
            var response = await client.ExecuteDataSetAsync(new FoxDataRequest("products.get_all"));
            ProductsGridView.DataSource = response.DataSet.Tables[0];
        }

        private async void ProductEditForm_Load(object sender, EventArgs e)
        {
            await LoadProductsAsync();
        }

        private async void RefreshButton_Click(object sender, EventArgs e)
        {
            await LoadProductsAsync();
        }

        private async void SaveButton_Click(object sender, EventArgs e)
        {
            DataTable? productDataTable = ProductsGridView.DataSource as DataTable;
            DataSet? changes = productDataTable?.DataSet?.GetChanges();
            if (changes == null)
            {
                MessageBox.Show(this, "No changes to save.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            FoxDataRequest request = new("products.get_all")
            {
                InsertQueryId = "products.insert",
                UpdateQueryId = "products.update",
                DeleteQueryId = "products.delete",
                DataSet = changes,
                SaveMode = FoxDataSaveModes.GroupedBatchUpdate,
                Transaction = FoxDataTransactions.Local
            };
            using var client = CreateDataClient();
            var response = await client.SaveDataTableAsync(request);
            ProductsGridView.DataSource = response.DataSet.Tables[0];
            MessageBox.Show(this, "Changes saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
