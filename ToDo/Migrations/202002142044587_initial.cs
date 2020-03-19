using System.Data.Entity.Migrations;

namespace ToDo.Migrations
{
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                    "dbo.LoginModel",
                    c => new
                    {
                        Id = c.Int(false, true),
                        Email = c.String(false),
                        Password = c.String(false),
                        ConfirmPassword = c.String(),
                        LoginTime = c.DateTime()
                    })
                .PrimaryKey(t => t.Id);

            CreateTable(
                    "dbo.TaskModel",
                    c => new
                    {
                        TaskId = c.Int(false, true),
                        CreatedBy = c.Int(false),
                        Description = c.String(false),
                        IsComplete = c.Boolean(false),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime()
                    })
                .PrimaryKey(t => t.TaskId);

            CreateTable(
                    "dbo.UserModel",
                    c => new
                    {
                        UserId = c.Int(false, true),
                        Name = c.String(false),
                        Email = c.String(false),
                        Password = c.String(false),
                        ConfirmPassword = c.String(false),
                        SecretWord = c.String(false),
                        CreatedDate = c.DateTime(),
                        ModifiedDate = c.DateTime()
                    })
                .PrimaryKey(t => t.UserId);
        }

        public override void Down()
        {
            DropTable("dbo.UserModel");
            DropTable("dbo.TaskModel");
            DropTable("dbo.LoginModel");
        }
    }
}