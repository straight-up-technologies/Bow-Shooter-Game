using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockWave : MonoBehaviour
{
    public static int ShockWaveRemainig = 0;
    public GameObject ShockWave_obj;
    public AudioClip ShockWav;

    AudioSource playClip;

    private void Start()
    {
       
    }

    void Update()
    {
        CreateShockwave();
    }

    public void IncreaseShock(int i)
    {
        ShockWaveRemainig += i;
    }

    public void CreateShockwave()
    {
        if(ShockWaveRemainig > 0)
        {
            playClip = GetComponent<AudioSource>();
            playClip.PlayOneShot(ShockWav);
            ShockWave_obj.SetActive(true);
            transform.localScale += new Vector3(0.2f, 0.2f, 0.2f);
            if (transform.localScale.x > 6f)
            {
                ShockWaveRemainig -= 1;
                PlayerPrefs.SetInt("shockcount", ShockWaveRemainig);
                ShockWave_obj.SetActive(false);
                transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
            }
        }
        else
        {
            ShockWave_obj.SetActive(false); 
        }
 
    }
}
