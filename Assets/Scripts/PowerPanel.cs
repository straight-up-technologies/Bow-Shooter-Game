using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerPanel : MonoBehaviour
{
    public GameObject ErrText;
    public GameObject CloseMyPanel;

    public int ShockWaveCost = 200;
    public int ElectricWaveCost = 400;
    public AudioClip buttns;
    public AudioClip BuyButton;
    public AudioClip errClip;

    AudioSource playClip;

    int totalShockWaveCost;
    int totalElectricWaveCost;

    int CurrentBaloons;
    int ShockWaveCount = 0;
    int ElectricWaveCount = 0;
    int EWcount = 0;
    int SWcount = 0;

    public Text ShockCount, TotalShockAmount;
    public Text ElectricCount, TotalElectricAmount;
    // Start is called before the first frame update
    void Start()
    {
        playClip = GetComponent<AudioSource>();
        CurrentBaloons = PlayerPrefs.GetInt("baloonspopped", 0);
        EWcount = PlayerPrefs.GetInt("electriccount", 0);
        SWcount = PlayerPrefs.GetInt("shockcount", 0);
        ShockCount.text = ShockWaveCount.ToString();
        ElectricCount.text = ElectricWaveCount.ToString();
    }

    public void increaseShockCount()
    {
        playClip.PlayOneShot(buttns);
        ShockWaveCount += 1;
        ShockCount.text = ShockWaveCount.ToString();
        totalShockWaveCost = ShockWaveCost * ShockWaveCount;
        TotalShockAmount.text = totalShockWaveCost.ToString();
        
    }

    public void decreaseShockCount()
    {
        playClip.PlayOneShot(buttns);
        if (ShockWaveCount > 1)
        {
            ShockWaveCount -= 1;
            ShockCount.text = ShockWaveCount.ToString();
            totalShockWaveCost = ShockWaveCost * ShockWaveCount;
            TotalShockAmount.text = totalShockWaveCost.ToString();
        }
      
    }

    public void increaseElectricCount()
    {
        playClip.PlayOneShot(buttns);
        ElectricWaveCount += 1;
        ElectricCount.text = ElectricWaveCount.ToString();
        totalElectricWaveCost = ElectricWaveCost * ElectricWaveCount;
        TotalElectricAmount.text = totalElectricWaveCost.ToString();
    }

    public void decreaseElecricCount()
    {
        playClip.PlayOneShot(buttns);
        if (ElectricWaveCount > 1)
        {
            ElectricWaveCount -= 1;
            ElectricCount.text = ElectricWaveCount.ToString();
            totalElectricWaveCost = ElectricWaveCost * ElectricWaveCount;
            TotalElectricAmount.text = totalElectricWaveCost.ToString();
        }
        
    }

    public void BuyShockWave()
    {
        
        if (totalShockWaveCost <= CurrentBaloons && ShockWaveCount >= 1)
        {
            playClip.PlayOneShot(BuyButton);
            SWcount += ShockWaveCount;
            CurrentBaloons = CurrentBaloons - totalShockWaveCost;
            PlayerPrefs.SetInt("baloonspopped", CurrentBaloons);
            PlayerPrefs.SetInt("shockcount", SWcount);
        }
        else
        {
            playClip.PlayOneShot(errClip);
            ErrText.SetActive(true);
            StartCoroutine(ClosePanel());
        }
    }

    public void BuyElectricWave()
    {
        
        if (totalElectricWaveCost <= CurrentBaloons && ElectricWaveCount >=1)
        {
            playClip.PlayOneShot(BuyButton);
            EWcount += ElectricWaveCount;

            CurrentBaloons = CurrentBaloons - totalElectricWaveCost;
            PlayerPrefs.SetInt("baloonspopped", CurrentBaloons);
            PlayerPrefs.SetInt("electriccount", EWcount);
        }
        else
        {
            playClip.PlayOneShot(errClip);
            ErrText.SetActive(true);
            StartCoroutine(ClosePanel());
        }
    }

    public void CloseThePanel()
    {
        playClip.PlayOneShot(buttns);
        CloseMyPanel.SetActive(false);
    } 

    IEnumerator ClosePanel()
    {
        yield return new WaitForSeconds(1);
        ErrText.SetActive(false);
    }
        
    
}
