using LibraryApp.models;
using LibraryApp.models.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace LibraryApp.Data;

public class LibraryContext : DbContext
{
    public DbSet<Author> Authors { get; set; }
    public DbSet<BookDto> Books { get; set; }
    
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
        
        BookDto peaceAndWar = new BookDto { Id = 1, Title = "Peace and war", Year = 1869, Pages = 1225, Status = BookStatus.Available.ToString(), AuthorId = 1 };
        BookDto annaKarenina = new BookDto { Id = 2, Title = "Anna Karenina", Year = 1877, Pages = 864, Status = BookStatus.Available.ToString(), AuthorId = 1 };
        BookDto crimeAndPunishment = new BookDto { Id = 3, Title = "Crime and Punishment", Year = 1866, Pages = 671, Status = BookStatus.Available.ToString(), AuthorId = 2 };
        BookDto theBrothersKaramazov = new BookDto { Id = 4, Title = "The Brothers Karamazov", Year = 1880, Pages = 796, Status = BookStatus.Available.ToString(), AuthorId = 2 };
        BookDto harryPotterAndPhilosopherStone = new BookDto { Id = 5, Title = "Harry Potter and the Philosopher's Stone", Year = 1977, Pages = 223, Status = BookStatus.Available.ToString(), AuthorId = 3 };
        modelBuilder.Entity<BookDto>().HasData(peaceAndWar, annaKarenina, crimeAndPunishment, theBrothersKaramazov, harryPotterAndPhilosopherStone);
    }
}