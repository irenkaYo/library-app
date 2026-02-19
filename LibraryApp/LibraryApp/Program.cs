using LibraryApp.Services;
class Program
{
    static void Main()
    {
        int choice = 0;
        while (choice != 12)
        {
            Console.WriteLine("1. Add an author\n" +
                              "2. Add a book\n" +
                              "3. Show all authors\n" +
                              "4. Show all books\n" +
                              "5. Show books by author\n" +
                              "6. Find a book by title\n" +
                              "7. Borrow a book\n" +
                              "8. Return a book\n" +
                              "9. Delete a book\n" +
                              "10. Delete an author\n" +
                              "11. Show statistics\n" +
                              "12. Exit");
            Console.Write("Choose an option: ");
            choice = int.Parse(Console.ReadLine());
            DoChoice(choice);
        }
    }

    private static void DoChoice(int choice)
    {
        LibraryService service = new LibraryService();
        switch (choice)
        {
            case 1:
            {
                Console.Write("Enter author's name: ");
                string name = Console.ReadLine();
                Console.Write("Enter author's country: ");
                string country = Console.ReadLine();
                Console.Write("Enter author's year of birth: ");
                int  yearOfBirth = int.Parse(Console.ReadLine());
                service.AddAuthor(name, country, yearOfBirth);
                break;
            }
            case 2:
            {
                //вызвать метод ShowAllAuthors

                Console.Write("Enter author's number: ");
                int authorId = int.Parse(Console.ReadLine());
                Console.Write("Enter book's title: ");
                string title = Console.ReadLine();
                Console.Write("Enter book release year: ");
                int year =  int.Parse(Console.ReadLine());
                Console.Write("Enter the number of pages: ");
                int pages = int.Parse(Console.ReadLine());
                service.AddBook(title, year, pages, pages);
                break;
            }
            case 3:
            {
                var authors =  service.GetAllAuthors();
                foreach (var author in authors)
                {
                    Console.WriteLine($"Author\n{author.Id}. {author.Name}, {author.Country}, {author.BirthYear}");
                }
                break;
            }
            case 4:
            {
                var books = service.GetAllBooks();
                foreach (var book in books)
                {
                    Console.WriteLine($"Book\n{book.Id} ({book.Year}, {book.Pages} pages)");
                }
                break;
            }
            case 5:
            {
                Console.Write("Enter author's name: ");
                string name = Console.ReadLine();
                var books = service.GetAllBooksByAuthor(name);
                foreach (var book in books)
                {
                    Console.WriteLine($"Book\n{book.Id} ({book.Year}, {book.Pages} pages)");
                }
                break;
            }
            case 6:
            {
                Console.Write("Enter book's title: ");
                string title = Console.ReadLine();
                var book = service.GetBookByTitle(title);
                if  (book != null)
                    Console.WriteLine($"Book\n{book.Id} ({book.Year}, {book.Pages} pages)");
                else
                    Console.WriteLine("Book not found");
                break;
            }
            case 7:
            {
                Console.Write("Enter book's title: ");
                string title = Console.ReadLine();
                service.GetBookByTitle(title);
                Console.WriteLine("You took the book");
                break;
            }
            case 8:
            {
                Console.Write("Enter book's title: ");
                string title = Console.ReadLine();
                service.ReturnBook(title);
                Console.WriteLine("You returned the book");
                break;
            }
            case 9:
            {
                Console.Write("Enter book's title: ");
                string title = Console.ReadLine();
                service.DeleteBook(title);
                Console.WriteLine("You deleted the book");
                break;
            }
            case 10:
            {
                Console.WriteLine("Enter author's name: ");
                string name = Console.ReadLine();
                service.DeleteAuthor(name);
                Console.WriteLine("You deleted the author");
                break;
            }
            case 11:
            {
                //
                break;
            }
            case 12:
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
}