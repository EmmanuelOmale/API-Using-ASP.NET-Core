using Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TutorialHell.Models;

namespace TutorialHell.Data
{
    //public class ContactsDbContext : IdentityDbContext<IdentityUser>
    public class ContactsDbContext : IdentityDbContext<User>
    {

        public ContactsDbContext(DbContextOptions<ContactsDbContext> options): base(options) 
        {
        
        }
      
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
           
           
        }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<User> users { get; set; }


    }
}
