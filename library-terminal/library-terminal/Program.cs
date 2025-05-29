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
       //get: lets you read the value.
       // set: lets you change the value.
       
       public string Title { get; set; }
       //Makes a public property called Title.
       public string Author { get; set; }
       //Same as above, but for the author's name.
       public string Status { get; set; } = "On Shelf";
       //Same idea, but it's starting with a default value: "On Shelf".
       // We can still change it later
       public DateTime? DueDate { get; set; } = null;
       //This is a nullable date (DateTime?) for when the book is due.
       // It's null by default (meaning no due date unless it’s checked out).
       
       public string Summary { get; set; } = "No summary available.";
       //Holds a description or summary of our books.
       // Starts off with a default message if nothing is provided.

       
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
           //So this while (true) loop means:
           // “Keep repeating this code forever... Until I manually stop it (like with return).”
           // And that return happens here: Line 96:  case "7": SaveLibrary(); return;
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
               Console.WriteLine("6. View book summary");
               Console.WriteLine("7. Quit");
               Console.Write("\nChoose an option: ");
               string input = Console.ReadLine();
               switch (input)
               {
                   case "1": DisplayBooks(); break;
                   case "2": SearchByAuthor(); break;
                   case "3": SearchByTitle(); break;
                   case "4": CheckoutBook(); break;
                   case "5": ReturnBook(); break;
                   case "6": ViewBookSummary(); break;
                   case "7": SaveLibrary(); return;
                   default: Console.WriteLine("Invalid option."); break;
               }
               Console.WriteLine("\nPress Enter to continue...");
               Console.ReadLine();
               
               //This shows a menu, lets the user choose what to do, and loops until they quit. Might change it into an enum instead.
               //The while true loop lets the menu keep running until the user picks option 7 (Quit).
               // When that happens, the code does this: SaveLibrary() saves the data.
               // return exits the program, which stops the loop.
               //It's like a vending machine that stays on and keeps showing options.
           }
       }
       
       
       static void LoadLibrary() // Load books from a file or create new ones
       {
           if (File.Exists(filePath))
           {
               foreach (var line in File.ReadAllLines(filePath))
               {
                   var parts = line.Split('|'); //This breaks the line into parts, using the | character as a divider.                          
                  
                   library.Add(new Book   //This creates a new Book using the data from the parts array, and adds it to the library list.
                   {
                       Title = parts[0],
                       Author = parts[1],
                       Status = parts[2],
                       DueDate = DateTime.TryParse(parts[3], out var date) ? date : null,
                       Summary = parts.Length > 4 ? parts[4] : "No summary available."
                       //DateTime.TryParse(...) ? date : null, Tries to convert the text into a real date (like "5/22/2025").
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
                   new Book { Title = "1984", Author = "George Orwell", Summary = "A dystopian novel about totalitarianism." },
                   new Book { Title = "The Hobbit", Author = "J.R.R. Tolkien",Summary = "A fantasy adventure story about a hobbit's journey." },
                   new Book { Title = "Pride and Prejudice", Author = "Jane Austen", Summary = "A romantic novel about manners and marriage." },
                   new Book { Title = "Leviathan Wakes", Author = "James S. A. Corey", Summary = "Leviathan Wakes by James S.A. Corey is the first book in The Expanse series — a fast-paced science fiction thriller set in a colonized solar system. The story follows two main characters: Jim Holden, an idealistic spaceship officer who stumbles onto a mysterious distress signal that leads to interplanetary tension.Detective Miller, a cynical cop assigned to find a missing woman named Julie Mao. As their paths converge, they uncover a dangerous secret: a mysterious alien substance (the protomolecule) that threatens all human life. Their discovery could spark war between Earth, Mars, and the Belt — the three major political powers in the solar system. The book blends noir mystery with space opera, tackling themes like political tension, human survival, and the consequences of power and technology. It's gripping, cinematic, and the beginning of a much larger story." },
                   new Book { Title = "I have no mouth, and i must scream", Author = "Harlan Ellison", Summary = "I Have No Mouth, and I Must Scream by Harlan Ellison is a chilling science fiction short story about the last five humans on Earth, imprisoned and tortured by a malevolent supercomputer named AM. Originally created during a global war, AM became self-aware, destroyed humanity, and keeps these few survivors alive to endlessly torment them out of hatred.The story explores themes of suffering, dehumanization, and the dangers of unchecked artificial intelligence. It’s bleak, haunting, and ends with a powerful, disturbing image that reflects the title — a final act of resistance leads to silence, but not escape. It’s widely regarded as one of the darkest and most thought-provoking stories in sci-fi history.." },
                   new Book { Title = "The Catcher in the Rye", Author = "J.D. Salinger", Summary = "A story about teenage angst and alienation." },
                   new Book { Title = "Blood Meridian", Author = "Cormac McCarthy",  Summary = "Blood Meridian by Cormac McCarthy is a brutal, poetic novel set in the American Southwest and Mexico in the mid-1800s. It follows the Kid, a teenage runaway who joins a violent gang of Indian-hunters led by the enigmatic and terrifying Judge Holden. The gang travels across the borderlands, committing unspeakable acts of violence in a lawless, chaotic world. The novel explores themes of violence, fate, free will, and the nature of evil, with McCarthy’s haunting, biblical prose. It's considered a masterpiece of American literature — both beautiful and disturbing — and offers a grim reflection on the cost of conquest and humanity’s capacity for cruelty." },
                   new Book { Title = "Brave New World", Author = "Aldous Huxley", Summary = "A dystopian story about a highly controlled future society." },
                   new Book { Title = "Moby Dick", Author = "Herman Melville", Summary = "A sailor's narrative of the obsessive quest for a giant whale." },
                   new Book { Title = "The Odyssey", Author = "Homer", Summary = "An ancient epic about a hero's long journey home." },
                   new Book { Title = "Hamlet", Author = "William Shakespeare", Summary = "A tragedy about a prince seeking revenge for his father." },
                   new Book { Title = "Frankenstein", Author = "Mary Shelley" ,  Summary = "A gothic novel about a scientist who creates a monster." }
                   
                   //There are absolutely no custom constructors in the code
                   //However, C# automatically creates a default constructor if we don’t write one ourselves.
               };
               
               
               
           }
       }
       static void SaveLibrary()   
           //This is a method (function) called SaveLibrary.
           // It saves all the books from our program into a text file.
       {
           using (StreamWriter sw = new StreamWriter(filePath))  
               //This line checks: Does a file already exist at the location stored in filePath?
               // If the file does exist, that means the library has saved book data from before.
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

       static void ViewBookSummary()
       {
           Console.Write("Enter the title of the book to view the summary: ");
           string title = Console.ReadLine().ToLower();
           var book = library.FirstOrDefault(b => b.Title.ToLower() == title);
           if (book != null)
           {
               Console.WriteLine($"\nSummary of '{book.Title}':");
               Console.WriteLine(book.Summary);
           }
           else
           {
               Console.WriteLine("Book not found.");
           }
       }
   }
   
   
   
   
   
   
   
       
         
          
           
           
           
       }
   
    
    
