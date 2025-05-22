namespace library_terminal;

using System;


class Book
{
    public string Title { get; set; }
    public string Author { get; set; }
    public bool Status { get; set; } 
    public DateTime? DueDate { get; set; } = null;

    public override string ToString()
    {
        string due = DueDate.HasValue ? DueDate.Value.ToShortDateString() : "N/A";
        return $"{Title,-30} | {Author,-20} | {Status,-12} | Due: {due}";
    }
    
    //-----------------------------------------------------------------
    // CREATION OF THE BOOK OBJECT ITSELF
    public Book(string title, string author, bool status, string dueDate)
    {
        Title = title;
        Author = author;
        Status = status;
        DueDate = DueDate;
    }
    public List<Book> Books;

    public Book()
    {
        Books = new List<Book>();
        {
            new Book("Title", "Author", false, "Due date");
            new Book("Title", "Author", false, "Due date");
            new Book("Title", "Author", false, "Due date");
            new Book("Title", "Author", false, "Due date");
            new Book("Title", "Author", false, "Due date");
            new Book("Title", "Author", false, "Due date");
            new Book("Title", "Author", false, "Due date");
            new Book("Title", "Author", false, "Due date");
            new Book("Title", "Author", false, "Due date");
            new Book("Title", "Author", false, "Due date");
            new Book("Title", "Author", false, "Due date");
            new Book("Title", "Author", false, "Due date");   
        }
    }
    
    //------------------------------------------------------------------
}
