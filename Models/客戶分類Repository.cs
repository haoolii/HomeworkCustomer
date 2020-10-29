using System;
using System.Linq;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace HomeworkCustomer.Models
{
	public  class 客戶分類Repository : EFRepository<客戶分類>, I客戶分類Repository
	{
		public new virtual IQueryable<客戶分類> All()
		{
			return base.All().Where(p => p.IsDelete != true);
		}

		public new virtual IQueryable<客戶分類> Where(Expression<Func<客戶分類, bool>> expression)
		{
			return base.Where(expression).Where(p => p.IsDelete != true);
		}

		public new virtual void Delete(客戶分類 entity)
		{
			var data = base.Where(p => p.Id == entity.Id).FirstOrDefault();
			data.IsDelete = true;
		}
	}

	public  interface I客戶分類Repository : IRepository<客戶分類>
	{

	}
}