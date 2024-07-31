using System.ComponentModel.DataAnnotations.Schema;

namespace Customer_manager.Models
{
    public class Portfolyo
    {
        public int ID { get; set; }
        public string Category { get; set; }
        public string ProjectName { get; set; }
		[NotMapped]
		public IFormFile Photo { get; set; }
		public string PhotoPath { get; set; }
		public string Descripton { get; set; }
        public Customer_infos? CustomerInfo { get; set; }
        public int CustomerInfoId { get; set; }
    }
}
