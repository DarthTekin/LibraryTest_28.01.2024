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
    [SerializeField] GameObject menuPanel;
    [SerializeField] GameObject loginFailedPanel;
    [SerializeField] AudioClip[] audioClips;
    [SerializeField] string introContent;

    string username;
    TMP_InputField loginInput;
    TextMeshProUGUI introTxt;

    AudioSource audioSource;

    private void Awake()
    {
        introTxt = introPanel.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        loginInput = introPanel.transform.GetChild(2).GetComponent<TMP_InputField>();
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        introPanel.transform.GetChild(0).GetComponent<CanvasGroup>().DOFade(1, 0.5f);
        Invoke("ReleaseLoginBtnFNC", 0.3f);
        StartCoroutine(ReleaseIntroTxtRoutine());
        menuPanel.GetComponent<RectTransform>().localScale = Vector3.zero;
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

    public void ExitAdminPanelFNC()
    {
        Application.Quit();
    }

    public void MucisOffFNC()
    {
        audioSource.Stop();
    }

}
