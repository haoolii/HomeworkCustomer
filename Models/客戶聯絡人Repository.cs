using System;
using System.Linq;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace HomeworkCustomer.Models
{
	public  class 客戶聯絡人Repository : EFRepository<客戶聯絡人>, I客戶聯絡人Repository
	{
		public new virtual IQueryable<客戶聯絡人> All()
		{
			return base.All().Where(p => p.IsDelete != true);
		}

		public new virtual IQueryable<客戶聯絡人> Where(Expression<Func<客戶聯絡人, bool>> expression)
		{
			return base.Where(expression).Where(p => p.IsDelete != true);
		}

		public new virtual void Delete(客戶聯絡人 entity)
		{
			var data = base.Where(p => p.Id == entity.Id).FirstOrDefault();
			data.IsDelete = true;
		}
	}

	public  interface I客戶聯絡人Repository : IRepository<客戶聯絡人>
	{

	}
}