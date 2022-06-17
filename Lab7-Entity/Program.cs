using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

public class Program
{
    public static void Main()
    {
        // Hint: change `DESKTOP-123ABC\SQLEXPRESS` to your server name
        //       alternatively use `localhost` or `localhost\SQLEXPRESS`

        PerformDatabaseOperations().Wait();
    }

    public static async Task PerformDatabaseOperations()
    {
        string connectionString = @"Data Source=DESKTOP-4E1085R;Initial Catalog=blogdb;Integrated Security=True";

        using (BloggingContext db = new BloggingContext(connectionString))
        {
            Console.WriteLine($"Database ConnectionString: {db.ConnectionString}.");

            // Create
            Console.WriteLine("Inserting a new blog");
            db.Add(new Blog { Url = "http://blogs.msdn.com/adonet" });
            //db.SaveChanges();
            await db.SaveChangesAsync();

            Console.WriteLine("Inserting a new role");
            var roleAdmin = db.Add(new Role { Name = "Admin" });
            var roleReader = db.Add(new Role { Name = "Reader" });
            //db.SaveChanges();
            await db.SaveChangesAsync();

            Console.WriteLine("Inserting a new task");
            var cl = db.Add(new Case { Name = "Cleaning" });
            var sl = db.Add(new Case { Name = "Slepping" });
            var lea = db.Add(new Case { Name = "Learning" });
            //db.SaveChanges();
            await db.SaveChangesAsync();

            Console.WriteLine("Inserting a new user");
            db.Add(new User { Name = "Adam Nowak", Pesel = 95874456881, Address = "Stalowa 8", RoleId = roleAdmin.Entity.Id, TaskId = cl.Entity.Id });
            db.Add(new User { Name = "Zofia Nowak", Pesel = 99974456881, Address = "Stalowa 8", RoleId = roleReader.Entity.Id, TaskId = sl.Entity.Id });
            var userUpdate = db.Add(new User { Name = "Zofia Nowak", Pesel = 99974456881, Address = "Stalowa 8", RoleId = roleReader.Entity.Id, TaskId = sl.Entity.Id });
            // db.SaveChanges();
            await db.SaveChangesAsync();

            // Read
            Console.WriteLine("Querying for a blog");
            Blog blog = await db.Blogs
                .OrderBy(b => b.Id)
                .FirstAsync();
            User user = await db.User
                .OrderBy(b => b.Address)
                .LastAsync();

            int count = await db.Roles
                .CountAsync();

            int task = await db.Tasks
                .Where(u => u.Name.StartsWith("S"))
                .CountAsync();

            // Update
            Console.WriteLine("Updating the blog and adding a post");
            blog.Url = "https://devblogs.microsoft.com/dotnet";
            blog.Posts.Add(new Post { Title = "Hello World", Content = "I wrote an app using EF Core!" });
            user.Name = "Marek";
            userUpdate.Entity.Name = "Elżbieta";
            //db.SaveChanges();
            await db.SaveChangesAsync();

            // Delete
            Console.WriteLine("Delete the blog");
            db.Remove(blog);
            db.Remove(user);
            //db.SaveChanges();
            await db.SaveChangesAsync();
        }
    }
}