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
 */
    
    using System;
   using System.Collections.Generic;
   using System.IO;
   using System.Linq;
   class Book
   {
       
       public string Title { get; set; }
       public string Author { get; set; }
       public string Status { get; set; } = "On Shelf";
       public DateTime? DueDate { get; set; } = null;
       
      // Title, Author, Status, DueDate: These are like properties of each book (like name, label, and due date).
      // Status starts as "On Shelf" by default.
      // DueDate starts as null (meaning there's no due date unless checked out).
       public override string ToString()
       {
           string due = DueDate.HasValue ? DueDate.Value.ToShortDateString() : "N/A";
           return $"{Title,-30} | {Author,-20} | {Status,-12} | Due: {due}";
       }
       
       //If there's a due date, show it. If not, show "N/A".
       // Makes this a neat, formatted row to show each book in the console.
       
   }
   class Program
   {
       static List<Book> library = new List<Book>();
       static string filePath = "library.txt";
       
       //library: Stores the list of all books.
       // filePath: The name of the file where books will be saved/loaded.
       
       
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
               
               //This shows a menu, lets the user choose what to do, and loops until they quit. Might change it into an enum instead.
           }
       }
       
       
       static void LoadLibrary() // Load books from a file or create new ones
       {
           if (File.Exists(filePath))
           {
               foreach (var line in File.ReadAllLines(filePath))
               {
                   var parts = line.Split('|');  //This breaks a line of text (like "1984|George Orwell|Checked Out|5/22/2025") into pieces wherever it sees a |.
                                                 
                   library.Add(new Book   //This creates a new Book using the data from the parts array, and adds it to the library list.
                   {
                       Title = parts[0],
                       Author = parts[1],
                       Status = parts[2],
                       DueDate = DateTime.TryParse(parts[3], out var date) ? date : null
                       
                       //DateTime.TryParse(...) ? date : null: Tries to convert the text into a real date (like "5/22/2025").
                       // If it works, use that date.
                       // If it fails (bad or missing date), use null instead.
                       // This will hopefully keep our program from crashing if the date is missing or not valid.
                   });
                   
                   
                   
               }
           }
           else
           {
               library = new List<Book>  //Save book list to a file. 
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
                   
                   //There are absoulutely no custom constructors in the code
                   //However, C# automatically creates a default constructor if you don’t write one yourself.
               };
               
               
               
           }
       }
       static void SaveLibrary()   
           //This is a method (function) called SaveLibrary.
           // It saves all the books from our program into a text file.
       {
           using (StreamWriter sw = new StreamWriter(filePath))  
               //This opens a file to write text into it.
               // filePath is the name of the file (like "library.txt")
              
           {
               foreach (var book in library)   //This goes through every book in our list called library.
               {
                   sw.WriteLine($"{book.Title}|{book.Author}|{book.Status}|{book.DueDate}");
               }
               
               
               
               
           }
       }
       static void DisplayBooks()   //Show all books
       {
           Console.WriteLine("\nLibrary Catalog:\n");
           Console.WriteLine($"{"Title",-30} | {"Author",-20} | {"Status",-12} | Due Date");
           Console.WriteLine(new string('-', 75));
           foreach (var book in library)
           {
               Console.WriteLine(book);
           }
           
           
           
       }
       static void SearchByAuthor() //Find books by author name (Shows books that contain the author name.)
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
       static void SearchByTitle() //Find books by title keyword
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
       
       
       static void CheckoutBook()  //Borrow a book
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
       static void ReturnBook()   // Return a book
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
   
    
    
