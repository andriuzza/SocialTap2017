﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SocialTap.DataAccess.Models;
using SocialTap.DataAccess.Models.Feedbacks;
using SocialTap.DataAccess.Models.Notifications;

namespace SocialTap.WEB.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
      
        public ICollection<NotificationUser> Notifications { get; set; }

        public ApplicationUser()
        {
            Notifications = new Collection<NotificationUser>();
        }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("SocialTapDB", throwIfV1Schema: false)
        {
        }
        public DbSet<Drink> Drinks { get; set; }
        public DbSet<DrinkFeedback> DrinkFeedbacks { get; set; }
        public DbSet<DrinkImage> DrinkImages { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<DrinkType> DrinkTypes { get; set; }
        public DbSet<LocationFeedback> LocationFeedbacks { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<NotificationUser> NotificationUsers { get; set; }
        public DbSet<DrinkRating> DrinkRating { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
           base.OnModelCreating(modelBuilder);

           modelBuilder.Entity<Drink>()
                .HasRequired(u => u.LocationOfDrink)
                .WithMany(e => e.Drinks)
                .HasForeignKey(a => a.LocationOfDrinkId)
                .WillCascadeOnDelete(true);


            /*  modelBuilder.Entity<NotificationUser>()
                  .HasRequired(a => a.UserAccount)
                  .WithMany(b => b.Notifications)
                  .WillCascadeOnDelete(false);*/
            /*modelBuilder.Entity<NotificationUser>()
             .HasKey(c => new { c.UserAccountId, c.NotificationId });

            modelBuilder.Entity<Notification>()
                .HasMany(c => c.)
                .WithRequired()
                .HasForeignKey(c => c.ContractId);

            modelBuilder.Entity<Media>()
                .HasMany(c => c.ContractMedias)
                .WithRequired()
                .HasForeignKey(c => c.MediaId);*/


        }
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}