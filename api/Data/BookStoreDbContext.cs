using api.Models;
using Microsoft.EntityFrameworkCore;
using MongoDB.EntityFrameworkCore.Extensions;

namespace api.Data;

public class BookStoreDbContext : DbContext
{
    public DbSet<Book> Books { get; init; }
    public DbSet<Order> Orders { get; init; }
    public DbSet<User> Users { get; init; }
    public DbSet<Author> Authors { get; init; }
    public DbSet<Publisher> Publishers { get; init; }
   
    
    public BookStoreDbContext(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Book>().ToCollection("books");
        modelBuilder.Entity<Order>().ToCollection("orders");
        modelBuilder.Entity<User>().ToCollection("users");
        modelBuilder.Entity<Author>().ToCollection("authors");
        modelBuilder.Entity<Publisher>().ToCollection("publishers");
    }
}