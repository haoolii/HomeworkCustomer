namespace HomeworkCustomer.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    [MetadataType(typeof(Customer_Test_ViewMetaData))]
    public partial class Customer_Test_View
    {
    }
    
    public partial class Customer_Test_ViewMetaData
    {
        [Required]
        public int Id { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        [Required]
        public string 客戶名稱 { get; set; }
        public Nullable<int> 客戶銀行數 { get; set; }
        public Nullable<int> 客戶聯絡人數 { get; set; }
    }
}
