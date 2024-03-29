using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;



[RequireComponent(typeof(AudioSource))]

public class UIManager : MonoBehaviour
{

    [SerializeField] GameObject introPanel;
    [SerializeField] GameObject loginFailedPanel;
    [SerializeField] GameObject menuPanel;
    [SerializeField] GameObject addBookPanel;
    [SerializeField] GameObject addNewPanel;
    [SerializeField] GameObject listBookPanel;
    public GameObject checkOutPanel;
    [SerializeField] GameObject checkInPanel;
    [SerializeField] AudioClip[] audioClips;
    [SerializeField] string introContent;

    public GameObject searchBookPanel;
    public GameObject searchAuthorPanel;
    public GameObject bookMissingPanel;
    public GameObject authorMissingPanel;
    public TextMeshProUGUI listBookTxt;
    public TextMeshProUGUI checkOutTxt;
    public TextMeshProUGUI checkInTxt;
    public Button checkOutBtn;
    string username;

    [HideInInspector] public TMP_InputField searchTitleInput;
    [HideInInspector] public TMP_InputField searchAuthorInput;


    TMP_InputField loginInput;
    TextMeshProUGUI introTxt;

    AudioSource audioSource;

    LibraryManager libraryManager;

    private void Awake()
    {
        introTxt = introPanel.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        loginInput = introPanel.transform.GetChild(2).GetComponent<TMP_InputField>();
        searchTitleInput = searchBookPanel.transform.GetChild(1).GetComponent<TMP_InputField>();
        searchAuthorInput = searchAuthorPanel.transform.GetChild(1).GetComponent<TMP_InputField>();
        libraryManager = FindObjectOfType<LibraryManager>();
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        introPanel.transform.GetChild(0).GetComponent<CanvasGroup>().DOFade(1, 0.5f);
        Invoke("ReleaseLoginBtnFNC", 0.3f);
        StartCoroutine(ReleaseIntroTxtRoutine());
        menuPanel.GetComponent<RectTransform>().localScale = Vector3.zero;
        addBookPanel.GetComponent<RectTransform>().localScale = Vector3.zero;
        listBookPanel.GetComponent<RectTransform>().localScale = Vector3.zero;
        checkOutPanel.GetComponent<RectTransform>().localScale = Vector3.zero;
        checkInPanel.GetComponent<RectTransform>().localScale = Vector3.zero;
    }

    void ReleaseLoginBtnFNC()
    {
        introPanel.transform.GetChild(3).GetComponent<CanvasGroup>().DOFade(1, 0.5f).SetEase(Ease.OutBack);
        introPanel.transform.GetChild(2).GetComponent<CanvasGroup>().DOFade(1, 0.5f).SetEase(Ease.OutBack);
    }

    IEnumerator ReleaseIntroTxtRoutine()
    {
        yield return new WaitForSeconds(1);

        foreach (char c in introContent)
        {
            float randomDelay = Random.Range(0.1f, 0.2f);

            introTxt.text += c.ToString();
            //audioSource.pitch = Random.Range(0.2f, 0.5f);
            audioSource.PlayOneShot(audioClips[Random.Range(0, audioClips.Length)], 0.1f);

            if (c.ToString() == ",")
            {
                yield return new WaitForSeconds(0.4f);
            }
            else
            {
                yield return new WaitForSeconds(randomDelay);
            }
        }
    }

    public void LoginControlFNC()
    {
        username = loginInput.text;

        if (username == "admin")
        {
            OpenMenuPanelFNC();
        }
        else
        {
            loginFailedPanel.SetActive(true);
        }
    }

    public void ReLoginFNC()
    {
        loginFailedPanel.SetActive(false);
        loginInput.text = string.Empty;
    }

    public void OpenMenuPanelFNC()
    {
        introPanel.GetComponent<RectTransform>().DOScale(Vector3.zero, 3f);
        menuPanel.GetComponent<RectTransform>().DOScale(Vector3.one, 1f);
    }

    public void OpenAddBookPanelFNC()
    {
        menuPanel.GetComponent<RectTransform>().DOScale(Vector3.zero, 0.02f).OnComplete(() => addBookPanel.GetComponent<RectTransform>().DOScale(Vector3.one, 0.2f));
    }

    public void OpenListBookPanelFNC()
    {
        menuPanel.GetComponent<RectTransform>().DOScale(Vector3.zero, 0.02f).OnComplete(() => listBookPanel.GetComponent<RectTransform>().DOScale(Vector3.one, 0.2f));
        listBookTxt.text = string.Empty;
        libraryManager.ListAllFNC();
    }

    public void OpenCheckOutPanelFNC()
    {
        menuPanel.GetComponent<RectTransform>().DOScale(Vector3.zero, 0.02f).OnComplete(() => checkOutPanel.GetComponent<RectTransform>().DOScale(Vector3.one, 0.2f));
    }
    
    public void OpenCheckInPanelFNC()
    {
        menuPanel.GetComponent<RectTransform>().DOScale(Vector3.zero, 0.02f).OnComplete(() => checkInPanel.GetComponent<RectTransform>().DOScale(Vector3.one, 0.2f));
        //libraryManager.BookCheckOut();
    }

    public void AddNewPanelFNC()
    {
        addNewPanel.SetActive(true);
        libraryManager.AddBookFNC();
    }

    public void AddNewToListBookPanelFNC()
    {
        addBookPanel.GetComponent<RectTransform>().DOScale(Vector3.zero, 0.02f).OnComplete(() => listBookPanel.GetComponent<RectTransform>().DOScale(Vector3.one, 0.2f));

        CloseAddNewPanel();

        listBookTxt.text = string.Empty;
        libraryManager.ListAllFNC();
    }

    public void CloseAddNewPanel()
    {
        libraryManager.title.text = string.Empty;
        libraryManager.author.text = string.Empty;
        libraryManager.isbn.text = string.Empty;
        libraryManager.copyCount.text = string.Empty;

        addNewPanel.SetActive(false);
    }

   

    public void AddBookToMenuPanelFNC()
    {
        addBookPanel.GetComponent<RectTransform>().DOScale(Vector3.zero, 0.02f).OnComplete(() => menuPanel.GetComponent<RectTransform>().DOScale(Vector3.one, 0.2f));
    }

    public void ListBookToAddBookPanelFNC()
    {
        listBookPanel.GetComponent<RectTransform>().DOScale(Vector3.zero, 0.02f).OnComplete(() => addBookPanel.GetComponent<RectTransform>().DOScale(Vector3.one, 0.2f));
        listBookTxt.text = string.Empty;
        libraryManager.AddBookFNC();
    }

    public void ListBookToMenuPanelFNC()
    {
        listBookPanel.GetComponent<RectTransform>().DOScale(Vector3.zero, 0.02f).OnComplete(() => menuPanel.GetComponent<RectTransform>().DOScale(Vector3.one, 0.2f));
    }

    public void CheckOutToMenuPanelFNC()
    {
        checkOutPanel.GetComponent<RectTransform>().DOScale(Vector3.zero, 0.02f).OnComplete(() => menuPanel.GetComponent<RectTransform>().DOScale(Vector3.one, 0.2f));
    }

    public void CheckOutToCheckInPanelFNC()
    {
        checkOutPanel.GetComponent<RectTransform>().DOScale(Vector3.zero, 0.02f).OnComplete(() => checkInPanel.GetComponent<RectTransform>().DOScale(Vector3.one, 0.2f));
    }

    public void CheckInToMenuPanelFNC()
    {
        checkInPanel.GetComponent<RectTransform>().DOScale(Vector3.zero, 0.02f).OnComplete(() => menuPanel.GetComponent<RectTransform>().DOScale(Vector3.one, 0.2f));
    }

    public void OpenSearchBookPanelFNC()
    {
        checkOutTxt.text = string.Empty;
        searchBookPanel.SetActive(true);
        bookMissingPanel.SetActive(false);
    }
    
    public void OpenSearchAuthorPanelFNC()
    {
        checkOutTxt.text = string.Empty;
        searchAuthorPanel.SetActive(true);
        authorMissingPanel.SetActive(false);
    }

    public void SearchTitleFNC()
    {   
        searchBookPanel.SetActive(false);
        libraryManager.SearchBookFNC();
    }
    
    public void SearchAuthorNameFNC()
    {   
        searchAuthorPanel.SetActive(false);
        libraryManager.SearchAuthorFNC();
    }

    public void CloseBookMissingPanelFNC()
    {
        bookMissingPanel.SetActive(false);
    }
    
    public void CloseAuthorMissingPanelFNC()
    {
        authorMissingPanel.SetActive(false);
    }

    public void ExitAdminPanelFNC()
    {
        Application.Quit();
    }

    public void MucisOffFNC()
    {
        audioSource.Stop();
    }

}
