using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject Health1;
    public GameObject Health2;
    public GameObject Health3;
    public GameObject twoX;
    public GameObject fourX;
    public GameObject sixX;
    public AudioClip BaloonPop;
    public AudioClip doubleScore;
    public AudioClip heartLost;
    
    public static int TotalScore = 0;
    public static bool enemyDie = false;
    public static int life = 3;

    int ScoreMultiplier = 0;
    int currentBaloonsPoped = 0;

    public Text ScoreBoard;
    public Text BaloonsBoard;

    
    float timeRemaining = 5f;
    int Highscore = 0 ;
    AudioSource playAudio;


    private void Start()
    {
        playAudio = GetComponent<AudioSource>();
        TotalScore = PlayerPrefs.GetInt("totalscore", 0);
        ScoreBoard.text = TotalScore.ToString();
        Highscore = PlayerPrefs.GetInt("topscore", 0);
        currentBaloonsPoped = PlayerPrefs.GetInt("baloonspopped", 0);
        BaloonsBoard.text = currentBaloonsPoped.ToString();
    }
    // Update is called once per frame
    void Update()
    {
       if (enemyDie)
        {
            timeRemaining -= 1f*Time.deltaTime;
        }

        if(Health3.activeSelf == false && Health2.activeSelf == true)
        {
            life = 2;
        } else if(Health2.activeSelf == false)
        {
            life = 1;
        }
    }

    public void UpdateScore(int score)
    {
        if(timeRemaining > 0 && TotalScore > 1)
        {
            if(ScoreMultiplier == 0)
            {
                playAudio.PlayOneShot(doubleScore);
                twoX.SetActive(true);
                fourX.SetActive(false);
                sixX.SetActive(false);
                score = score * 2;
                ScoreMultiplier += 1;
                
            }else if(ScoreMultiplier == 1) 
                {
                playAudio.PlayOneShot(doubleScore);
                twoX.SetActive(false);
                fourX.SetActive(true);
                sixX.SetActive(false);
                ScoreMultiplier += 1;
                score = score * 4;
                
            }else if(ScoreMultiplier == 2)
            {
                playAudio.PlayOneShot(doubleScore);
                twoX.SetActive(false);
                fourX.SetActive(false);
                sixX.SetActive(true);
                score = score * 6;
                
            }
        } else if(timeRemaining <=0)
        {
            twoX.SetActive(false);
            fourX.SetActive(false);
            sixX.SetActive(false);
            timeRemaining = 5f;
            ScoreMultiplier = 0;
            enemyDie = false;
        }
        
        TotalScore += score;
        ScoreBoard.text = TotalScore.ToString();
    }

    public void BaloonsPopedUpdate(int i)
    {
        playAudio.PlayOneShot(BaloonPop);
        currentBaloonsPoped += i;

        BaloonsBoard.text = currentBaloonsPoped.ToString();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(life);
        if(collision.tag == "baloon")
        {
            if(life == 3)
            {
                playAudio.PlayOneShot(heartLost);
                Health3.SetActive(false);
            }else if(life == 2)
            {
                playAudio.PlayOneShot(heartLost);
                Health2.SetActive(false);
            }else if(life == 1)
            {
                playAudio.PlayOneShot(heartLost);
                PlayerPrefs.SetInt("baloonspopped", currentBaloonsPoped);
                Health1.SetActive(false);
                if(TotalScore > Highscore)
                {
                    Highscore = TotalScore;
                    PlayerPrefs.SetInt("topscore", Highscore);
                }
                SceneManager.LoadScene(2);
            }

        }
    }
}
