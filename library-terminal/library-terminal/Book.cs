namespace ConsoleApp1;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;




/*
 *  Your solution must include some kind of a book class with a title, author, status, and due
   date if checked out.
   o Status should be On Shelf or Checked Out (or other statuses you can imagine).
   
 */
class Book
{
    public string Title { get; set; }
    public string Author { get; set; }
    public bool onShelfStatus  = true;
    public bool checkedoutStatus  = false;
    
    public DateTime? DueDate { get; set; } = null;

    public override string ToString()
    {
        string due = DueDate.HasValue ? DueDate.Value.ToShortDateString() : "N/A";
        return $"{Title,-30} | {Author,-20} | {onShelfStatus,-12} | Due: {due}";
    }
}  