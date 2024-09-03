﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CompanyManagmentApplication.Models
{
    public class Messages
    {
        [Key]
        public int message_id { get; set; }
        public string message_object { get; set; }
        public int? user_id { get; set; }

        [ForeignKey("user_id")]
        public virtual Users Users { get; set; }
    }
}
