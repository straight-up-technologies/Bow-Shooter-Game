using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateBaloons : MonoBehaviour
{
    public GameObject BlueBaloon;
    public GameObject GreenBaloon;
    public GameObject OrangeBaloon;
    public GameObject PinkBaloon;
    public GameObject PurpleBaloon;
    public GameObject RedBaloon;
    public GameObject YellowBaloon;
    public GameObject[] Baloons;
    private GameObject Baloon;
    public float timer = 5f;
    public float PushStrength = 0.5f;

    float RemainingTime;
    // Start is called before the first frame update
    void Start()
    {
        RemainingTime = timer;
        Baloons = new GameObject[7];
        Baloons[0] = BlueBaloon;
        Baloons[1] = GreenBaloon;
        Baloons[2] = OrangeBaloon;
        Baloons[3] = PinkBaloon;
        Baloons[4] = PurpleBaloon;
        Baloons[5] = RedBaloon;
        Baloons[6] = YellowBaloon;
    }

    // Update is called once per frame
    void Update()
    {
        RemainingTime -= 1f * Time.deltaTime;
        if (RemainingTime <= 0)
        {
            GenerateBaloon();
            PushStrength += 0.5f;
            RemainingTime = timer;
        }
        BaloonMovement.floatStrength = PushStrength;

    }

    void GenerateBaloon()
    {
        Baloon = GameObject.Instantiate(Baloons[UnityEngine.Random.Range(0,6)]);
        Baloon.transform.position = gameObject.transform.position;
    }
}
