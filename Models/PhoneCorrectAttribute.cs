using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace HomeworkCustomer.Models
{
    public class PhoneCorrectAttribute : DataTypeAttribute
    {
        public PhoneCorrectAttribute(): base(DataType.PhoneNumber)
        {
            ErrorMessage = "請輸入正確手機格式";
        }

        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return true;
            }

            Regex regex = new Regex(@"\d{4}-\d{6}");

            return regex.IsMatch(value.ToString());
        }
    }
}