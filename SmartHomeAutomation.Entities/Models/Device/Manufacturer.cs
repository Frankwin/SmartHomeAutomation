using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartHomeAutomation.Entities.Models.Device
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

        #region Tracking Properties
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime LastUpdatedAt { get; set; }
        public string LastUpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
        #endregion
    }
}
