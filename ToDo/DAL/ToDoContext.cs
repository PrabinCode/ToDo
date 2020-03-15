using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using ToDo.Models;

namespace ToDo.DAL
{
    public class ToDoContext : DbContext
    {
        public ToDoContext() : base("name=ToDoList") 
        {
            Configuration.LazyLoadingEnabled = true;
        }


        public DbSet<LoginModel> Login { get; set; }
        public DbSet<TaskModel> Task { get; set; }
        public DbSet<UserModel> User { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}