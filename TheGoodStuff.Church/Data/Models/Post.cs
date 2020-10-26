using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TheGoodStuff.Church.Data.Models
{
    public class Post
    {
        public Guid Id { get; set; }
        [StringLength(200, ErrorMessage = "Name must be between 10 and 200 characters.", MinimumLength = 10)]
        [Required]
        public string SubjectLine { get; set; }
        [Required]
        public string Body { get; set; }
        public DateTime DateEntered { get; set; }
        [Required]
        public Guid OwnerId { get; set; }
        public ApplicationUser Owner { get; set; }
        public POST_STATUS Status { get; set; }

        public enum POST_STATUS
        { 
            DRAFT,
            REVIEW,
            READY,
            PUBLISHED,
            DELETED
        }
    }
}
