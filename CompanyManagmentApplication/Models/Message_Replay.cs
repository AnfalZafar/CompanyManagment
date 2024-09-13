using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CompanyManagmentApplication.Models
{
    public class Message_Replay
    {
        [Key]
        public int Id { get; set; } 
        public string Replay_text { get; set; }
        public int? message_id { get; set; }
        public int user_id { get; set; }

        [ForeignKey("message_id")]
        public virtual Messages Messages { get; set; }
    }
}
