using System;
using System.Linq;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace HomeworkCustomer.Models
{
	public  class 客戶資料Repository : EFRepository<客戶資料>, I客戶資料Repository
	{
		public new virtual IQueryable<客戶資料> All()
		{
			return base.All().Where(p => p.IsDelete != true);
		}

		public new virtual IQueryable<客戶資料> Where(Expression<Func<客戶資料, bool>> expression)
		{
			return base.Where(expression).Where(p => p.IsDelete != true);
		}

		public new virtual void Delete(客戶資料 entity)
		{
			var data = base.Where(p => p.Id == entity.Id).FirstOrDefault();
			data.IsDelete = true;
		}
	}

	public  interface I客戶資料Repository : IRepository<客戶資料>
	{

	}
}