using LibraryApp.models;
using LibraryApp.Services;
class Program
{
    static LibraryService service = new LibraryService();
    static void Main()
    {
        int choice = 0;
        while (choice != 11)
        {
            Console.WriteLine("\n1. Add an author\n" +
                              "2. Add a book\n" +
                              "3. Show all authors\n" +
                              "4. Show all books\n" +
                              "5. Show books by author\n" +
                              "6. Find a book by title\n" +
                              "7. Borrow a book\n" +
                              "8. Return a book\n" +
                              "9. Delete a book\n" +
                              "10. Delete an author\n" +
                              "11. Exit");
            Console.Write("Choose an option: ");
            choice = int.Parse(Console.ReadLine());
            DoChoice(choice);
        }
    }

    private static void DoChoice(int choice)
    {
        switch (choice)
        {
            case 1:
            {
                AddAnAuthor();
                break;
            }
            case 2:
            {
                AddBook();
                break;
            }
            case 3:
            {
                ShowAllAuthors();
                break;
            }
            case 4:
            {
                ShowAllBooks();
                break;
            }
            case 5:
            {
                ShowBooksByAuthor();
                break;
            }
            case 6:
            {
                ShowBookByTitle();
                break;
            }
            case 7:
            {
                BorrowBook();
                break;
            }
            case 8:
            {
                ReturnBook();
                break;
            }
            case 9:
            {
                DeleteBook();
                break;
            }
            case 10:
            {
                DeleteAuthor();
                break;
            }
            case 11:
            {
                Console.WriteLine("Exiting...");
                break;
            }
            default:
            {
                Console.WriteLine("Invalid choice");
                break;
            }
        }
    }

    private static void AddAnAuthor()
    {
        Console.Write("Enter author's name: ");
        string name = Console.ReadLine();
        Console.Write("Enter author's country: ");
        string country = Console.ReadLine();
        Console.Write("Enter author's year of birth: ");
        int  yearOfBirth = int.Parse(Console.ReadLine());
        service.AddAuthor(name, country, yearOfBirth);
    }

    private static void AddBook()
    {
        ShowAllAuthors();
        var authors =  service.GetAllAuthors();
                
        Console.Write("Enter author's number: ");
        int authorNumber = int.Parse(Console.ReadLine());
        int authorId = authors[authorNumber - 1].Id;
        string title = InputTitle();
        Console.Write("Enter book release year: ");
        int year =  int.Parse(Console.ReadLine());
        Console.Write("Enter the number of pages: ");
        int pages = int.Parse(Console.ReadLine());
        service.AddBook(title, year, pages, authorId);
    }

    private static void ShowAllAuthors()
    {
        var authors =  service.GetAllAuthors();
        Console.WriteLine("Authors");
        for (int i = 0; i < authors.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {authors[i].Name}, {authors[i].Country}, {authors[i].BirthYear}");
        }
    }

    private static void ShowAllBooks()
    {
        var books = service.GetAllBooks();
        Console.WriteLine("Books");
        ShowBooks(books);
    }

    private static void ShowBooksByAuthor()
    {
        Console.Write("Enter author's name: ");
        string name = Console.ReadLine();
        var books = service.GetAllBooksByAuthor(name);
        ShowBooks(books);
    }

    private static void ShowBookByTitle()
    {
        string title = InputTitle();
        var book = service.GetBookByTitle(title);
        Console.WriteLine($"{book.Title} ({book.Year}, {book.Pages} pages)");
    }

    private static void BorrowBook()
    {
        string title = InputTitle();
        bool isFree = service.TakeBook(title);
        if (isFree == false)
            Console.WriteLine("Book is already borrowed");
        else
            Console.WriteLine("You took the book");
    }

    private static void ReturnBook()
    {
        string title = InputTitle();
        service.ReturnBook(title);
        Console.WriteLine("You returned the book");
    }

    private static void DeleteBook()
    {
        string title = InputTitle();
        service.DeleteBook(title);
        Console.WriteLine("You deleted the book");
    }
    
    private static void DeleteAuthor()
    {
        Console.WriteLine("Enter author's name: ");
        string name = Console.ReadLine();
        service.DeleteAuthor(name);
        Console.WriteLine("You deleted the author");
    }
    
    private static string InputTitle()
    {
        Console.Write("Enter title: ");
        string title = Console.ReadLine();
        return title;
    }
    

    private static void ShowBooks(List<Book> books)
    {
        for (int i = 0; i < books.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {books[i].Title} ({books[i].Year}, {books[i].Pages} pages)");
        }
    }
}