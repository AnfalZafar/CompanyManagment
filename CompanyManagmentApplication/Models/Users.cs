using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace CompanyManagmentApplication.Models
{
    public class Users
    {
        [Key]
        public int user_id { get; set; }
        [Required]
        public string user_name { get; set; }
        [Required]
        public string user_email { get; set; }
        [Required]
        public string user_password { get; set; }
        public string user_phone { get; set; }
        public string user_address { get; set; }
        public string user_img { get; set; }
        public int? role_id { get; set; }

        [ForeignKey("role_id")]
        public virtual Role Role { get; set; }
    }
}
