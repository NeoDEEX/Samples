using common;
using NeoDEEX.Data;
using Spectre.Console;

namespace traditional_local_tx;

internal class Program
{
    static void Main()
    {
        AnsiConsole.MarkupLine("[green]Traditional Local Transaction Test[/]");

        TestUtils.TestDB1 = "Oracle";
        TestUtils.TestDB2 = null;
        TestUtils.SetupTest<Program>();

        TestUtils.DumpTables("Before inserting test data");

        bool commit = false;
        //SimpleLocalTransaction(commit);
        //LegacyLocalTransaction(commit);
        InsertMany([997, 998, 999], commit);

        TestUtils.DumpTables($"After Local Transaction: ", commit ? "Commit" : "Rollback");
    }

    static void SimpleLocalTransaction(bool doCommit)
    {
        using FoxDbAccess dbAccess = FoxDbAccess.CreateDbAccess(TestUtils.TestDB1);
        dbAccess.Open();
        dbAccess.BeginTrans();
        try
        {
            dbAccess.ExecuteSqlNonQuery("INSERT INTO TxTestTable(PK, COL1, COL2) VALUES(998, 9, 'X')");
            dbAccess.ExecuteSqlNonQuery("INSERT INTO TxTestTable(PK, COL1, COL2) VALUES(999, 9, 'Y')");
            if (doCommit)
            {
                dbAccess.CommitTrans();
            }
            else
            {
                dbAccess.RollbackTrans();
            }
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]Error occurred: {ex.Message}[/]");
            dbAccess.RollbackTrans();
            return;
        }
        finally
        {
            dbAccess.Close();
        }
    }

    static void LegacyLocalTransaction(bool doCommit)
    {
        using FoxDbAccess dbAccess1 = FoxDbAccess.CreateDbAccess(TestUtils.TestDB1);
        using FoxDbAccess dbAccess2 = FoxDbAccess.CreateDbAccess(TestUtils.TestDB2);

        dbAccess1.Open();
        dbAccess2.Open();

        dbAccess1.BeginTrans();
        dbAccess2.BeginTrans();
        try
        {
            dbAccess1.ExecuteSqlNonQuery("INSERT INTO TxTestTable(PK, COL1, COL2) VALUES(998, 9, 'X')");
            dbAccess1.ExecuteSqlNonQuery("INSERT INTO TxTestTable(PK, COL1, COL2) VALUES(999, 9, 'Y')");
            dbAccess2.ExecuteSqlNonQuery("INSERT INTO TxTestTable(PK, COL1, COL2) VALUES(998, 9, 'X')");
            dbAccess2.ExecuteSqlNonQuery("INSERT INTO TxTestTable(PK, COL1, COL2) VALUES(999, 9, 'Y')");

            if (doCommit)
            {
                dbAccess1.CommitTrans();
                dbAccess2.CommitTrans();
            }
            else
            {
                dbAccess1.RollbackTrans();
                dbAccess2.RollbackTrans();
            }
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]Error occurred: {ex.Message}[/]");
            dbAccess1.RollbackTrans();
            dbAccess2.RollbackTrans();
            return;
        }
        finally
        {
            dbAccess1.Close();
            dbAccess2.Close();
        }
    }

    static void InsertMany(int[] ids, bool doCommit)
    {
        using FoxDbAccess dbAccess = FoxDbAccess.CreateDbAccess(TestUtils.TestDB1);
        dbAccess.Open();
        dbAccess.BeginTrans();
        try
        {
            foreach(int id in ids)
            {
                InsertData(dbAccess, id, id, "STR #" + id);
            }

            if (doCommit)
            {
                dbAccess.CommitTrans();
            }
            else
            {
                dbAccess.RollbackTrans();
            }
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]Error occurred: {ex.Message}[/]");
            dbAccess.RollbackTrans();
        }
        finally
        {
            dbAccess.Close();
        }
    }

    static void InsertData(FoxDbAccess dbAccess, int id, int col1, string col2)
    {
        bool ownsTransaction = false;
        bool ownsConnection = false;
        if (dbAccess.IsInLocalTransaction == false)
        {
            if (dbAccess.IsOpen == false)
            {
                dbAccess.Open();
                ownsConnection = true;
            }
            dbAccess.BeginTrans();
            ownsTransaction = true;
        }
        try
        {
            var parameters = dbAccess.CreateParamCollection();
            parameters.AddWithValue("pk", id);
            parameters.AddWithValue("col1", col1);
            parameters.AddWithValue("col2", col2);
            dbAccess.ExecuteSqlNonQuery("INSERT INTO TxTestTable(PK, COL1, COL2) VALUES(:PK, :COL1, :COL2)", parameters);
            if (ownsTransaction)
            {
                dbAccess.CommitTrans();
            }
        }
        catch
        {
            if (ownsTransaction)
            {
                dbAccess.RollbackTrans();
            }
            throw;
        }
        finally
        {
            if (ownsConnection)
            {
                dbAccess.Close();
            }
        }
    }

    class TxContext : IDisposable
    {
        private readonly FoxDbAccess _dbAccess;
        private readonly bool _ownsConnection;
        private readonly bool _ownsTransaction;
        private bool _isCompleted;

        public TxContext(FoxDbAccess dbAccess)
        {
            _dbAccess = dbAccess;
            if (_dbAccess.IsOpen == false)
            {
                _dbAccess.Open();
                _ownsConnection = true;
            }
            if (_dbAccess.IsInLocalTransaction == false)
            {
                _dbAccess.BeginTrans();
                _ownsTransaction = true;
            }
            _isCompleted = false;
        }

        public void SetComplete()
        {
            if (_ownsTransaction)
            {
                _isCompleted = true;
            }
        }

        public void Dispose()
        {
            if (_ownsTransaction)
            {
                if (_isCompleted)
                {
                    _dbAccess.CommitTrans();
                }
                else
                {
                    _dbAccess.RollbackTrans();
                }
            }
            if (_ownsConnection)
            {
                _dbAccess.Close();
            }
        }
    }
}
