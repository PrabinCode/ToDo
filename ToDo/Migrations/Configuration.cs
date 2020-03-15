namespace ToDo.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using ToDo.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<ToDo.DAL.ToDoContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ToDo.DAL.ToDoContext context)
        {
            var User = new UserModel[]
            {
            new UserModel{Name = "Prabin C Shrestha",Email = "prabin@gmail.com",Password = "prabin", ConfirmPassword = "prabin",SecretWord="prabin", CreatedDate=DateTime.Now,ModifiedDate=DateTime.Now},
            new UserModel{Name = "TEST",Email = "test@gmail.com",Password = "test1", ConfirmPassword = "test1", SecretWord="test1", CreatedDate=DateTime.Now,ModifiedDate=DateTime.Now},
            new UserModel{Name = "TEST2",Email = "test2@gmail.com",Password = "test2", ConfirmPassword = "test2", SecretWord="test2", CreatedDate=DateTime.Now,ModifiedDate=DateTime.Now}
            };
            foreach (UserModel s in User)
            {
                context.User.Add(s);
            }
            context.SaveChanges();

            var Task = new TaskModel[]
            {
            new TaskModel{CreatedBy = 1,Description = "task description1",IsComplete = false, CreatedAt=DateTime.Now,UpdatedAt=DateTime.Now},
            new TaskModel{CreatedBy = 1,Description = "task description11",IsComplete = true, CreatedAt=DateTime.Now,UpdatedAt=DateTime.Now},
            new TaskModel{CreatedBy = 2,Description = "task description2",IsComplete = false, CreatedAt=DateTime.Now,UpdatedAt=DateTime.Now},
            new TaskModel{CreatedBy = 2,Description = "task description22",IsComplete = true, CreatedAt=DateTime.Now,UpdatedAt=DateTime.Now},
            new TaskModel{CreatedBy = 3,Description = "task description3",IsComplete = false, CreatedAt=DateTime.Now,UpdatedAt=DateTime.Now},
            new TaskModel{CreatedBy = 3,Description = "task description33",IsComplete = true, CreatedAt=DateTime.Now,UpdatedAt=DateTime.Now},
            };
            foreach (TaskModel s in Task)
            {
                context.Task.Add(s);
            }
            context.SaveChanges();
        }
    }
}
