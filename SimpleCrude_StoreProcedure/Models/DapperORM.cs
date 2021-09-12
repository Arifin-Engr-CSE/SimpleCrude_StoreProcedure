using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleCrude_StoreProcedure.Models
{
    public static class DapperORM
    {
        private static string connectionString = @"Data Source=.;Initial Catalog=Sample_12; Integrated Security=True";

        public static void ExicuteWithoutReturn(string procedureName,DynamicParameters param=null)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                con.Execute(procedureName, param, commandType: CommandType.StoredProcedure);
            }
        }

        public static T ExicuteReturnScalar <T>(string procedureName, DynamicParameters param = null)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
              return (T) Convert.ChangeType( con.ExecuteScalar(procedureName, param, commandType: CommandType.StoredProcedure),typeof(T));
            }
        }

        public static IEnumerable<T> ReturnList <T>(string procedureName, DynamicParameters param = null)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
              return  con.Query<T>(procedureName, param, commandType: CommandType.StoredProcedure);
            }
        }


      

    }
}
