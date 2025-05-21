namespace ConsoleApp1;

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

    public override string ToString()
    {
        string due = DueDate.HasValue ? DueDate.Value.ToShortDateString() : "N/A";
        return $"{Title,-30} | {Author,-20} | {Status,-12} | Due: {due}";
    }
}  