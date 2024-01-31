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
    public float duration;

    int bookFoundCount;
    int bookMissingCount;
    int authorFoundCount;
    int authorMissingCount;
    int copyAwailable;
    int copyCheckedOut;
    int copyCheckedIn;
    Vector2 bookOut;
    [HideInInspector] public List<Book> inventory;
    [HideInInspector] public List<Vector2> bookCopy;
    [HideInInspector] public List<Vector2> bookCheckOut;
    [HideInInspector] public List<Vector2> bookCheckIn;
    [HideInInspector] public Dictionary<int, Book> bookRef;
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
        if (title.text != string.Empty && author.text != string.Empty && isbn.text != string.Empty && copyCount.text != string.Empty)
        {
            //book.Title = title.text;
            //book.Author = author.text;
            //book.ISBN = UInt64.Parse(isbn.text);
            //book.CopyCount = int.Parse(copyCount.text);
            //book.BookIndex = bookIndex;
            bookIndex = inventory.Count + 1;
            copyAwailable=int.Parse(copyCount.text);
            inventory.Add(new Book(bookIndex, title.text, author.text, UInt64.Parse(isbn.text), int.Parse(copyCount.text)));
            Debug.Log("item added succesfully");

            for (int i = 1; i <= int.Parse(copyCount.text); i++)
            {
                bookCopy.Add(new Vector2((float)bookIndex, (float)i));
            }
        }
        else
        {
            Debug.Log("Open Missing Information Panel");
            //title.text = string.Empty;
            //author.text = string.Empty;
            //isbn.text = string.Empty;
            //copyCount.text = string.Empty;
        }
        
    }

    public void SearchBookFNC()
    {
        string searchBookResult = uiManager.searchTitleInput.text.ToLower();
        bookFoundCount = 0;
        bookMissingCount = 0;

        foreach (Book book in inventory)
        {
            if (uiManager.searchTitleInput.text == string.Empty)
            {
                uiManager.bookMissingPanel.SetActive(true);
                uiManager.searchTitleInput.text = string.Empty;
                break;
            }

            if (uiManager.searchTitleInput.text != string.Empty)
            {    
                if (book.Title.ToLower().Contains(searchBookResult))
                {
                    bookFoundCount++;
                    Debug.Log("book exist");
                    uiManager.checkOutTxt.text += book.BookIndex.ToString() + ". " + book.Title + "\n\tAuthor: " + "\t" + book.Author + "\n\tISBN: " + "\t" + book.ISBN + 
                        "\n\tCopyCount: " + "\t" + book.CopyCount + "\n" + "\n\tCopyAwailable: " + "\t" + copyAwailable + "\n" + new string('-', 150) + "\n\n";

                    bookOut = new Vector2(bookIndex, copyAwailable);
                    BookCheckOut();
                }
                else
                {
                    bookMissingCount++;                        
                }              
            }
            if(bookMissingCount == inventory.Count && bookFoundCount == 0) 
            {
                uiManager.bookMissingPanel.SetActive(true);
                uiManager.searchTitleInput.text = string.Empty;                
                //bookQueryCount++;
            }
        }
        
    }

    public void SearchAuthorFNC()
    {
        string searchAuthorResult = uiManager.searchAuthorInput.text.ToLower();      
        authorFoundCount = 0;
        authorMissingCount = 0;

        foreach (Book book in inventory)
        {
            if (uiManager.searchAuthorInput.text == string.Empty)
            {
                uiManager.authorMissingPanel.SetActive(true);
                uiManager.searchAuthorInput.text = string.Empty;
                break;
            }

            if (uiManager.searchAuthorInput.text != string.Empty)
            {
                if (book.Author.ToLower().Contains(searchAuthorResult))
                {
                    authorFoundCount++;
                    Debug.Log("author exist");
                    uiManager.checkOutTxt.text += book.BookIndex.ToString() + ". " + book.Title + "\n\tAuthor: " + "\t" + book.Author + "\n\tISBN: " + "\t" + book.ISBN +
                        "\n\tCopyCount: " + "\t" + book.CopyCount + "\n" + "\n\tCopyAwailable: " + "\t" + copyAwailable + "\n" + new string('-', 150) + "\n\n";
                }
                else
                {
                    authorMissingCount++;
                }
            }
            if (authorMissingCount == inventory.Count && authorFoundCount == 0)
            {
                uiManager.authorMissingPanel.SetActive(true);
                uiManager.searchAuthorInput.text = string.Empty;
                //bookQueryCount++;
            }
        }
    }

    public void BookCheckOut()
    {
        if ((int)bookOut.y > 0)
        {
            bookCheckIn.Add(bookOut);
            bookCheckOut.Remove(bookOut);
            copyAwailable = (int)bookOut.y - 1;
            Debug.Log("BookOut: " + bookRef[bookIndex]);
        }        
    }
}    

