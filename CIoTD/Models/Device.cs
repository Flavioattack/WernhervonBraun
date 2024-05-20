using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CIoTD.Models
{
    public class Device
    {
        [Key]
        public string Identifier { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Manufacturer { get; set; }

        [Required]
        public string Url { get; set; }

        [Required]
        public List<CommandDescription> Commands { get; set; }
    }
}
