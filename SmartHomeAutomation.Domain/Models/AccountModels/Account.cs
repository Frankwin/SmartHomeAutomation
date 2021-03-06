﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SmartHomeAutomation.Domain.Enums;
using SmartHomeAutomation.Domain.Interfaces;
using SmartHomeAutomation.Domain.Models.SettingsModels;
using SmartHomeAutomation.Domain.Models.UserModels;

namespace SmartHomeAutomation.Domain.Models.AccountModels
{
    [Table(nameof(Account), Schema="Accounts")]
    public class Account : ITrackable, IObjectWithState
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column(Order = 0)]
        public Guid AccountId { get; set; }
        [MaxLength(50), Required, Column(Order = 1)]
        public string AccountName { get; set; }

//        public ICollection<User> Users { get; set; }
        public ICollection<Room> Rooms { get; set; }
        public ICollection<OwnedDevice> OwnedDevices { get; set; }

        [NotMapped]
        public ObjectState ObjectState { get; set; }

        #region Tracking Properties
        [Column(Order = 2)]
        public DateTime CreatedAt { get; set; }
        [Column(Order = 3)]
        public string CreatedBy { get; set; }
        public DateTime LastUpdatedAt { get; set; }
        [Column(Order = 4)]
        public string LastUpdatedBy { get; set; }
        [Column(Order = 5)]
        public bool IsDeleted { get; set; }
        #endregion
    }
}
