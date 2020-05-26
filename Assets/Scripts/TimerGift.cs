using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerGift : MonoBehaviour
{
    AudioSource playClip;
    float seconds = 00f;
    float minutes = 1f;
    bool paused = false;
    int baloons;
    
    Animator anim;

    public AudioClip GiftGet;
    public AudioClip clapping;
    public Text Sec;
    public Text min;
    
    
    public GameObject timer;
    public GameObject baloonAnim;
   

   // int time = 0;

    private void Start()
    {
        
        playClip = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        baloons = PlayerPrefs.GetInt("baloonspopped", 0);
        if(Time.time <= 0f)
        {
            minutes = 1f;
        }else
        {
            minutes = PlayerPrefs.GetFloat("minutes", 0);
        }
            
       
       
        min.text = minutes.ToString();
        Sec.text = seconds.ToString();
       
    }

    private void Update()
    {
        if(paused)
        {
            //Debug.Log("minutes :" + minutes);
            timer.SetActive(false);
            anim.SetBool("ready", true);
            //Time.timeScale = 0;
        }
        else if (!paused)
        {
            seconds -= 1f * Time.deltaTime;

            if (seconds <= 0 && minutes > 0)
            {
                minutes -= 1f;
                seconds = 60f;
                //time += 1;
            }
            else if (seconds <= 0 && minutes <= 0)
            {
                minutes = minutes + 2;
                paused = true;
            }
            Sec.text = seconds.ToString();
            min.text = minutes.ToString();
        }
       
    }

    public void GetGift()
    {
        if(paused)
        {
            anim.SetBool("ready", false);
            timer.SetActive(true);
            baloonAnim.SetActive(true);
            playClip.PlayOneShot(clapping);
            playClip.PlayOneShot(GiftGet);
            StartCoroutine(ExecuteAfterTime(2));
            baloons += 40;
            PlayerPrefs.SetFloat("minutes", minutes);
            PlayerPrefs.SetInt("baloonspopped", baloons);
            paused = false;
        }
    }

    IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        // Code to execute after the delay
        baloonAnim.SetActive(false);
    }
}


