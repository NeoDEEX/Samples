using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using TheOne.Transactions;

namespace ServiceWebApp.Biz
{
    [FoxTransaction(FoxTransactionOption.Supported)]
    public class DacComp : FoxDacBase
    {
        public void DoDataJob()
        {
            //CREATE TABLE [dbo].[TestTempTable] (
            //    [Id]   INT           IDENTITY (1, 1) NOT NULL,
            //    [Desc] NVARCHAR (64) NULL,
            //    [CreateTime] DATETIME NULL DEFAULT getdate(), 
            //    PRIMARY KEY CLUSTERED ([Id] ASC)
            //);
            string sql = "INSERT INTO TestTempTable([Desc]) VALUES('Test Data')";
            this.DbAccess.ExecuteSqlNonQuery(sql);
        }

        public DataSet GetTestData()
        {
            return this.DbAccess.ExecuteSqlDataSet("SELECT * FROM TestTempTable");
        }

        public void ClearTestData()
        {
            this.DbAccess.ExecuteSqlNonQuery("DELETE FROM TestTempTable");
        }
    }
}