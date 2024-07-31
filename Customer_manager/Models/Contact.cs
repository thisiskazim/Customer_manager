namespace Customer_manager.Models
{
    public class Contact
    {
        public int ID { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public Customer_infos? CustomerInfo { get; set; }
        public int CustomerInfoId { get; set; }
    }
}
