using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CompanyManagmentApplication.Models
{
    public class Products
    {
        [Key]
        public int product_id { get; set; }
        public string product_name { get; set; }
        public string product_description {get; set;}
        public string product_price { get; set;}
        public string product_verify {  get; set;}
        public int? user_id { get; set;}
        [ForeignKey("user_id")]
        public virtual Users Users { get; set; }

    }


}
