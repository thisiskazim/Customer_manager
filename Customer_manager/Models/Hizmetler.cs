namespace Customer_manager.Models
{
    public class Hizmetler
    {
        public int ID { get; set; }
        public string Icon { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Customer_infos? CustomerInfo { get; set; }
        public int CustomerInfoId { get; set; }
    }
}
