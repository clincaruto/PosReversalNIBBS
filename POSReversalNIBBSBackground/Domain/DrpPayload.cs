using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSReversalNIBBSBackground.Domain
{
    public class DrpPayload
    {
        public string accountNumber { get; set; }
        public string clientRequestId  { get; set; }
        public string logType { get; set; }
        public string  serviceType { get; set; }
        public string terminalId { get; set; }
        public string transactionAmount { get; set; }
        public string transactionDate { get; set; }
    }
}
