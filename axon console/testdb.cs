using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace axon_console
{
    class testDb
    {
        public void test()
        {
            using (SqlConnection conn = new SqlConnection())
            {

                //Server = tcp:xxx,1433; Initial Catalog = test; Persist Security Info = False; User ID = { your_username }; Password ={ your_password}; MultipleActiveResultSets = False; Encrypt = True; TrustServerCertificate = False; Connection Timeout = 30;

                //conn.ConnectionString = "Server=xxx,1433;Database=test; User ID=xxx;Password=xxx;Trusted_Connection=true";
                //conn.Open();
                
                conn.ConnectionString = "Server=tcp:xxx,1433;Initial Catalog = test; Persist Security Info = False; User ID=xxx;Password=xxx;MultipleActiveResultSets = False; Encrypt = True; TrustServerCertificate = False; Connection Timeout = 300";
                conn.Open();

                string sql = "select top 1 * from axon_temp_lbz_ambulant_match2 ";

                SqlCommand command = new SqlCommand(sql, conn);

                //SqlDataReader reader2 = command.ExecuteReader();
                int Kc = 0; int Vc0 = 0;int Vc = 0;int Vc1 = 0;
                string[] fieldNames = new string[99];
                string[] fieldTypes = new string[99];
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    // while there is another record present
                    while (reader.Read())
                    {


                        for (int f = 0; f < reader.FieldCount; f++)
                        {

                            fieldTypes[f] = reader.GetFieldType(f).Name;
                            fieldNames[f] = reader.GetName(f);

                            //if (fieldNames[f].Substring(1, 2) == "k_") Kc += 1; BlokLabels[Kc] = fieldNames[f];
                            //if (fieldNames[f].Substring(1, 3) == "v0_") Vc0 += 1; Var0Labels[Vc0] = fieldNames[f];
                            //if (fieldNames[f].Substring(1, 1) == "v_") Vc += 1; Var0Labels[Vc] = fieldNames[f];
                            //if (fieldNames[f].Substring(1, 3) == "v1_") Vc1 += 1; Var1Labels[Vc1] = fieldNames[f];
                            string val = reader.GetString(f);
                        }

                    }
                }
                // using the code here...
            }
        }
    }
}
