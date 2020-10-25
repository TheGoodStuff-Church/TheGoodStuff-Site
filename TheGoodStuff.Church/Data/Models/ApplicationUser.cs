using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace TheGoodStuff.Church.Data.Models
{
    public class ApplicationUser : IdentityUser<Guid> 
    { 
        //public List<QueuedSiteToRetreive> QueuedSitesToRetrieve { get; set; }
    }
}
