using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Oracle.ManagedDataAccess.Client;
using POSReversalNIBBSBackground.Data;
using POSReversalNIBBSBackground.Domain;
using POSReversalNIBBSBackground.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace POSReversalNIBBSBackground.Services
{
    public class DbReversal
    {
        private PosNibbsDbContext _dbContext;

        private DbContextOptions<PosNibbsDbContext> GetAllOptions()
        {
            DbContextOptionsBuilder<PosNibbsDbContext> optionsBuilder = new DbContextOptionsBuilder<PosNibbsDbContext>();

            optionsBuilder.UseSqlServer(AppSettings.ConnectionString);

            return optionsBuilder.Options;
        }


        public List<ExcelResponse> GetAll()
        {
            using (_dbContext = new PosNibbsDbContext(GetAllOptions()))
            {
                try
                {
                    var _excel = _dbContext.ExcelResponses.Where(x => x.IsReversed == null).AsNoTracking().ToList();
                    return _excel;
                }
                catch (Exception ex)
                {
                    throw new Exception("No Excel list");

                    //}
                    //try
                    //{
                    //    var _excel = _dbContext.ExcelResponses.Where(x => x.IsReversed == null).AsNoTracking().ToList();
                    //    return _excel; // Return the retrieved data
                    //}
                    //catch (Exception ex)
                    //{
                    //    // Log the exception for debugging or error tracking
                    //   // _logger.LogError(ex, "An error occurred while fetching Excel data");

                    //    // You can perform any necessary cleanup here

                    //    // Return an empty list or null if appropriate for your application
                    //    return new List<ExcelResponse>(); // Or return null, depending on your requirements
                    //}
                 }
            }

        }

        public string FormatIsReversed(string s)
        {
            if (string.IsNullOrEmpty(s)) return "NA";
            if (s == "D") return "NO";
            if (s == "C") return "YES";
            return "NA";
        }


        public string UpDownDate(string dbDateString, bool isUp = true)
        {
            //  string dbDateString = "2023-04-02T17:00:07.547";

            DateTime dbDate = isUp ? DateTime.Parse(dbDateString).AddDays(5) : DateTime.Parse(dbDateString).AddDays(-5);

            string newDateString = dbDate.ToString("yyyy-MM-dd HH:mm:ss.fff");
            return newDateString;
        }


        public void UpdateAllTheRecords(List<ExcelResponse> excelRecords)
        {
            ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;

           // string connectionString = "Server=localhost;Database=PosReversalNibbsDB;Trusted_Connection=True; multipleactiveresultsets=True; TrustServerCertificate=Yes";
          //  string connectionString = "Server=10.100.13.159;Database=PosReversalNibbsDB;user id=PosReversaluser; password=Manager@123$;Encrypt=True;TrustServerCertificate=True";
            string connectionString = "DATA SOURCE=(DESCRIPTION=(ADDRESS = (PROTOCOL = TCP)(HOST = 10.100.20.12)(PORT = 1540))(CONNECT_DATA =(SERVER = DEDICATED)(SERVICE_NAME = UBANG))) ;PASSWORD=POSREVERSALUSERFI#1a; USER ID=posreversaluserfi;Min Pool Size=10;Connection Lifetime=120;Connection Timeout=800; Incr Pool Size=5;Decr Pool Size=2\" providerName=\"Oracle.DataAccess.Client\"";
            foreach (var item in excelRecords)
            {
                try
                {
                   // SqlConnection conn = new SqlConnection(connectionString);
                    OracleConnection conn = new OracleConnection(connectionString);
                    conn.Open();
                    Console.WriteLine("finnacle db connected");
                    string sqlQuery = $"select FORACID , D.* , g.foracid from tbaadm.htd d, tbaadm.gam g  where g.acid = d.acid  and  foracid in (@ACCOUNT) " +
                        "and tran_amt in (@AMOUNT);";
                    //string sqlQuery = $"select ID, FORACID, TRAN_DATE, PART_TRAN_TYPE, TRAN_AMT FROM PosReversalNibbsDB.dbo.finnacleDbs " +
                    //    "where FORACID in (@ACCOUNT) and TRAN_AMT in (@AMOUNT);";

                    // string sqlQuery = $"select payee, structured_data_req, structured_data_rsp,datetime_req,message_type,pan,from_account_id as ACCOUNT, " +
                    //" from_account_type ,(tran_amount_req/100) as transaction_amount, " +
                    //"(settle_amount_req/100) settlement_amount,tran_currency_code,settle_currency_code,(retrieval_reference_nr), " +
                    //"system_trace_audit_nr, tran_nr, " +
                    //"terminal_id, card_acceptor_name_loc, " +
                    //"tran_type,pos_terminal_type,source_node_name, sink_node_name, rsp_code_rsp,c.response_code_description, card_acceptor_id_code " +
                    //"from post_tran a (nolock), post_tran_cust b (nolock), Def_Transaction_Response_Codes c " +
                    //"where a.post_tran_cust_id = b.post_tran_cust_id and a.rsp_code_rsp = c.response_code " +
                    //"and source_node_name not in ('ActiveSrc', 'KIMONOsrc') and " +
                    //// "datetime_req between '2023-03-01 00:00:00.000' and '2023-03-04 23:59:59.999' " +
                    //// "datetime_req between @downDate and @upDate " +
                    ////  " and tran_postilion_originated ='1'" +
                    //"tran_postilion_originated ='1'" +
                    //" and left(pan,6) in (@panLeft) and " +
                    //"right(pan,4) in (@panRight) and " +
                    //"terminal_id in (@terminalId) " +
                    //"and retrieval_reference_nr in (@RRN) " +
                    //"and tran_amount_req in (@AMOUNT) " +
                    //"and system_trace_audit_nr in (@STAN);";

                    //sqlQuery = sqlQuery.Replace("@STAN", $"'{item.STAN}'");
                    sqlQuery = sqlQuery.Replace("@ACCOUNT", $"'{item.ACCOUNT_ID}'");
                    sqlQuery = sqlQuery.Replace("@AMOUNT", $"'{item.AMOUNT}'");
                    // sqlQuery=sqlQuery.Replace("@downDate", $"'{UpDownDate(item.TRANSACTION_DATE, false)}'");   
                    // sqlQuery=sqlQuery.Replace("@upDate", $"'{UpDownDate(item.TRANSACTION_DATE, true)}'");   



                    // SqlCommand command = new SqlCommand(sqlQuery, conn);


                    //  SqlDataReader reader = command.ExecuteReader();

                    Console.WriteLine(sqlQuery);

                    OracleCommand command = new OracleCommand(sqlQuery, conn);

                    OracleDataReader reader = command.ExecuteReader();


                    while (reader.Read())
                    {



                        using (_dbContext = new PosNibbsDbContext(GetAllOptions()))
                        {
                            try
                            {
                                var record = _dbContext.ExcelResponses.FirstOrDefault(x => x.Id == item.Id);
                                if (record != null)
                                {
                                    record.IsReversed = FormatIsReversed(reader["PART_TRAN_TYPE"].ToString());
                                    _dbContext.Update(record);
                                    _dbContext.SaveChanges();

                                    Console.WriteLine("Reversal in DB");
                                }
                               

                            }
                            catch (Exception ex)
                            {
                                throw new Exception("No Excel list");

                            }
                        }




                    }


                }
                catch (Exception ex)
                {

                    continue;
                }


            }



        }

    }
}
