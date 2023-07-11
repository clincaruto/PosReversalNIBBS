namespace PosReversalNIBBS_API.Models.DTO
{
	//this is used so that the user do not add ID, Id will be automatically generated
	public class AddExcelResponseVM
	{
        public string TERMINAL_ID { get; set; }
        public string MERCHANT_ID { get; set; }

        public double AMOUNT { get; set; }
        public string STAN { get; set; }
        public string RRN { get; set; }
        public string PAN { get; set; }
        public string TRANSACTION_DATE { get; set; }
        public string PROCESSOR { get; set; }
        public string BANK { get; set; }
        public Guid BatchId { get; set; }
        
    }
}
