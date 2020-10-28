using System;
using System.Linq;
using System.Collections.Generic;

namespace HomeworkCustomer.Models
{
	public  class Customer_Test_ViewRepository : EFRepository<Customer_Test_View>, ICustomer_Test_ViewRepository
	{

	}

	public  interface ICustomer_Test_ViewRepository : IRepository<Customer_Test_View>
	{

	}
}