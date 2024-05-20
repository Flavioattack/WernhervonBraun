using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CIoTD.Models
{
    public class CommandDescription
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Operation { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Result { get; set; }

        [Required]
        public string Format { get; set; }

        [Required]
        public string DeviceId { get; set; }
    }
}