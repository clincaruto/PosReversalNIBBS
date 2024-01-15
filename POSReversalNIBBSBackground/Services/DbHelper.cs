using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using POSReversalNIBBSBackground.Data;
using POSReversalNIBBSBackground.Domain;
using POSReversalNIBBSBackground.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace POSReversalNIBBSBackground.Services
{
    public class DbHelper
    {
        private PosNibbsDbContext _dbContext;

        private DbContextOptions<PosNibbsDbContext> GetAllOptions()
        {
            DbContextOptionsBuilder<PosNibbsDbContext> optionsBuilder = new DbContextOptionsBuilder<PosNibbsDbContext>();

            optionsBuilder.UseSqlServer(AppSettings.ConnectionString);

            return optionsBuilder.Options;
        }
        public  List<ExcelResponse> GetAll() 
        { 
            using(_dbContext= new PosNibbsDbContext(GetAllOptions()))
            {
                try
                {
                    var _excel = _dbContext.ExcelResponses.Where(x=>x.ACCOUNT_ID==null).AsNoTracking().ToList();
                    return _excel;
                }
                catch (Exception ex)
                {
                    throw new Exception("No Excel list"); 

                }
              //  return new List<ExcelResponse>();
                
            }

        }
        public string UpDownDate(string dbDateString, bool isUp = true)
        {
          //  string dbDateString = "2023-04-02T17:00:07.547";

            DateTime dbDate =isUp? DateTime.Parse(dbDateString).AddDays(5): DateTime.Parse(dbDateString).AddDays(-5);
          
            string newDateString = dbDate.ToString("yyyy-MM-dd HH:mm:ss.fff");
            return newDateString;
        }
        public void UpdateAllTheRecords( List<ExcelResponse> excelRecords) {
            ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;

            //  string connectionString = "Server=172.25.1.247,1554; Initial Catalog=postilion_office;User ID=pos_auto_revsl;Password=Auto@1234$;Encrypt=True;TrustServerCertificate=True;";
            //  string connectionString = "Server=localhost;Database=PosReversalNibbsDB;Trusted_Connection=True; multipleactiveresultsets=True; TrustServerCertificate=Yes";
           // string connectionString = "Server=10.100.13.159;Database=PosReversalNibbsDB;user id=PosReversaluser; password=Manager@123$;Encrypt=True;TrustServerCertificate=True";
            string connectionString = "Server=172.25.1.247,1554; Initial Catalog=postilion_office;User ID=posreversaluserpo;Password=Posreversal#userpo10;Encrypt=True;TrustServerCertificate=True;";

            foreach (var item in excelRecords)
            {
                try
                {
                    SqlConnection conn = new SqlConnection(connectionString);
                conn.Open();
                    Console.WriteLine("Post-office db connected");
                    string sqlQuery = $"select payee, structured_data_req, structured_data_rsp,datetime_req,message_type,pan,from_account_id as ACCOUNT, " +
                        " from_account_type ,(tran_amount_req/100) as transaction_amount, " +
                        "(settle_amount_req/100) settlement_amount,tran_currency_code,settle_currency_code,(retrieval_reference_nr), " +
                        "system_trace_audit_nr, tran_nr, " +
                        "terminal_id, card_acceptor_name_loc, " +
                        "tran_type,pos_terminal_type,source_node_name, sink_node_name, rsp_code_rsp,c.response_code_description, card_acceptor_id_code " +
                        "from post_tran a (nolock), post_tran_cust b (nolock), Def_Transaction_Response_Codes c " +
                        "where a.post_tran_cust_id = b.post_tran_cust_id and a.rsp_code_rsp = c.response_code " +
                        "and source_node_name not in ('ActiveSrc', 'KIMONOsrc') and " +
            // "datetime_req between '2023-03-01 00:00:00.000' and '2023-03-04 23:59:59.999' " +
            // "datetime_req between @downDate and @upDate " +
            //  " and tran_postilion_originated ='1'" +
                        "tran_postilion_originated ='1'" +
                        " and left(pan,6) in (@panLeft) and " +
                        "right(pan,4) in (@panRight) and " +
                        "terminal_id in (@terminalId) " +
                        "and retrieval_reference_nr in (@RRN) " +
                        "and tran_amount_req/100 in (@AMOUNT) " +
                        "and system_trace_audit_nr in (@STAN);";

                    //string sqlQuery = $" select ACCOUNT, pan, card_acceptor_id_code, terminal_id, system_trace_audit_nr, retrieval_reference_nr, transaction_amount " +
                    //    "from PosReversalNibbsDB.dbo.postilionDBs " +
                    //    "where card_acceptor_id_code in (@merchantId) and terminal_id in (@terminalId) and system_trace_audit_nr in (@STAN) " +
                    //    "and retrieval_reference_nr in (@RRN) and transaction_amount in (@AMOUNT) " +
                    //    "and left(pan,6) in (@panLeft) and right(pan,4) in (@panRight);";

                    // "where FORACID in (@ACCOUNT) and TRAN_AMT in (@AMOUNT);";

                    sqlQuery = sqlQuery.Replace("@merchantId", $"'{item.MERCHANT_ID}'");
                    sqlQuery = sqlQuery.Replace("@panLeft", $"'{ item.PAN.Substring(0, 6)}'");    
                    sqlQuery=sqlQuery.Replace("@panRight", $"'{ item.PAN.Substring(12, 4)}'");   
                    sqlQuery=sqlQuery.Replace("@terminalId", $"'{ item.TERMINAL_ID}'");   
                    sqlQuery=sqlQuery.Replace("@RRN", $"'{ item.RRN}'");
                    sqlQuery=sqlQuery.Replace("@AMOUNT", $"'{item.AMOUNT}'");
                    sqlQuery=sqlQuery.Replace("@STAN", $"'{ item.STAN}'");   
                   // sqlQuery=sqlQuery.Replace("@downDate", $"'{UpDownDate(item.TRANSACTION_DATE, false)}'");   
                   // sqlQuery=sqlQuery.Replace("@upDate", $"'{UpDownDate(item.TRANSACTION_DATE, true)}'");   

                   
                  Console.WriteLine(sqlQuery);

                   SqlCommand command = new SqlCommand(sqlQuery, conn);

                //SqlParameter parameter= new SqlParameter();
                //parameter.ParameterName = "@panLeft";
                //parameter.Value = item.PAN.Substring(0, 6);
                //command.Parameters.Add(parameter);
                //SqlParameter parameterRight = new SqlParameter();
                //parameterRight.ParameterName = "@panRight";
                //parameterRight.Value = item.PAN.Substring(12, 4);
                //command.Parameters.Add(parameterRight);
                //SqlParameter parameterTerminalId = new SqlParameter();
                //parameterTerminalId.ParameterName = "@terminalId";
                //parameterTerminalId.Value = item.TERMINAL_ID;
                //command.Parameters.Add(parameterTerminalId);
                //SqlParameter parameterRRN = new SqlParameter();
                //parameterRRN.ParameterName = "@RRN";
                //parameterRRN.Value = item.RRN;
                //command.Parameters.Add(parameterRRN);
                //SqlParameter parameterSTAN = new SqlParameter();
                //parameterSTAN.ParameterName = "@STAN";
                //parameterSTAN.Value = item.STAN;
                //command.Parameters.Add(parameterSTAN);
                //SqlParameter parameterDownDate = new SqlParameter();
                //parameterDownDate.ParameterName = "@downDate";
                //parameterDownDate.Value = UpDownDate(item.TRANSACTION_DATE, false);
                //command.Parameters.Add(parameterDownDate);

                //SqlParameter parameterUpDate = new SqlParameter();
                //parameterUpDate.ParameterName = "@upDate";
                //parameterUpDate.Value = UpDownDate(item.TRANSACTION_DATE, true);
                //command.Parameters.Add(parameterUpDate);
                SqlDataReader reader = command.ExecuteReader();


                while (reader.Read())
                {
                 

                     
                        using (_dbContext = new PosNibbsDbContext(GetAllOptions()))
                        {
                            try
                            {
                                var record = _dbContext.ExcelResponses.FirstOrDefault(x=>x.Id==item.Id);
                                if (record!=null)
                                {
                                    record.ACCOUNT_ID = reader["ACCOUNT"].ToString();
                                    _dbContext.Update(record);
                                    _dbContext.SaveChanges();

                                    Console.WriteLine("Update account");
                                }
                             
                            }
                            catch (Exception ex)
                            {
                                throw new Exception("No Excel list");

                            }
                        }

                   
                       
                   
                }


                }
                catch (Exception ex) {

                    continue;
                }


            }



        }
    
    }
}
