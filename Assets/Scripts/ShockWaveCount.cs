using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShockWaveCount : MonoBehaviour
{
    public Text WaveCount;

    int shockRemaining;


    private void Start()
    {
        shockRemaining = PlayerPrefs.GetInt("shockcount", 0);
        //Debug.Log("ShockRemain :" + shockRemaining);
        ShockWave.ShockWaveRemainig = shockRemaining;
    }
    // Update is called once per frame
    void Update()
    {
        WaveCount.text = shockRemaining.ToString();
        shockRemaining = ShockWave.ShockWaveRemainig;
    }
}
