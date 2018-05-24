using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartHomeAutomation.Web.Models.Devices
{
    [Table(nameof(Manufacturer), Schema = "Devices")]
    public class Manufacturer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ManufacturerId { get; set; }
        [MaxLength(50)]
        public string ManufacturerName { get; set; }
        [MaxLength(100)]
        public string ManufacturerWebsiteAddress { get; set; }

        public ICollection<Device> Devices { get; set; }

        [Required]
        public string CreatedBy { get; set; } = "System Generated";
        [Required]
        public DateTime CreatedOn { get; set; } = new DateTime();
        public string UpdatedBy { get; set; } = "System Generated";
        public DateTime UpdatedOn { get; set; }
        [MaxLength(1)]
        [Required]
        public string Status { get; set; } = "A";
    }
}
