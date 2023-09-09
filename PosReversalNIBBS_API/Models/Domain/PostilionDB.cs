namespace PosReversalNIBBS_API.Models.Domain
{
    public class PostilionDB
    {
        public int Id { get; set; }
        public string ACCOUNT { get; set; }
        public string pan { get; set; }
        public string card_acceptor_id_code { get; set; }
        public string terminal_id { get; set; }
        public string system_trace_audit_nr { get; set; }
        public string retrieval_reference_nr { get; set; }
        public double transaction_amount { get; set; }
        public DateTime datetime_req { get; set; }

    }
}
