using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuControl : MonoBehaviour
{
    public Text HighScoreText;
    public Text TotalBaloonsText;

    public AudioSource EntryClip;
    public AudioClip Swoosh;
    public AudioClip PlayButton;

   
    private float deltaTime = 0.0f;

    int range = 0;
    int HighScore;
    int TotalBaloons = 0;
    float volume = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
        range = PlayerPrefs.GetInt("range", 0);
        EntryClip.PlayOneShot(Swoosh, volume);
        HighScore = PlayerPrefs.GetInt("topscore", 0);
        TotalBaloons = PlayerPrefs.GetInt("baloonspopped", 0);
        HighScoreText.text = HighScore.ToString();
        TotalBaloonsText.text = TotalBaloons.ToString();
        
    }

    // Update is called once per frame
    void Update()
    {
        TotalBaloons = PlayerPrefs.GetInt("baloonspopped", 0);
        TotalBaloonsText.text = TotalBaloons.ToString();
        // calculate simple moving average for time to render screen.
        this.deltaTime += (Time.deltaTime - this.deltaTime) * 0.1f;
    }

    public void LoadGameScene()
    {
        GameManager.life = 3;
        EntryClip.PlayOneShot(PlayButton);
        SceneManager.LoadScene(1);
    }

    
}
