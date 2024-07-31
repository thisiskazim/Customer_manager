namespace Customer_manager.Models;
using Microsoft.AspNetCore.Identity;


	public class Customers : IdentityUser
	{
		public string CustomerKey { get; set; } // Unique Key (user name, ID number, etc.)

		  public Customer_infos? CustomerInfo { get; set; }
	}

