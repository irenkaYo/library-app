namespace LibraryApp.models;

public class Book
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int Year { get; set; }
    public int Pages { get; set; }
    public BookStatus Status { get; set; }
    public int AuthorId { get; set; }
    public Author Author { get; set; }

    public Book(int id, string title, int year, int pages, BookStatus status, int authorId)
    {
        Id = id;
        Title = title;
        Year = year;
        Pages = pages;
        Status = status;
        AuthorId = authorId;
    }
}