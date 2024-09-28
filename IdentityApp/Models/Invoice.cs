namespace IdentityApp.Models
{
    public class Invoice
    {
        public int InvoiceId { get; set; }
        public double InvoiceAmount { get; set; }
        public string InvoiceMonth { get; set; }
        public string InvoiceOwner { get; set; }
        public string CreatorId { get; set; } // Id of the user who creates invoice
        public InvoiceStatus Status { get; set; }

    }
}

namespace IdentityApp.Models
{
    public enum InvoiceStatus
    {
        Submitted,
        Approved,
        Rejected
    }
}
