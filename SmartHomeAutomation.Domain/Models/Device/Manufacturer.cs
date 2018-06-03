using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SmartHomeAutomation.Domain.Enums;
using SmartHomeAutomation.Domain.Interfaces;

namespace SmartHomeAutomation.Domain.Models.Device
{
    [Table(nameof(Manufacturer), Schema = "Devices")]
    public class Manufacturer : ITrackable, IObjectWithState
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column(Order = 0)]
        public Guid ManufacturerId { get; set; }
        [MaxLength(50), Column(Order = 1)]
        public string ManufacturerName { get; set; }
        [MaxLength(100), Column(Order = 2)]
        public string ManufacturerWebsiteAddress { get; set; }

        public ICollection<Device> Devices { get; set; }

        [NotMapped]
        public ObjectState ObjectState { get; set; }

        #region Tracking Properties
        [Column(Order = 3)]
        public DateTime CreatedAt { get; set; }
        [Column(Order = 4)]
        public string CreatedBy { get; set; }
        [Column(Order = 5)]
        public DateTime LastUpdatedAt { get; set; }
        [Column(Order = 6)]
        public string LastUpdatedBy { get; set; }
        [Column(Order = 7)]
        public bool IsDeleted { get; set; }
        #endregion
    }
}
