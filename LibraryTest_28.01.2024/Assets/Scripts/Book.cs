using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Book : MonoBehaviour
{       
    public string Title { get; set; }
    public string Author { get; set; }
    public UInt64 ISBN { get; set; }
    public int CopyCount { get; set; }
    public int BookIndex { get; set; }
    //public int CopyReserved { get; set; }
    //public int CopyReturned { get; set; }
    public int CopyAvailable { get; set; }

    //public Dictionary<int, string> itemInventory = new Dictionary<int, string>();
    //public Dictionary<string, int> itemCount;
    //public Dictionary<string, int> itemReserved;
    //public Dictionary<string, int> itemReturned;

    public Book(int bookIndex, string title, string author, ulong ýsbn, int copyCount)
    {
        Title = title;
        Author = author;
        ISBN = ýsbn;
        CopyCount = copyCount;
        BookIndex = bookIndex;
        
        //itemInventory.Add(Title, CopyCount);
        //for (int i = 1; i <= CopyCount; i++)
        //{
        //    itemInventory.Add(i, title);
        //}
    }
}
