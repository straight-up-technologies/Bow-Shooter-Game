using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SocialButton : MonoBehaviour
{
    public GameObject powerPanel;
    public GameObject FbButton;
    public GameObject InstaButton;

    int pref = 0;
    AudioSource PlayClip;

    public AudioClip PanelOpen;
    // Start is called before the first frame update
    private void Start()
    {
        pref = PlayerPrefs.GetInt("pref", 0);
        PlayClip = GetComponent<AudioSource>();
        if(pref == 0)
        {
            InstaButton.SetActive(true);
            FbButton.SetActive(false);
        }else if (pref == 1)
        {
            InstaButton.SetActive(false);
            FbButton.SetActive(true);
        }
    }

    

    public void ActivatePowerPanel()
    {
        PlayClip.PlayOneShot(PanelOpen);
        powerPanel.SetActive(true);
    }

    public void FacebookOpen()
    {
        PlayerPrefs.SetInt("pref", 0);
        PlayClip.PlayOneShot(PanelOpen);
        Application.OpenURL("https://www.facebook.com/thestraightuptech/");
        
    }

    public void InstaOpen()
    {
        PlayerPrefs.SetInt("pref", 1);
        PlayClip.PlayOneShot(PanelOpen);
        Application.OpenURL("https://www.instagram.com/thestraightuptech/");
    }
}
