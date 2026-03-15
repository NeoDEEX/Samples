using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using NeoDEEX.ServiceModel.Client.Data;
using NeoDEEX.ServiceModel.Data;
using NeoDEEX.Windows.Forms;
using System.Data;

namespace adv_client;

public partial class AdvProductEditForm : XtraForm, IFoxSupportProgress
{
    public AdvProductEditForm()
    {
        InitializeComponent();
    }

    #region IFoxSupportProgress 구현

    private FoxProgressDialog? _progressDialog;

    public IFoxProgressDialog GetProgressDialog()
    {
        if (_progressDialog == null)
        {
            _progressDialog = new();
            this.Controls.Add(_progressDialog);
        }
        return _progressDialog;
    }

    #endregion

    private static FoxDataServiceClient CreateDataClient()
    {
        return new FoxDataServiceClient("/api/dataservice");
    }

    // 데이터를 그리드에 바인딩한다.
    private void BindData(DataSet? ds)
    {
        DataView? dataView = ds?.Tables[0].DefaultView;
        // 삭제된 행도 표시하기 위해서 DataViewRowState.Deleted 플래그를 추가한다.
        dataView?.RowStateFilter |= DataViewRowState.Deleted;
        ProductsGrid.DataSource = dataView;
    }

    private async Task LoadProductsAsync()
    {
        FoxAsyncProxy proxy = new(this);
        DataSet? ds = await proxy.ExecuteAsync(() =>
        {
            using var client = CreateDataClient();
            var response = client.ExecuteDataSet(new FoxDataRequest("products.get_all"));
            return response.DataSet;
        });
        BindData(ds);
    }

    private async void AdvProductEditForm_Load(object sender, EventArgs e)
    {
        await LoadProductsAsync();
    }

    private async void RefreshButton_Click(object sender, EventArgs e)
    {
        await LoadProductsAsync();
    }

    private void DeleteButton_Click(object sender, EventArgs e)
    {
        DataRow? currentRow = ProductsGridView.GetFocusedDataRow();
        currentRow?.Delete();
        ProductsGridView.RefreshRow(ProductsGridView.FocusedRowHandle);
    }

    private async void SaveButton_Click(object sender, EventArgs e)
    {
        if (ProductsGrid.DataSource is not DataView dataView)
        {
            FoxMessageBox.Show(this, "No data available.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }
        DataSet? changes = dataView.Table?.DataSet?.GetChanges();
        if (changes == null || changes.Tables.Count == 0 || changes.Tables[0].Rows.Count == 0)
        {
            FoxMessageBox.Show(this, "No changes to save.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        FoxAsyncProxy proxy = new(this);
        FoxDataResponse? response = await proxy.ExecuteAsync(() =>
        {
            using var client = CreateDataClient();
            FoxDataRequest request = new("products.get_all")
            {
                InsertQueryId = "products.insert",
                UpdateQueryId = "products.update",
                DeleteQueryId = "products.delete",
                DataSet = changes,
                SaveMode = FoxDataSaveModes.GroupedBatchUpdate,
                Transaction = FoxDataTransactions.Local
            };
            var response = client.SaveDataTable(request);
            return response;
        });
        DataSet? ds = response!.DataSet;
        BindData(ds);
        string message = $"{response.AffectedRows} rows saved successfully.";
        FoxMessageBox.Show(this, message, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private void CloseButton_Click(object sender, EventArgs e)
    {
        this.Close();
    }

    #region Grid 컨트롤 이벤트 핸들러

    // 변경된(added, modified, deleted) 행에 대해서 그리드의 행 머리글 부분에 커스텀 이미지를 표시한다.
    private void ProductsGridView_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
    {
        if (e.Info.IsRowIndicator == false || sender is not DevExpress.XtraGrid.Views.Grid.GridView view)
        {
            return;
        }
        // 변경된(Modified) 행에 대해서만 커스텀 표시기를 표시한다.
        DataRow? dataRow = view.GetDataRow(e.RowHandle);
        if (dataRow == null)
        {
            return;
        }
        // 행의 RowState에 따라서 표시할 이미지를 선택한다.
        Image indicator;
        switch (dataRow.RowState)
        {
            case DataRowState.Added:
                indicator = GridImages.Images["Added"];
                break;
            case DataRowState.Deleted:
                indicator = GridImages.Images["Deleted"];
                break;
            case DataRowState.Modified:
                indicator = GridImages.Images["Modified"];
                break;
            default:
                return;
        }
        // 디폴트 그리기 수행 
        // (Handled 속성이 false인 경우에만 기본 그리기를 수행하고 Handled 속성을 true로 변경한다)
        e.DefaultDraw();
        // 변경된 행을 표시하는 이미지를 표시한다.
        Rectangle bounds = e.Bounds;
        bounds.Inflate(-1, -1);
        int x = bounds.X + (bounds.Width - indicator.Width) / 2;
        int y = bounds.Y + (bounds.Height - indicator.Height) / 2;
        e.Cache.DrawImage(indicator, new Rectangle(x, y, indicator.Width, indicator.Height));
    }

    private static Font? _DeletedRowFont = null;

    // 삭제된 행은 글자색을 회색으로 변경하고 취소선 스타일을 적용한다.
    private static void ChangeDeletedRowStyle(object sender, int rowHandle, DevExpress.Utils.AppearanceObject appearance)
    {
        GridView view = (GridView)sender;
        DataRow? dataRow = view.GetDataRow(rowHandle);
        if (dataRow == null)
        {
            return;
        }
        if (dataRow.RowState == DataRowState.Deleted)
        {
            appearance.ForeColor = Color.Gray;
            _DeletedRowFont ??= new Font(appearance.Font, FontStyle.Strikeout);
            appearance.Font = _DeletedRowFont;
            return;
        }
    }

    // Row Style 이벤트 핸들러
    private void ProductsGridView_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
    {
        ChangeDeletedRowStyle(sender, e.RowHandle, e.Appearance);
    }

    // Row Cell Style 이벤트 핸들러 (사용하지 않음)
    private void ProductsGridView_RowCellStyle(object sender, RowCellStyleEventArgs e)
    {
        ChangeDeletedRowStyle(sender, e.RowHandle, e.Appearance);
    }

    // 편집이 시작되기 전에 키 컬럼과 삭제된 행에 대해서 편집이 불가능하도록 한다.
    private void ProductsGridView_ShowingEditor(object sender, System.ComponentModel.CancelEventArgs e)
    {
        // 키 컬럼은 새로운 행 추가 상태에서만 편집이 가능해야 한다.
        if (sender is GridView view)
        {
            DataRow? dataRow = view.GetDataRow(view.FocusedRowHandle);
            if (dataRow == null)
            {
                return;
            }
            if (dataRow.RowState == DataRowState.Deleted
                || (view.FocusedColumn.FieldName == "product_id" && view.FocusedRowHandle != GridControl.NewItemRowHandle))
            {
                e.Cancel = true;
            }
        }
    }

    #endregion
}
