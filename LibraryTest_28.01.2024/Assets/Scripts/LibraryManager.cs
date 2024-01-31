using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LibraryManager : MonoBehaviour
{
    public TMP_InputField title;
    public TMP_InputField author;
    public TMP_InputField isbn;
    public TMP_InputField copyCount;
    public float duration = 30;

    int bookFoundCount;
    int bookMissingCount;
    int authorFoundCount;
    int authorMissingCount;
    int copyAvailable;
    int copyCheckedOut;
    int copyCheckedIn;
    int copyNumber;
    Vector2 bookOut;

    [HideInInspector] public List<Book> inventory;
    [HideInInspector] public List<Vector2> copyVector = new List<Vector2>();
    [HideInInspector] public List<Vector2> checkOutVector;
    [HideInInspector] public List<Vector2> checkInVector;
    [HideInInspector] public Dictionary<string, int> checkOutDic = new Dictionary<string, int>();
    [HideInInspector] public Dictionary<string, int> copyOutDic;
    [HideInInspector] public Dictionary<string, int> checkInDic = new Dictionary<string, int>();
    [HideInInspector] public Dictionary<string, Book> titleDic = new Dictionary<string, Book>();
    [HideInInspector] public int bookIndex;

    Book book;
    UIManager uiManager;

    private void Awake()
    {
        book = FindObjectOfType<Book>();
        uiManager=FindObjectOfType<UIManager>();
    }

    private void Start()
    {
            
    }

    public void AddBookFNC()
    {
        if (title.text != string.Empty && author.text != string.Empty && isbn.text != string.Empty && copyCount.text != string.Empty)
        {
            if(!titleDic.ContainsKey(title.text))
            {
                bookIndex = inventory.Count + 1;
                inventory.Add(new Book(bookIndex, title.text, author.text, UInt64.Parse(isbn.text), int.Parse(copyCount.text)));
                Debug.Log("item added succesfully");

                titleDic.Add(title.text, inventory[inventory.Count - 1]);
                Debug.Log("TitleDic eleman sayýsý: " + titleDic.Count);
                
                titleDic[title.text].CopyAvailable = int.Parse(copyCount.text);


                for (int i = 1; i <= titleDic[title.text].CopyCount; i++)
                {
                    copyVector.Add(new Vector2((float)bookIndex, (float)i));
                }
                Debug.Log("CopyVector eleman sayýsý: " + copyVector.Count);
            }
            else
            {
                Debug.Log("Open Duplicate Information/Book Exist Panel");
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

    public void ListAllFNC()
    {
        foreach (Book book in inventory)
        {
            uiManager.listBookTxt.text += book.BookIndex.ToString() + ". " + book.Title + "\n\tAuthor: " + "\t" + book.Author + "\n\tISBN: " + "\t" + book.ISBN +
                        "\n\tCopyCount: " + "\t" + book.CopyCount + "\n" + "\n\tCopyAvailable: " + "\t" + book.CopyAvailable + "\n" + new string('-', 150) + "\n\n";
            //Debug.Log(book.Title + " " + book.Author + " " + book.CopyCount + " " + book.ISBN);
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
                        "\n\tCopyCount: " + "\t" + book.CopyCount + "\n" + "\n\tCopyAvailable: " + "\t" + book.CopyAvailable + "\n" + new string('-', 150) + "\n\n";

                    //checkOutDic.Add(book.Title, book.CopyAvailable);

                    Debug.Log("CopyNumber to Check-Out: " + checkOutDic[book.Title].ToString());

                    //int copyNumber = checkOutDic[book.Title];

                    //bookOut = new Vector2(bookIndex, copyAvailable);
                    //copyOutDic[book.Title] = book.CopyAvailable;

                    //if (uiManager.checkOutPanel.transform.GetChild(5).GetComponent<Button>()
                    //{

                    //}
                }
                else
                {
                    bookMissingCount++;                        
                }              
            }
            if(bookMissingCount == inventory.Count && bookFoundCount == 0) 
            {
                uiManager.bookMissingPanel.SetActive(true);
                //uiManager.searchTitleInput.text = string.Empty;                
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
                        "\n\tCopyCount: " + "\t" + book.CopyCount + "\n" + "\n\tCopyAvailable: " + "\t" + book.CopyAvailable + "\n" + new string('-', 150) + "\n\n";
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
            }
        }
    }

    /*
    public void BookCheckOut()
    {   
        foreach (var co in checkOutDic) 
        {
            if (checkOutDic.ContainsKey(uiManager.searchTitleInput.text))
            {
                if (copyNumber > 0)
                {
                    Debug.Log("Check-in Panel");
                    uiManager.checkInTxt.text += book.BookIndex.ToString() + ". " + book.Title + "\n\tCopyNumber: " + "\t" + checkOutDic[book.Title] + "\t\tTimer: " + "\t" + duration + "\n" + new string('-', 150) + "\n\n";
                    //checkInVector.Add(bookOut);
                    //checkOutVector.Remove(bookOut);
                    //copyAvailable = (int)bookOut.y - 1;
                    //Debug.Log("BookOut: " + checkOutDic[bookOut].Title);

                    checkInDic.Add(book.Title, book.CopyAvailable);
                }
            }
        }               
    }*/
}    

