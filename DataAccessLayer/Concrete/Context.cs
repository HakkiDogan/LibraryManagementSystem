using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrete
{
    public class Context : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Connection string yazarız
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=LibraryManagementDb;Trusted_Connection=true");
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<BookTransaction> BookTransactions { get; set; }
        public DbSet<Checkout> Checkouts { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Punishment> Punishments { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<Writer> Writers { get; set; }
		public DbSet<Admin> Admins { get; set; }
	}
}
