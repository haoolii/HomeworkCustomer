using System;
using System.Linq;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace HomeworkCustomer.Models
{
	public  class 客戶銀行資訊Repository : EFRepository<客戶銀行資訊>, I客戶銀行資訊Repository
	{
		public new virtual IQueryable<客戶銀行資訊> All()
		{
			return base.All().Where(p => p.IsDelete != true);
		}

		public new virtual IQueryable<客戶銀行資訊> Where(Expression<Func<客戶銀行資訊, bool>> expression)
		{
			return base.Where(expression).Where(p => p.IsDelete != true);
		}

		public new virtual void Delete(客戶銀行資訊 entity)
		{
			var data = base.Where(p => p.Id == entity.Id).FirstOrDefault();
			data.IsDelete = true;
		}
	}

	public  interface I客戶銀行資訊Repository : IRepository<客戶銀行資訊>
	{

	}
}