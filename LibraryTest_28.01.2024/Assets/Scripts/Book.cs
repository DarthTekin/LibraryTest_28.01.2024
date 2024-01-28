using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Book : MonoBehaviour
{
    public int BookIndex { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public UInt64 ISBN { get; set; }
    public int CopyCount { get; set; }
    public int CopyReserved { get; set; }
    public int CopyReturned { get; set; }
    public int CopyAvailable { get; set; }

    public Book(int bookIndex, string title, string author, ulong ýSBN, int copyCount)
    {
        BookIndex = bookIndex;
        Title = title;
        Author = author;
        ISBN = ýSBN;
        CopyCount = copyCount;
        CopyAvailable = copyCount;
    }

    
  
}
