using System.ComponentModel.DataAnnotations;

namespace CompanyManagmentApplication.Models
{
    public class Role
    {
        [Key]
        public int role_id { get; set; }
        [Required]
        public string role_name { get; set; }
    }
}
