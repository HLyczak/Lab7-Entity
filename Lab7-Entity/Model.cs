using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

public class BloggingContext : DbContext
{
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Case> Tasks { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<User> User { get; set; }

    public string ConnectionString { get; }

    public BloggingContext(string connectionString)
    {
        this.ConnectionString = connectionString;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseSqlServer(this.ConnectionString);
}

public class Blog
{
    public long Id { get; set; }
    public string Url { get; set; }

    public List<Post> Posts { get; } = new();
}

public class Case
{
    public long Id { get; set; }
    public string Name { get; set; }
}

public class Role
{
    public long Id { get; set; }
    public string Name { get; set; }
}

public class User
{
    public long Id { get; set; }
    public string Name { get; set; }
    public long Pesel { get; set; }
    public string Address { get; set; }
    public long RoleId { get; set; }
    public long TaskId { get; set; }
    public Case Task { get; set; }
    public Role Role { get; set; }
}

public class Post
{
    public long Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }

    public long BlogId { get; set; }
    public Blog Blog { get; set; }
}