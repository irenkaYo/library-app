using LibraryApp.models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;

namespace LibraryApp.Data;

public class LibraryContext : DbContext
{
    public DbSet<Author> Authors { get; set; }
    public DbSet<Book> Books { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .SetBasePath(Directory.GetCurrentDirectory())
            .Build();
 
        optionsBuilder.UseNpgsql(config.GetConnectionString("DefaultConnection"));
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        Author tolstoi = new Author { Id = 1, Name = "Lev Tolstoi", Country = "Russia", BirthYear = 1828};
        Author dostoevsy = new Author { Id = 2, Name = "Fyodor Dostoevsky", Country = "Russia", BirthYear = 1821};
        Author rowling = new Author { Id = 3, Name = "Joanne Rowling", Country = "United Kingdom", BirthYear = 1965};
        Author oruell = new Author { Id = 4, Name = "Jorj Oruell", Country = "United Kingdom", BirthYear = 1903};
        modelBuilder.Entity<Author>().HasData(tolstoi,  dostoevsy, rowling, oruell);
        
        Book peaceAndWar =  new Book { Id = 1, Title = "Peace and war", Year = 1869, Pages = 1225, Status = BookStatus.Available, AuthorId = 1 };
        Book annaKarenina =  new Book { Id = 2, Title = "Anna Karenina", Year = 1877, Pages = 864, Status = BookStatus.Available, AuthorId = 1 };
        Book crimeAndPunishment =  new Book { Id = 3, Title = "Crime and Punishment", Year = 1866, Pages = 671, Status = BookStatus.Available, AuthorId = 2 };
        Book theBrothersKaramazov =  new Book { Id = 4, Title = "The Brothers Karamazov", Year = 1880, Pages = 796, Status = BookStatus.Available, AuthorId = 2 };
        Book harryPotterAndPhilosopherStone =  new Book { Id = 5, Title = "Harry Potter and the Philosopher's Stone", Year = 1977, Pages = 223, Status = BookStatus.Available, AuthorId = 3 };
        modelBuilder.Entity<Book>().HasData(peaceAndWar, annaKarenina, crimeAndPunishment, theBrothersKaramazov, harryPotterAndPhilosopherStone);

        modelBuilder.HasPostgresEnum<BookStatus>();

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(LibraryContext).Assembly);

    }
}