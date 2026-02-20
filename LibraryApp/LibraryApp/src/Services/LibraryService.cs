using LibraryApp.Data;
using LibraryApp.models;
using LibraryApp.models.DTO;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Services;

public class LibraryService
{
    LibraryContext db  = new LibraryContext();

    public void AddAuthor(string name, string country, int birthYear)
    {
        Author author = new Author() { Name = name, Country = country, BirthYear = birthYear };
        db.Authors.Add(author);
        db.SaveChanges();
    }
    
    public void AddBook(string title, int year, int pages, int authorId)
    {
       BookDto bookDto = new BookDto() {Title = title, Year = year, Pages = pages, AuthorId = authorId, Status = BookStatus.Available.ToString()};
       db.Books.Add(bookDto);
       db.SaveChanges();
    }

    public List<Author> GetAllAuthors()
    {
        var authors = db.Authors.Include(a => a.Books).ToList();
        return authors;
    }
    
    public List<Book> GetAllBooks()
    {
        var books = db.Books.Include(a => a.Author).ToList();
        List<Book> result = new List<Book>();
        foreach (var book in books)
            result.Add(ChangeBooksType(book));
        return result;
    }

    public List<Book> GetAllBooksByAuthor(string name)
    {
        var books = db.Books.Where(b => b.Author.Name.ToLower().Contains(name.ToLower()))
                                .Include(a => a.Author)
                                .ToList();
        List<Book> result = new List<Book>();
        foreach (var book in books)
            result.Add(ChangeBooksType(book));
        return result;
    }

    public Book? GetBookByTitle(string title)
    {
        var book = GetBookDtoByTitle(title);
        if (book == null)
            throw new Exception("Book not found");
        Book result = ChangeBooksType(book);
        return result;
    }
    
    public bool TakeBook(string title)
    {
        var book = GetBookDtoByTitle(title);
        bool isFree = false;
        if (book != null && book.Status == BookStatus.Available.ToString())
        {
            book.Status = BookStatus.Borrowed.ToString();
            isFree = true;
            db.SaveChanges();
        }
        if (book == null)
            throw new Exception("Book not found");
        return isFree;
    }

    public void ReturnBook(string title)
    {
        var book = GetBookDtoByTitle(title);
        if (book != null && book.Status != BookStatus.Available.ToString())
        {
            book.Status = BookStatus.Available.ToString();
            db.SaveChanges();
        }
        else
            throw new Exception("Book not found");
    }

    public void DeleteBook(string title)
    {
        var book = GetBookDtoByTitle(title);
        if (book != null)
        {
            db.Books.Remove(book);
            db.SaveChanges();
        }
        else
            throw new Exception("Book not found");
    }
    
    public void DeleteAuthor(string name)
    {
        var author = GetAuthorByName(name);
        if (author != null)
        {
            db.Authors.Remove(author);
            db.SaveChanges();
        }
        else
            throw new Exception("Author not found");
    }

    private Author? GetAuthorByName(string name)
    {
        var author = db.Authors.Include(a => a.Books)
                                .FirstOrDefault(a => a.Name.ToLower() == name.ToLower());
        return author;
    }

    private Book ChangeBooksType(BookDto bookDto)
    {
        BookStatus bookDtoStatus = Enum.Parse<BookStatus>(bookDto.Status);
        Book book = new Book(bookDto.Id, bookDto.Title, bookDto.Year, bookDto.Pages, bookDtoStatus, bookDto.AuthorId);
        return book;
    }

    private BookDto? GetBookDtoByTitle(string title)
    {
        var book = db.Books.Include(a => a.Author)
            .FirstOrDefault(b => b.Title == title);
        return book;
    }
}