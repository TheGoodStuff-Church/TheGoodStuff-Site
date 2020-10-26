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

        public DbSet<Post> Posts { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            var dictionaryJasonConverter = BuildDictionaryJsonConverter();

            SetupQueuedSiteToRetreive(builder, dictionaryJasonConverter);

            SetupPost(builder);
        }

        /// <summary>
        /// Setups the queued site to retreive.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <param name="dictionaryJasonConverter">The dictionary jason converter.</param>
        private static void SetupQueuedSiteToRetreive(ModelBuilder builder, ValueConverter<Dictionary<string, string>, string> dictionaryJasonConverter)
        {
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
            queuedSiteToRetreive.Property(n => n.DateEntered)
                .IsRequired(true)
                .ValueGeneratedOnAdd()
                .HasDefaultValue<DateTime>(DateTime.UtcNow);
            queuedSiteToRetreive.Property(n => n.FeedType)
                .IsRequired(true)
                .HasDefaultValue<QueuedSiteToRetreive.FEED_TYPE>(
                    QueuedSiteToRetreive.FEED_TYPE.XML
                    );
        }

        /// <summary>
        /// Setups the post.
        /// </summary>
        /// <param name="builder">The builder.</param>
        private static void SetupPost(ModelBuilder builder) 
        {
            var posts = builder.Entity<Post>();
            posts.HasKey(n => n.Id);
            posts
                .Property(n => n.Id)
                .ValueGeneratedOnAdd();
            posts.HasOne(n => n.Owner)
                //.WithMany(n => n.QueuedSitesToRetrieve)
                .WithMany()
                .HasForeignKey(n => n.OwnerId)
                .HasPrincipalKey(n => n.Id)
                .OnDelete(DeleteBehavior.Restrict);
            posts.Property(n => n.DateEntered)
                .IsRequired(true)
                .ValueGeneratedOnAdd()
                .HasDefaultValue<DateTime>(DateTime.UtcNow);
            posts.Property(n => n.Status)
                .IsRequired(true)
                .HasDefaultValue<Post.POST_STATUS>(
                    Post.POST_STATUS.DRAFT
                    );

        }

        /// <summary>
        /// Builds the dictionary json converter.
        /// </summary>
        /// <returns></returns>
        private static ValueConverter<Dictionary<string, string>, string> BuildDictionaryJsonConverter()
        {
            return new ValueConverter<Dictionary<string, string>, string>(
                n => JsonSerializer.Serialize(n, new JsonSerializerOptions()),
                n => string.IsNullOrEmpty(n)
                ? new Dictionary<string, string>()
                : JsonSerializer.Deserialize<Dictionary<string, string>>(n, new JsonSerializerOptions())
                );
        }
    }
}
