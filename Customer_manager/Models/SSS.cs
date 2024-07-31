namespace Customer_manager.Models
{
    public class SSS
    {
        public int ID { get; set; }
        public string Soru { get; set; }
        public string Cevap { get; set; }
        public Customer_infos? CustomerInfo { get; set; }
        public int CustomerInfoId { get; set; }
    }
}
