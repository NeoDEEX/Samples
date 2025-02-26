using System;
using System.Data;
using TheOne.Data;
using TheOne.Data.Npgsql;
using Npgsql;
using NpgsqlTypes;

namespace FuncInvokeTest
{
    internal static class Program
    {
        static void Main(string[] args)
        {
            //SimpleSql();
            //SimpleInvokeFunc();
            //SimpleInvokeFuncAndFetch();
            //AdvancedInvokeFuncAndFetch();
            //AdvancedInvokeFuncAndFetchWithTx();
            //InvokeFuncUsingNeoDEEX();
            InvokeFuncUsingNeoDEEX_ErrTest();
            //BatchedInvokeFunc();
            //BatchedInvokeFunc2();
            //BatchedInvokeFunc3();
        }

        static void Dump(this DataTable dt)
        {
            foreach (DataRow dr in dt.Rows)
            {
                foreach (DataColumn col in dt.Columns)
                {
                    Console.Write($"{dr[col.ColumnName]}\t");
                }
                Console.WriteLine();
            }
        }

        // 단순 쿼리
        static void SimpleSql()
        {
            FoxDbAccess dbAccess = FoxDatabaseFactory.CreateDatabase();
            string query = "SELECT product_id, product_name FROM products WHERE product_id < 5";
            DataSet ds = dbAccess.ExecuteSqlDataSet(query);
            ds.Tables[0].Dump();
        }

        // 단순 함수 호출
        static void SimpleInvokeFunc()
        {
            FoxDbAccess dbAccess = FoxDatabaseFactory.CreateDatabase();
            string funcName = "myfunc";
            DataSet ds = dbAccess.ExecuteSpDataSet(funcName);
            // refcur 이름이 반환된다.
            ds.Tables[0].Dump();
        }

        // 함수 호출 및 fetch 수행 (오류 발생)
        // DB 연결 2회 수행 => 좋지 못함
        static void SimpleInvokeFuncAndFetch()
        {
            FoxDbAccess dbAccess = FoxDatabaseFactory.CreateDatabase();
            string funcName = "myfunc";
            DataSet ds = dbAccess.ExecuteSpDataSet(funcName);
            // FETCH 수행
            string refcurName = (string)ds.Tables[0].Rows[0][0];
            string fetchQuery = $"FETCH ALL IN \"{refcurName}\"";
            DataSet ds2 = dbAccess.ExecuteSqlDataSet(fetchQuery);
            ds2.Tables[0].Dump();
        }

        // 향상된 함수 호출 및 fetch 수행 (오류 발생)
        // DB 연결 1회 내에서 함수 호출과 fetch 수행을 하지만 여전히 오류 발생
        static void AdvancedInvokeFuncAndFetch()
        {
            FoxDbAccess dbAccess = FoxDatabaseFactory.CreateDatabase();
            string funcName = "myfunc";
            dbAccess.Open();
            try
            {
                DataSet ds = dbAccess.ExecuteSpDataSet(funcName);
                // FETCH 수행
                string refcurName = (string)ds.Tables[0].Rows[0][0];
                string fetchQuery = $"FETCH ALL IN \"{refcurName}\"";
                DataSet ds2 = dbAccess.ExecuteSqlDataSet(fetchQuery);
                ds2.Tables[0].Dump();
            }
            finally
            {
                dbAccess.Close();
            }
        }

        // 트랜잭션 내에서 함수 호출 및 fetch 수행 (정상 작동)
        // DB 연결 후 트랜잭션을 시작하고 함수 호출과 fetch 수행.
        static void AdvancedInvokeFuncAndFetchWithTx()
        {
            FoxDbAccess dbAccess = FoxDatabaseFactory.CreateDatabase();
            string funcName = "myfunc";
            dbAccess.Open();
            dbAccess.BeginTrans();
            try
            {
                DataSet ds = dbAccess.ExecuteSpDataSet(funcName);
                // FETCH 수행
                string refcurName = (string)ds.Tables[0].Rows[0][0];
                string fetchQuery = $"FETCH ALL IN \"{refcurName}\"";
                DataSet ds2 = dbAccess.ExecuteSqlDataSet(fetchQuery);
                ds2.Tables[0].Dump();
                dbAccess.CommitTrans();
            }
            catch
            {
                dbAccess.RollbackTrans();
                throw;
            }
            finally
            {
                dbAccess.Close();
            }
        }

        // NeoDEEX 확장 API 사용
        static void InvokeFuncUsingNeoDEEX()
        {
            FoxNpgsqlDbAccess dbAccess = (FoxNpgsqlDbAccess)FoxDatabaseFactory.CreateDatabase();
            string funcName = "myfunc";
            // ExecuteSpDataSet3 메서드는 결과셋에서 커서 이름을 추출하고 FETCH 를 수행한다.
            // 이 과정에 필요하다면 Open/BeginTransaction/Commit/Rollback/Close 를 모두 수행한다.
            DataSet ds = dbAccess.ExecuteSpDataSet3(funcName);
            ds.Tables[0].Dump();
        }

        // NeoDEEX 확장 API 사용
        static void InvokeFuncUsingNeoDEEX_ErrTest()
        {
            FoxNpgsqlDbAccess dbAccess = (FoxNpgsqlDbAccess)FoxDatabaseFactory.CreateDatabase();
            string funcName = "myfunc_err";
            dbAccess.Open();
            dbAccess.BeginTrans();
            try
            {
                DataSet ds1 = dbAccess.ExecuteSpDataSet3(funcName);
                ds1.Tables[0].Dump();
                DataSet ds2 = dbAccess.ExecuteSpDataSet3(funcName);
                ds2.Tables[0].Dump();
            }
            catch
            {
                dbAccess.RollbackTrans();
                throw;
            }
            finally
            {
                dbAccess.Close();
            }
        }

        // 함수 호출과 fetch를 배치로 수행
        static void BatchedInvokeFunc()
        {
            FoxDbAccess dbAccess = FoxDatabaseFactory.CreateDatabase();
            string query = "SELECT myfunc(); FETCH ALL IN \"nlis_refcur1\"";
            DataSet ds = dbAccess.ExecuteSqlDataSet(query);
            // 첫 번째 테이블에는 SELECT myfunc(); 호출 결과(커서 이름)가 담겨 있다.
            ds.Tables.RemoveAt(0);
            // 두 번째 테이블에 실제 함수 호출 결과가 담긴다.
            ds.Tables[0].Dump();
        }

        // 여러 결과셋을 반환하는 함수 호출과 fetch를 배치로 수행
        static void BatchedInvokeFunc2()
        {
            FoxDbAccess dbAccess = FoxDatabaseFactory.CreateDatabase();
            string query = "SELECT myfunc2(); FETCH ALL IN \"nlis_refcur1\";FETCH ALL IN \"nlis_refcur2\";";
            DataSet ds = dbAccess.ExecuteSqlDataSet(query);
            // 첫 번째 테이블에는 SELECT myfunc(); 호출 결과(커서 이름)가 담겨 있다.
            ds.Tables.RemoveAt(0);
            // 나머지 테이블에 실제 함수 호출 결과가 담긴다.
            ds.Tables[0].Dump();
            ds.Tables[1].Dump();
        }

        // 매개변수를 갖고 결과셋을 반환하는 함수 호출과 fetch를 배치로 수행
        static void BatchedInvokeFunc3()
        {
            FoxDbAccess dbAccess = FoxDatabaseFactory.CreateDatabase();
            string query = "SELECT myfunc3(p_from => @p_from, p_to => @p_to); FETCH ALL IN \"nlis_refcur1\"";
            FoxNpgsqlParameterCollection parameters = new FoxNpgsqlParameterCollection();
            parameters.AddWithValue("p_from", 1);
            parameters.AddWithValue("p_to", 5);
            DataSet ds = dbAccess.ExecuteSqlDataSet(query, parameters);
            // 첫 번째 테이블에는 SELECT myfunc(); 호출 결과(커서 이름)가 담겨 있다.
            ds.Tables.RemoveAt(0);
            // 나머지 테이블에 실제 함수 호출 결과가 담긴다.
            ds.Tables[0].Dump();
        }

    }
}
