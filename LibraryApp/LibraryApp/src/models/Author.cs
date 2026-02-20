using LibraryApp.models.DTO;

namespace LibraryApp.models;

public class Author
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Country { get; set; }
    public int BirthYear { get; set; }
    public List<BookDto> Books { get; set; }
}