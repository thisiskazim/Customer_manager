namespace Customer_manager.Models
{
    public class Ozgecmis
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Title1 { get; set; }
        public int? YearStart { get; set; }
        public int? YearFinish { get; set; }
        public string Description { get; set; }
        public Customer_infos? CustomerInfo { get; set; }
        public int CustomerInfoId { get; set; }

    }
}
