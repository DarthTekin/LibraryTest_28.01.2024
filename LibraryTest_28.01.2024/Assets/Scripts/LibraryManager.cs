using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LibraryManager : MonoBehaviour
{
    public TMP_InputField title;
    public TMP_InputField author;
    public TMP_InputField isbn;
    public TMP_InputField copyCount;

    public List<Book> inventory;

    [HideInInspector] public int bookIndex;

    Book book;
    UIManager uiManager;

    private void Awake()
    {
        book = FindObjectOfType<Book>();
        uiManager=FindObjectOfType<UIManager>();
    }

    public void AddBookFNC()
    {
        //book.Title = title.text;
        //book.Author = author.text;
        //book.ISBN = UInt64.Parse(isbn.text);
        //book.CopyCount = int.Parse(copyCount.text);
        bookIndex = inventory.Count + 1;
        //book.BookIndex = bookIndex;
        inventory.Add(new Book(bookIndex, title.text, author.text, UInt64.Parse(isbn.text), int.Parse(copyCount.text)));
        Debug.Log("item added succesfully");
    }    
}
