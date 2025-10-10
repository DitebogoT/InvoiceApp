namespace InvoiceApp.Models
{
    public class InvoiceItem
    {
        public int Unit {  get; set; }
        public int Total { get; internal set; }
    }
}
