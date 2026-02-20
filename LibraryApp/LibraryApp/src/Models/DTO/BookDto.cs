namespace LibraryApp.models.DTO;

public class BookDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int Year { get; set; }
    public int Pages { get; set; }
    public string Status { get; set; }
    public int AuthorId { get; set; }
    public Author Author { get; set; }
}