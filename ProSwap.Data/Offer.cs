using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSwap.Data
{
    public class Offer
    {
        [Key]
        public int ID { get; set; }

        public Guid OwnerID { get; set; }

        [Required]
        [MinLength(6, ErrorMessage = "There must be atleast 6 characters in this field.")]
        [MaxLength(100, ErrorMessage = "There must be less than 100 characters in this field.")]
        public string Title { get; set; }
        
        [Required]
        [MinLength(75, ErrorMessage = "There must be atleast 75 characters in this field.")]
        [MaxLength(6000, ErrorMessage = "There must be less than 6000 characters in this field.")]
        public string Body { get; set; }
        
        [Required]
        public bool IsAService { get; set; }
        
        [Required]
        public string Entity { get; set; }

        [Required]
        public decimal UnitPrice { get; set; }

        [Required]
        public double TotalUnitsAvailable { get; set; }

        [Required]
        public DateTimeOffset CreatedUtc { get; set; }

        public DateTimeOffset? ModifiedUtc { get; set; }
        public bool IsActive { get; set; }

        [ForeignKey("Game")]
        public int GameID { get; set; }
        public virtual Game Game { get; set; }

    }
}
