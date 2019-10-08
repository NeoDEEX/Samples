using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataServiceClient
{
    public partial class DbProfileViewPopup : FormBase
    {
        private readonly Regex RegexNewLine = new Regex(@"\r\n?|\n");

        public DbProfileViewPopup()
        {
            InitializeComponent();
        }

        public DataRow DataRow { get; internal set; }

        private string GetTextField(string columnName)
        {
            return RegexNewLine.Replace(this.DataRow.Field<string>(columnName), "\r\n");
        }

        private void DbProfileViewPopup_Load(object sender, EventArgs e)
        {
            txtLogId.Text = this.DataRow.Field<string>("LogId");
            txtTimestamp.Text = this.DataRow.Field<DateTime>("LogTimestamp").ToString("yyyy-MM-dd hh:mm:ss.fff");
            txtUserId.Text = this.DataRow.Field<string>("UserId");
            txtExecutionType.Text = this.DataRow.Field<string>("ExecutionType");
            txtExecutionTime.Text = this.DataRow.Field<decimal>("ExecutionTime").ToString("N3");
            txtFoxQueryId.Text = this.DataRow.Field<string>("FoxQueryId");
            memQueryText.Text = GetTextField("QueryText");
            memParameters.Text = GetTextField("QueryParameters");
            memInlineQuery.Text = GetTextField("InlineQuery");
            txtResultString.Text = this.DataRow.Field<string>("ResultString");
            txtCallerName.Text = this.DataRow.Field<string>("CallerName");
            txtErrorCode.Text = this.DataRow.Field<int?>("ErrorCode").ToString();
            txtExceptionType.Text = this.DataRow.Field<string>("ExceptionType");
            memErrorMessage.Text = this.DataRow.Field<string>("ErrorMessage");
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
