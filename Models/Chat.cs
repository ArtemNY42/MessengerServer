using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MessengerServer.Models
{
    public class Chat
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public Guid CreatorId { get; set; }

        [Required]
        public DateTime DateCreate { get; set; }

        public List<User>? Participants { get; set; }
    }
}