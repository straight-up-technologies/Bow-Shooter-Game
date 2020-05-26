using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElectricWaveCount : MonoBehaviour
{
    public Text WaveCount;
   
    // Update is called once per frame
    void Update()
    {
        WaveCount.text = ShockWave.ShockWaveRemainig.ToString();
    }
}
