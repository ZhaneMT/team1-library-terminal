namespace library_terminal


{
    
/*
 * 1. create book object properties
 * 2. init book object properties
 * 3.create book constructor
 * 4.Create Library List
 * 5. Add 12 books to the list
 * 6.create method to display all books
 * 7.create method to Search for a book by author
 *8.create method to Search for a book by title keyword
 * 9.create method to select book
 * 10.if book is checked out let them know
 * 11. If not, check it out to them and set the due date to 2 weeks from today.
   12. Return a book. (You can decide how that looks/what questions it asks.)
 * 
 */using System;
   using System.Collections.Generic;
   using System.IO;
   using System.Linq;
   class Book
   {
       public string Title { get; set; }
       public string Author { get; set; }
       public string Status { get; set; } = "On Shelf";
       public DateTime? DueDate { get; set; } = null;
       public override string ToString()
       {
           string due = DueDate.HasValue ? DueDate.Value.ToShortDateString() : "N/A";
           return $"{Title,-30} | {Author,-20} | {Status,-12} | Due: {due}";
       }
   }
   class Program
   {
       static List<Book> library = new List<Book>();
       static string filePath = "library.txt";
       static void Main()
       {
           LoadLibrary();
           while (true)
           {
               Console.Clear();
               Console.WriteLine("Library Menu:");
               Console.WriteLine("1. Display all books");
               Console.WriteLine("2. Search by author");
               Console.WriteLine("3. Search by title keyword");
               Console.WriteLine("4. Check out a book");
               Console.WriteLine("5. Return a book");
               Console.WriteLine("6. Quit");
               Console.Write("\nChoose an option: ");
               string input = Console.ReadLine();
               switch (input)
               {
                   case "1": DisplayBooks(); break;
                   case "2": SearchByAuthor(); break;
                   case "3": SearchByTitle(); break;
                   case "4": CheckoutBook(); break;
                   case "5": ReturnBook(); break;
                   case "6": SaveLibrary(); return;
                   default: Console.WriteLine("Invalid option."); break;
               }
               Console.WriteLine("\nPress Enter to continue...");
               Console.ReadLine();
           }
       }
       static void LoadLibrary()
       {
           if (File.Exists(filePath))
           {
               foreach (var line in File.ReadAllLines(filePath))
               {
                   var parts = line.Split('|');
                   library.Add(new Book
                   {
                       Title = parts[0],
                       Author = parts[1],
                       Status = parts[2],
                       DueDate = DateTime.TryParse(parts[3], out var date) ? date : null
                   });
               }
           }
           else
           {
               library = new List<Book>
               {
                   new Book { Title = "1984", Author = "George Orwell" },
                   new Book { Title = "The Hobbit", Author = "J.R.R. Tolkien" },
                   new Book { Title = "Pride and Prejudice", Author = "Jane Austen" },
                   new Book { Title = "The Great Gatsby", Author = "F. Scott Fitzgerald" },
                   new Book { Title = "To Kill a Mockingbird", Author = "Harper Lee" },
                   new Book { Title = "The Catcher in the Rye", Author = "J.D. Salinger" },
                   new Book { Title = "The Lord of the Rings", Author = "J.R.R. Tolkien" },
                   new Book { Title = "Brave New World", Author = "Aldous Huxley" },
                   new Book { Title = "Moby Dick", Author = "Herman Melville" },
                   new Book { Title = "The Odyssey", Author = "Homer" },
                   new Book { Title = "Hamlet", Author = "William Shakespeare" },
                   new Book { Title = "Frankenstein", Author = "Mary Shelley" }
               };
           }
       }
       static void SaveLibrary()
       {
           using (StreamWriter sw = new StreamWriter(filePath))
           {
               foreach (var book in library)
               {
                   sw.WriteLine($"{book.Title}|{book.Author}|{book.Status}|{book.DueDate}");
               }
           }
       }
       static void DisplayBooks()
       {
           Console.WriteLine("\nLibrary Catalog:\n");
           Console.WriteLine($"{"Title",-30} | {"Author",-20} | {"Status",-12} | Due Date");
           Console.WriteLine(new string('-', 75));
           foreach (var book in library)
           {
               Console.WriteLine(book);
           }
       }
       static void SearchByAuthor()
       {
           Console.Write("Enter author name: ");
           string author = Console.ReadLine().ToLower();
           var results = library.Where(b => b.Author.ToLower().Contains(author)).ToList();
           if (results.Any())
           {
               Console.WriteLine("\nBooks by author:");
               results.ForEach(b => Console.WriteLine(b));
           }
           else
           {
               Console.WriteLine("No books found.");
           }
       }
       static void SearchByTitle()
       {
           Console.Write("Enter title keyword: ");
           string keyword = Console.ReadLine().ToLower();
           var results = library.Where(b => b.Title.ToLower().Contains(keyword)).ToList();
           if (results.Any())
           {
               Console.WriteLine("\nBooks matching title keyword:");
               results.ForEach(b => Console.WriteLine(b));
           }
           else
           {
               Console.WriteLine("No books found.");
           }
       }
       static void CheckoutBook()
       {
           DisplayBooks();
           Console.Write("\nEnter title of the book to check out: ");
           string title = Console.ReadLine().ToLower();
           var book = library.FirstOrDefault(b => b.Title.ToLower() == title);
           if (book == null)
           {
               Console.WriteLine("Book not found.");
           }
           else if (book.Status == "Checked Out")
           {
               Console.WriteLine($"This book is already checked out and due back on {book.DueDate?.ToShortDateString()}.");
           }
           else
           {
               book.Status = "Checked Out";
               book.DueDate = DateTime.Now.AddDays(14);
               Console.WriteLine($"You have checked out '{book.Title}'. Due back on {book.DueDate?.ToShortDateString()}.");
           }
       }
       static void ReturnBook()
       {
           Console.Write("Enter the title of the book to return: ");
           string title = Console.ReadLine().ToLower();
           var book = library.FirstOrDefault(b => b.Title.ToLower() == title);
           if (book == null)
           {
               Console.WriteLine("Book not found.");
           }
           else if (book.Status == "On Shelf")
           {
               Console.WriteLine("This book is already on the shelf.");
           }
           else
           {
               book.Status = "On Shelf";
               book.DueDate = null;
               Console.WriteLine($"You have returned '{book.Title}'.");
           }
       }
   }
   
   
   
   
   
   
   
       
         
          
           
           
           
       }
   
    
    
