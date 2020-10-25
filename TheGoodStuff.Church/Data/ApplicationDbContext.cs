using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.EntityFrameworkCore.SqlServer.Scaffolding;
using Microsoft.EntityFrameworkCore.SqlServer.Storage;
using Microsoft.EntityFrameworkCore.SqlServer.ValueGeneration;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using TheGoodStuff.Church.Data.Models;

namespace TheGoodStuff.Church.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { 
        }

        public DbSet<QueuedSiteToRetreive> QueuedSitesToRetreive { get; set; }

        protected override void OnModelCreating(ModelBuilder builder) 
        {
            base.OnModelCreating(builder);
            var dictionaryJasonConverter = new ValueConverter<Dictionary<string, string>, string>(
                n => JsonSerializer.Serialize(n, new JsonSerializerOptions()),
                n => string.IsNullOrEmpty(n)
                ? new Dictionary<string, string>()
                : JsonSerializer.Deserialize<Dictionary<string, string>>(n, new JsonSerializerOptions())
                );

            var queuedSiteToRetreive = builder.Entity<QueuedSiteToRetreive>();
            queuedSiteToRetreive.HasKey(n => n.Id);
            queuedSiteToRetreive
                .Property(n => n.Id)
                .ValueGeneratedOnAdd();
            queuedSiteToRetreive.HasOne(n => n.RecordCreatedBy)
                //.WithMany(n => n.QueuedSitesToRetrieve)
                .WithMany()
                .HasForeignKey(n => n.RecordCreatedByUserId)
                .HasPrincipalKey(n => n.Id)
                .OnDelete(DeleteBehavior.Restrict);
            queuedSiteToRetreive
                .Property(n => n.Details)
                .IsRequired(false)
                .HasConversion(dictionaryJasonConverter);
            //queuedSiteToRetreive.Property(n => n.RecordCreatedBy)
            //    .IsRequired(false);
            queuedSiteToRetreive.Property(n => n.DateEntered)
                .IsRequired(true)
                .ValueGeneratedOnAdd()
                .HasDefaultValue<DateTime>(DateTime.UtcNow);
            queuedSiteToRetreive.Property(n => n.FeedType)
                .IsRequired(true)
                .HasDefaultValue<QueuedSiteToRetreive.FEED_TYPE>(
                    QueuedSiteToRetreive.FEED_TYPE.XML
                    );

            //var applicationUser = builder.Entity<ApplicationUser>();
            //applicationUser.HasMany(n => n.QueuedSitesToRetrieve)
            //    .WithOne(n => n.RecordCreatedBy)
            //    .HasForeignKey(n => n.RecordCreatedByUserId)
            //    .HasPrincipalKey(n => n.Id)
            //    .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
