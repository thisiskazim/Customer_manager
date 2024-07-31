namespace Customer_manager.Models
{
    public class Skills
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public int Value { get; set; }
        public Customer_infos? CustomerInfo { get; set; }
        public int CustomerInfoId { get; set; }
    }
}
