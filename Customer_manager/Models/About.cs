using System.ComponentModel.DataAnnotations.Schema;

namespace Customer_manager.Models
{
    public class About
    {
        public int ID { get; set; }
        public string Description { get; set; }
        [NotMapped]
		public IFormFile Photo { get; set; }
		public string PhotoPath { get; set; }
		public int CustomerInfoId { get; set; }
        public Customer_infos? CustomerInfo { get; set; }
    }
}
