using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace TheGoodStuff.Church.Data.Models
{
    public class QueuedSiteToRetreive 
    { 
        public Guid Id { get; set; }
        [StringLength(100, MinimumLength = 10, ErrorMessage = "Name must be between 10 and 100 characters.")]
        public string Name { get; set; }
        public DateTime DateEntered { get; set; }
        public Nullable<DateTime> DateLastProcessed { get; set; }
        public Nullable<DateTime> DateRetired { get; set; }
        public Nullable<Guid> RecordCreatedByUserId { get; set; }
        public int FrequencyInDaysToHarvest { get; set; }
        public FEED_TYPE FeedType { get; set; }
        public Dictionary<string,string> Details { get; set; }

        public ApplicationUser RecordCreatedBy { get; set; }
        public enum FEED_TYPE
        { 
            HTML,
            XML,
            JSON,
            CUSTOM
        }
    }

}
