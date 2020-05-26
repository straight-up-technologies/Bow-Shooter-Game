using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElectricWave : MonoBehaviour
{
    public static int ElectricWaveRemainig = 0;
    public AudioClip ElectricShock;

    public GameObject ElecWave_obj;
    public Text ElecWave;

    AudioSource GameAudio;
    private void Start()
    {
        GameAudio = GetComponent<AudioSource>();
        ElectricWaveRemainig = PlayerPrefs.GetInt("electriccount", 0);
        ElecWave.text = ElectricWaveRemainig.ToString();
    }

    
    public void CreateShockwave()
    {
        if (ElectricWaveRemainig > 0)
        {
            GameAudio.PlayOneShot(ElectricShock);
            ElecWave_obj.SetActive(true);
            ElectricWaveRemainig -= 1;
            ElecWave.text = ElectricWaveRemainig.ToString();
            PlayerPrefs.SetInt("electriccount", ElectricWaveRemainig);
            StartCoroutine(ExecuteAfterTime(2));
        }
        

    }

    IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);

        // Code to execute after the delay
        ElecWave_obj.SetActive(false);
    }
}
