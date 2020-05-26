using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverControl : MonoBehaviour
{
    public Text scoreText;
    public Text HiScoreText;
    public Text BaloonText;

    public GameObject BaloonAddAnim;
    public GameObject GameOverPanel;
    public GameObject RestartButton;
    public GameObject GrtBaloonsButton;
    public GameObject ContinueButton;
    public GameObject NoThBtn;
    public AudioClip PanelOpen;
    public AudioClip Clapping;

    AudioSource PlayClip;

    int Highscore;
    int Baloons;
    int TotalScore;
   // Start is called before the first frame update
    void Start()
    {
        PlayClip = GetComponent<AudioSource>();
        TotalScore = GameManager.TotalScore;
        scoreText.text = TotalScore.ToString() ;
        Highscore = PlayerPrefs.GetInt("topscore", 0);
        HiScoreText.text = Highscore.ToString(); 
        Baloons = PlayerPrefs.GetInt("baloonspopped", 0);
        BaloonText.text = Baloons.ToString();
        if(TotalScore >= Highscore)
        {
            ContinueButton.SetActive(true);
            StartCoroutine(ActivateAfterTime(3));
        }
        else
        {
            RestartButton.SetActive(true);
            GrtBaloonsButton.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        BaloonText.text = Baloons.ToString();
    }

    public void RestartGame()
    {
        PlayClip.PlayOneShot(PanelOpen);
        //PlayerPrefs.SetInt("totalscore", TotalScore);
        GameManager.life = 3;
        GameManager.enemyDie = false;
        SceneManager.LoadScene(1); 
    }

    public void continueGame()
    {
        PlayClip.PlayOneShot(PanelOpen);
        Baloons += 40;
        PlayClip.PlayOneShot(Clapping);
        PlayerPrefs.SetInt("baloonspopped", Baloons);
        BaloonAddAnim.SetActive(true);
        StartCoroutine(ExecuteAfterTime(2));
        PlayerPrefs.SetInt("totalscore", TotalScore);
        GameManager.life = 3;
        GameManager.enemyDie = false;
        SceneManager.LoadScene(1);
    }

    public void GoHome()
    {
        
        PlayClip.PlayOneShot(PanelOpen);
        PlayerPrefs.SetInt("totalscore", 0);
        SceneManager.LoadScene(0);
    }
    public void GetBaloons()
    {
        PlayClip.PlayOneShot(PanelOpen);
        Baloons += 40;
        PlayClip.PlayOneShot(Clapping);
        PlayerPrefs.SetInt("baloonspopped", Baloons);
        BaloonAddAnim.SetActive(true);
        StartCoroutine(ExecuteAfterTime(2));
    }

    public void FacebookOpen()
    {
        PlayClip.PlayOneShot(PanelOpen);
        Application.OpenURL("https://www.facebook.com/thestraightuptech/");
    }

    public void InstaOpen()
    {
        PlayClip.PlayOneShot(PanelOpen);
        Application.OpenURL("https://www.instagram.com/thestraightuptech/");
    }

    IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        // Code to execute after the delay
        BaloonAddAnim.SetActive(false);
    }

    IEnumerator ActivateAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        NoThBtn.SetActive(true);
    }
}
