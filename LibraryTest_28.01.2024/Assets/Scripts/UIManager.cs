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
    [SerializeField] AudioClip[] audioClips;
    
    TextMeshProUGUI introTxt;
    [SerializeField] string introContent;

    AudioSource audioSource;

    private void Awake()
    {
        introTxt = introPanel.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
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
        introPanel.transform.GetChild(2).GetComponent<CanvasGroup>().DOFade(1, 0.5f).SetEase(Ease.OutBack);
    }

    IEnumerator ReleaseIntroTxtRoutine()
    {
        yield return new WaitForSeconds(1);

        foreach (char c in introContent) 
        {
            float randomDelay = Random.Range(0.1f, 0.3f);
            
            introTxt.text += c.ToString();
            audioSource.pitch = Random.Range(0.7f, 1f);
            audioSource.PlayOneShot(audioClips[Random.Range(0,audioClips.Length)]);

            if (c.ToString() == ",")
            {
                yield return new WaitForSeconds(0.5f);
            }
            else
            {
                yield return new WaitForSeconds(randomDelay);
            }
        }
    }

    public void OpenMenuPanelFNC()
    {
        introPanel.GetComponent<RectTransform>().DOScale(Vector3.zero, 0.1f);
        menuPanel.GetComponent<RectTransform>().DOScale(Vector3.one, 0.1f);
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
