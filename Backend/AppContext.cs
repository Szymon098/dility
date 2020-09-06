using System.Data.Entity;
using EfficiencyApp.Models.DbDataModels;

namespace EfficiencyApp
{
    public class AppContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Action> Actions { get; set; }
        public DbSet<UserActions> UsersActions { get; set; }
        public DbSet<DayActions> DayActions { get; set; }

        public AppContext()
            : base("WorkDays") { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Employee>()
            //  .HasKey(e => e.EmployeeId);

            /* modelBuilder.Entity<UserActions>()
                .HasRequired(ua => ua.Employee)
                .WithMany(e => e.UserActions)
                .HasForeignKey(ua => new {  ua.EmployeeId, ua.})*/

            base.OnModelCreating(modelBuilder);
        }
    }
}