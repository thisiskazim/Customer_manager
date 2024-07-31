namespace Customer_manager.Models
{
	public class Customer_infos
	{
	
			public int CustomerInfoId { get; set; } // Primary Key
			public string CustomerKey { get; set; } // Foreign Key

			public Customers Customer { get; set; }
     
            public ICollection<Contact> Contact { get; set; }
            public ICollection<About> About { get; set; }
            public ICollection<Hizmetler> Hizmetler { get; set; }
            public ICollection<Ozgecmis> Ozgecmis { get; set; }
            public ICollection<Portfolyo> Portfolyo { get; set; }
            public ICollection<Skills> Skills { get; set; }
            public ICollection<SSS> SSS { get; set; }

    }
}


// ICollection<T> kullanarak ilişkileri tanımlamak, veritabanı tarafında doğru ilişkisel yapıların oluşturulmasına yardımcı olur.