using HotelAdvice.Areas.WebSite.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Data.Entity;
using HotelAdvice.DataAccessLayer;

namespace HotelAdvice.Areas.Account.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            WishList = new HashSet<tbl_WishList>();
        }

        public string FirstName { get; set; }
        
        public string LastName { get; set; }

        public virtual ICollection<tbl_WishList> WishList { get; set; }
        public virtual ICollection<tbl_rating> Rating { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            userIdentity.AddClaims(new[] { new Claim("FirstName", FirstName) });

            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("HotelAdvice2", throwIfV1Schema: false)
        {
           
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); 

            modelBuilder.Entity<ApplicationUser>()
           .HasMany(e => e.WishList)
           .WithOptional(e => e.ApplicationUser)
           .HasForeignKey(e => e.UserId)
           .WillCascadeOnDelete();

            modelBuilder.Entity<ApplicationUser>()
          .HasMany(e => e.Rating)
          .WithOptional(e => e.ApplicationUser)
          .HasForeignKey(e => e.UserId)
          .WillCascadeOnDelete();


        }
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}