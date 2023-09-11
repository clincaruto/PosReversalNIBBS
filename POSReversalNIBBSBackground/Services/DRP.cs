using POSReversalNIBBSBackground.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.EntityFrameworkCore;
using POSReversalNIBBSBackground.Models;
using POSReversalNIBBSBackground.Data;
using POSReversalNIBBSBackground.Domain.Enums;

namespace POSReversalNIBBSBackground.Services
{
    public class DRP
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
                    var _excel = _dbContext.ExcelResponses.Where(x => x.IsReversed == "NO" & x.LOG_DRP == Domain.Enums.StatusEnum.Pending).AsNoTracking().ToList();
                    return _excel;
                }
                catch (Exception ex)
                {
                    throw new Exception("No Excel list");

                }
                //  return new List<ExcelResponse>();

            }

        }

        public async Task<ExcelResponse?> GetbyIdSend(Guid id)
        {
            return await _dbContext.ExcelResponses.FirstOrDefaultAsync(x => x.Id == id);
        }


        private void MarkRecordAsSent(DrpPayload record)
        {
            // Find and update the record in the database
          //  var dbRecord = _dbContext.ExcelResponses.FirstOrDefault(r => r.Id == record.clientRequestId);
            var dbRecord = GetbyIdSend;
            if (dbRecord != null)
            {
                //dbRecord.IsSentToApi = true; // Add an appropriate property to your model
                _dbContext.SaveChanges();
            }
        }

        public async Task UpdateRecordsAsSendAsync(ExcelResponse record)
        {
            var recordUpdate = await GetbyIdSend(record.Id);
            if (recordUpdate != null)
            {
                //record.ACCOUNT_ID = reader["ACCOUNT"].ToString();
                // recordUpdate.Log_drp
                recordUpdate.LOG_DRP = StatusEnum.SentToDRP;
                _dbContext.Update(recordUpdate);
                _dbContext.SaveChanges();

            }
        }

        public async Task SendToDRP()
        {
            var spoolFromDB = GetAll();
            try
            {
                spoolFromDB.ForEach(async x => {
                    var payload = new DrpPayload()
                    {
                        accountNumber=x.ACCOUNT_ID??"",
                        clientRequestId= x.clientRequestId,
                        logType=x.logType,
                        serviceType= x.serviceType,
                        terminalId = x.terminal_channel,
                        transactionAmount= x.AMOUNT.ToString(),
                        transactionDate=x.TRANSACTION_DATE,
                    };
                    //var payload = new DrpPayload()
                    //{
                    //    accountNumber = x.ACCOUNT_ID ?? "",
                    //    clientRequestId = "",
                    //    logType = "",
                    //    serviceType = "POS",
                    //    terminalId = x.TERMINAL_ID,
                    //    transactionAmount = x.AMOUNT.ToString(),
                    //    transactionDate = x.TRANSACTION_DATE,
                    //};
                    using (var httpClient = new HttpClient())
                {
                    var apiUrl = "http://10.100.12.38:8221/api/drp-channels/log-dispute";
                    httpClient.DefaultRequestHeaders.Add("DownStreamAuthorization", "Basic ZGlzcHV0ZUNoYW5uZWxzOmRpc3B1dGVjaGFubmVscw=="); // Replace with your actual access token or header value
                    httpClient.DefaultRequestHeaders.Add("Channels", "voiceBot");
                    // Serialize the records to JSON
                    var jsonContent = JsonSerializer.Serialize(payload);
                    // Create a StringContent object with JSON data
                    var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");


                    // Send a POST request to the API
                    var response = await httpClient.PostAsync(apiUrl, content);



                    // Check the response status and handle it accordingly

                    // response.EnsureSuccessStatusCode();
                    if (response.IsSuccessStatusCode)
                    {
                        // Handle success
                      await UpdateRecordsAsSendAsync(x);
                        Console.WriteLine("Report sent successfully");
                        


                        //var recordUpdate = await GetbyIdSend(x.Id);
                        //if (recordUpdate != null)
                        //{
                        //    _dbContext.Update(recordUpdate);
                        //    _dbContext.SaveChanges();
                        //}

                       


                    }
                    else
                    {
                        // Handle errors
                        var errorMessage = await response.Content.ReadAsStringAsync();
                        // You can log or further process the error message as needed.
                        Console.WriteLine($"API Error: {errorMessage}");
                    }
                }

                });
            }
            catch (Exception ex)
            {
                // Handle exceptions
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

    }
}
