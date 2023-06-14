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
        public  List<ExcelResponse> GetAll() { 
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
            }

        }
        public string UpDownDate(string dbDateString, bool isUp = true)
        {
          //  string dbDateString = "2023-04-02T17:00:07.547";

            DateTime dbDate =isUp? DateTime.Parse(dbDateString).AddDays(2): DateTime.Parse(dbDateString).AddDays(-2);
            DateTime newDate = new DateTime(dbDate.Year, dbDate.Month, 1, 0, 0, 0, 0);

            string newDateString = newDate.ToString("yyyy-MM-dd HH:mm:ss.fff");
            return newDateString;
        }
        public void UpdateAllTheRecords( List<ExcelResponse> excelRecords) {
            ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true; 

            string connectionString = "Server=172.25.1.247,1554; Initial Catalog=position_office;  User ID=pos_auto_revsl;Password=Auto@1234$;Encrypt=True;TrustServerCertificate=True;";

            foreach (var item in excelRecords)
            {
                try
                {
                    SqlConnection conn = new SqlConnection(connectionString);
                conn.Open();
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
                   "datetime_req between @downDate and @upDate " +
                    " and tran_postilion_originated ='1'" +
                    " and left(pan,6) in (@panLeft) and " +
                    "right(pan,4) in (@panRight) and " +
                    "terminal_id in (@terminalId) " +
                    "and retrieval_reference_nr in (@RRN) " +
                    "and system_trace_audit_nr in (@STAN);";

                SqlCommand command = new SqlCommand(sqlQuery, conn);

                SqlParameter parameter= new SqlParameter();
                parameter.ParameterName = "@panLeft";
                parameter.Value = item.PAN.Substring(0, 6);
                command.Parameters.Add(parameter);
                SqlParameter parameterRight = new SqlParameter();
                parameterRight.ParameterName = "@panLeft";
                parameterRight.Value = item.PAN.Substring(12, 4);
                command.Parameters.Add(parameterRight);
                SqlParameter parameterTerminalId = new SqlParameter();
                parameterTerminalId.ParameterName = "@terminalId";
                parameterTerminalId.Value = item.TERMINAL_ID;
                command.Parameters.Add(parameterTerminalId);
                SqlParameter parameterRRN = new SqlParameter();
                parameterRRN.ParameterName = "@RRN";
                parameterRRN.Value = item.RRN;
                command.Parameters.Add(parameterRRN);
                SqlParameter parameterSTAN = new SqlParameter();
                parameterSTAN.ParameterName = "@STAN";
                parameterSTAN.Value = item.STAN;
                command.Parameters.Add(parameterSTAN);
                SqlParameter parameterDownDate = new SqlParameter();
                parameterDownDate.ParameterName = "@downDate";
                parameterDownDate.Value = UpDownDate(item.TRANSACTION_DATE, false);
                command.Parameters.Add(parameterDownDate);

                SqlParameter parameterUpDate = new SqlParameter();
                parameterUpDate.ParameterName = "@upDate";
                parameterUpDate.Value = UpDownDate(item.TRANSACTION_DATE, true);
                command.Parameters.Add(parameterUpDate);
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
