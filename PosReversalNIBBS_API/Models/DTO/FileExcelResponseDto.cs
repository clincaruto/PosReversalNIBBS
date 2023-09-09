namespace PosReversalNIBBS_API.Models.DTO
{
    public class FileExcelResponseDto
    {
        public Guid? UploadedExcelDetailBatchId { get; set; }
       // public Guid Id { get; set; }
        public string TERMINAL_ID { get; set; }
        public string MERCHANT_ID { get; set; }

        public double AMOUNT { get; set; }
        public string STAN { get; set; }
        public string RRN { get; set; }
        public string PAN { get; set; }
        public string TRANSACTION_DATE { get; set; }
        public string PROCESSOR { get; set; }
        public string BANK { get; set; }
        public string ACCOUNT_ID { get; set; }
        public string? IsReversed { get; set; }
        public string LOG_DRP { get; set; }

        
    }
}
