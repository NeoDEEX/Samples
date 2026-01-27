using common;
using NeoDEEX.Data;
using Spectre.Console;
using System.Transactions;

namespace traditional_dist_tx;

internal class Program
{
    static void Main()
    {
        AnsiConsole.MarkupLine("[green]Traditional Distributed Transaction Test[/]");
        // 분산 트랜잭션 프로모션이 작동하기 위해 필요한 설정
        // 주) Npgsql 은 이 설정이 없어도 프로모션이 작동하지만 Oralce, SQL Server 등은 이 설정이 필요하다.
        TransactionManager.ImplicitDistributedTransactions = true;

        TestUtils.TestDB1 = "Oracle";
        TestUtils.TestDB2 = "SqlServer";
        TestUtils.SetupTest<Program>();

        TestUtils.DumpTables("Before Distributed Transaction: ");

        bool commit = false;
        //LegacyDistributedTransaction(commit);

        TestUtils.TestDB1 = "SqlServerNoPooling";
        InsertMany([997, 998, 999], commit);

        TestUtils.DumpTables($"After Distributed Transaction: ", commit ? "Commit" : "Rollback");
    }

    static void LegacyDistributedTransaction(bool doCommit)
    {
        using FoxDbAccess dbAccess1 = FoxDbAccess.CreateDbAccess(TestUtils.TestDB1);
        using FoxDbAccess dbAccess2 = FoxDbAccess.CreateDbAccess(TestUtils.TestDB2);
        using TransactionScope scope = new(TransactionScopeOption.Required);

        // 로컬 트랜잭션 시작
        dbAccess1.Open();
        dbAccess1.ExecuteSqlNonQuery("INSERT INTO TxTestTable(PK, COL1, COL2) VALUES(998, 9, 'X')");
        dbAccess1.ExecuteSqlNonQuery("INSERT INTO TxTestTable(PK, COL1, COL2) VALUES(999, 9, 'Y')");
        dbAccess1.Close();

        // Transaction promotion 발생(분산 트랜잭션 시작)
        dbAccess2.Open();
        dbAccess2.ExecuteSqlNonQuery("INSERT INTO TxTestTable(PK, COL1, COL2) VALUES(998, 9, 'X')");
        dbAccess2.ExecuteSqlNonQuery("INSERT INTO TxTestTable(PK, COL1, COL2) VALUES(999, 9, 'Y')");
        dbAccess2.Close();

        if (doCommit)
        {
            scope.Complete();
        }
        // TransactionScope.Dispose() 시점에 커밋 또는 롤백이 수행된다.
    }

    static void InsertMany(int[] ids, bool doCommit)
    {
        using TransactionScope scope = new(TransactionScopeOption.Required);

        foreach(int id in ids)
        {
            AnsiConsole.MarkupLine($"[gray]Inserting data #{id}[/]");
            InsertData(id, id, "STR #" + id);
        }

        if (doCommit)
        {
            scope.Complete();
        }
        // TransactionScope.Dispose() 시점에 커밋 또는 롤백이 수행된다.
    }

    static void InsertData(int id, int col1, string col2)
    {
        using FoxDbAccess dbAccess = FoxDbAccess.CreateDbAccess(TestUtils.TestDB1);
        var parameters = dbAccess.CreateParamCollection();
        parameters.AddWithValue("pk", id);
        parameters.AddWithValue("col1", col1);
        parameters.AddWithValue("col2", col2);
        dbAccess.ExecuteSqlNonQuery("INSERT INTO TxTestTable(PK, COL1, COL2) VALUES(@PK, @COL1, @COL2)", parameters);
    }
}
