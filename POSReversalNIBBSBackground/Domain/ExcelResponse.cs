using POSReversalNIBBSBackground.Domain.Enums;

namespace POSReversalNIBBSBackground.Domain
{
    public class ExcelResponse
    {

        public Guid Id { get; set; }
        public string TERMINAL_ID { get; set; }
        public string MERCHANT_ID { get; set; }

        public double AMOUNT { get; set; }
        public string STAN { get; set; }
        public string RRN { get; set; }
        public string PAN { get; set; }
        public string TRANSACTION_DATE { get; set; }
        public string PROCESSOR { get; set; }
        public string BANK { get; set; }
        public string? ACCOUNT_ID { get; set; }
        public StatusEnum LOG_DRP { get; set; }

        public string? IsReversed { get; set; }

        public Guid? UploadedExcelDetailBatchId { get; set; }

        // Navigation properties
        public UploadedExcelDetail uploadedExcelDetail { get; set; }

        // from payload
        public string clientRequestId { get; set; } = "987654";
        public string logType { get; set; } = "Regular";
        public string serviceType { get; set; } = "POS";
        public string terminal_channel { get; set; } = "WEB";
    }
}
