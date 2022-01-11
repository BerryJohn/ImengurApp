using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Imengur.Models
{
    public class Comment
    {
        [Required]
        [DataType(DataType.Text)]
        [MaxLength(length: 150, ErrorMessage = "Comment has to be lower than 150")]
        public string Content { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Key]
        public int Id { get; set; }

        public Image Image { get; set; }
        public BetterUser BetterUser { get; set; }
    }
}
