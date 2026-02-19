using LibraryApp.Data;
using LibraryApp.models;

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
       Book book = new Book() {Title = title, Year = year, Pages = pages, AuthorId = authorId, Status = BookStatus.Available};
        db.Books.Add(book);
        db.SaveChanges();
    }

    public List<Author> GetAllAuthors()
    {
        var authors = db.Authors.ToList();
        return authors;
    }
    
    public List<Book> GetAllBooks()
    {
        var books = db.Books.ToList();
        return books;
    }

    public List<Book> GetAllBooksByAuthor(string name)
    {
        var books = db.Books.Where(b => b.Author.Name.Contains(name)).ToList();
        return books;
    }

    public Book? GetBookByTitle(string title)
    {
        var book = db.Books.FirstOrDefault(b => b.Title == title);
        if (book == null)
            throw new Exception("Book not found");
        return book;
    }
    
    public bool TakeBook(string title)
    {
        var book = GetBookByTitle(title);
        bool isFree = false;
        if (book != null && book.Status == BookStatus.Available)
        {
            book.Status = BookStatus.Borrowed;
            isFree = true;
            db.SaveChanges();
        }
        if (book == null)
            throw new Exception("Book not found");
        return isFree;
    }

    public void ReturnBook(string title)
    {
        var book = GetBookByTitle(title);
        if (book != null && book.Status != BookStatus.Available)
        {
            book.Status = BookStatus.Available;
            db.SaveChanges();
        }
        else
            throw new Exception("Book not found");
    }

    public void DeleteBook(string title)
    {
        var book = GetBookByTitle(title);
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
        var author = db.Authors.FirstOrDefault(a => a.Name.ToLower() == name.ToLower());
        return author;
    }
}