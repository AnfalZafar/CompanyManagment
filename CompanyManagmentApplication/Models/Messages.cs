using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CompanyManagmentApplication.Models
{
    public class Messages
    {
        [Key]
        public int message_id { get; set; }
        public string message_object { get; set; }
        public string from_email { get; set; }
        public string to_email { get; set; }
      
    }
}
