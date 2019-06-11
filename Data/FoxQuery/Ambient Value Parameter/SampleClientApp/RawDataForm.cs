using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using System;
using System.Drawing;
using TheOne.ServiceModel;

namespace SampleClientApp
{
    public partial class RawDataForm : XtraForm
    {
        public RawDataForm()
        {
            InitializeComponent();
        }

        private async void RawDataForm_Load(object sender, EventArgs e)
        {
            // Product 테이블 전체 컬럼을 조회해 온다.
            using (var client = FoxClientFactory.CreateDataClient("api/dataservice"))
            {
                var response = await client.ExecuteDataSetAsync("Common.GetRawData");
                grdProducts.DataSource = response.DataSet.Tables[0];
            }
            // ModifiedBy 컬럼 값이 TestUser로 시작하는 경우 색상, 폰트 변경
            GridFormatRule formatRule = new GridFormatRule();
            var rule = new FormatConditionRuleExpression();
            rule.Expression = "StartsWith([ModifiedBy], \'TestUser\')";
            rule.Appearance.ForeColor = Color.White;
            rule.Appearance.BackColor = Color.DarkOrange;
            rule.Appearance.Font = new Font(rule.Appearance.Font, FontStyle.Bold);
            formatRule.Rule = rule;
            formatRule.ColumnApplyTo = formatRule.Column = grvProducts.Columns["ModifiedBy"];
            grvProducts.FormatRules.Add(formatRule);
            // MofifiedAt 컬럼 날짜 포맷 변경
            grvProducts.Columns["ModifiedAt"].DisplayFormat.FormatString = "yyyy-MM-dd HH:mm:ss";
            // 컬럼 폭 조정
            grvProducts.BestFitColumns();
        }
    }
}
