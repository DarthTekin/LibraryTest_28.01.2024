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

    //int bookQueryCount;
    int authorQueryCount;
    int bookFoundCount;
    int bookMissingCount;
    int authorFoundCount;
    int authorMissingCount;

    [HideInInspector] public List<Book> inventory;
    [HideInInspector] public List<Vector2> bookCopy;
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
            bookIndex = inventory.Count + 1;
            //book.BookIndex = bookIndex;
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

    public void SearchBook()
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
                    uiManager.checkOutTxt.text += book.BookIndex.ToString() + ". " + book.Title + "\n\tAuthor: " + "\t" + book.Author + "\n\tISBN: " + "\t" + book.ISBN + "\n\tCopyCount: " + "\t" + book.CopyCount + "\n" + new string('-', 100) + "\n\n";
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
        //if (uiManager.searchTitleInput.text == string.Empty || bookMissingCount == inventory.Count)
        //{
        //    uiManager.bookMissingPanel.SetActive(true);
        //    uiManager.searchTitleInput.text = string.Empty;
        //}
    }
}    

