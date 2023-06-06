using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MessengerServer.Models
{
   public class Message
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public DateTime CreateDate { get; set; }

        [Required]
        public Guid SenderId { get; set; }
        public User Sender { get; set; }

        [Required]
        public Guid ChatId { get; set; }
        public Chat Chat { get; set; }

        [Required]
        public bool IsChecked { get; set; }

        [Required]
        public bool IsDelivered { get; set; }

        [Required]
        public bool IsForwarded { get; set; }
    }
}