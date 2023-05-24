namespace PosReversalNIBBS_API.Models.DTO
{
	//this is used so that the user do not add ID, Id will be automatically generated
	public class AddExcelResponseVM
	{
		public string Terminal_Id { get; set; }
		public string Merchant_Id { get; set; }
		public int Processing_Code { get; set; }
		public int Bin { get; set; }
		public int Pan { get; set; }
		public int Response_code { get; set; }
		public int Amount { get; set; }
		public int System_Trace_Number { get; set; }
		public int Retrieval_Ref_Number { get; set; }
		public string IssuingBankName { get; set; }
		public DateTime Transaction_Date { get; set; }
		public int Original_Data_Element { get; set; }
		public string Processor_Name { get; set; }
	}
}
