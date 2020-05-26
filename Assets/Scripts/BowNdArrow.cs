using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowNdArrow : MonoBehaviour
{
    public GameObject Bow;
    public GameObject Arrow;
    public GameObject BowString;

    public GameObject TopPoint;
    public GameObject BottomPoint;

    public AudioClip BowStretch;
    public AudioClip ArrowThrow;
    
    public Collider2D StartZone;
    public Collider2D EndZone;

    public Color C1 = Color.white;
    public Color C2 = Color.red;

    private LineRenderer Line01;
    private LineRenderer Line02;

    private bool ActivateDrawingBowString = false;
    private bool isPressed, isArrowLeft;
    
    public GameObject MinBowString;
    public GameObject MaxBowString;

    public bool AllowFizzle = true;
    public float FizzleSpeed = 10;
    public float mouseSensitivityY = 1;

    private GameObject CurrentArrow;

    public float FireSpeed = 30;

    public float RestTime = 0;
    private float CountDownTimer;
    AudioSource playAudio;
    int stretch = 0;
    // Use this for initialization
    void Start()
    {
        playAudio = GetComponent<AudioSource>();
        Line01 = BowString.transform.GetChild(0).GetComponent<LineRenderer>();
        Line02 = BowString.transform.GetChild(1).GetComponent<LineRenderer>();
        GenerateBowString(MinBowString.transform.position);
    }

    void GenerateBowString(Vector3 ArrowPosition)
    {

        Vector3 TopPointPosition = TopPoint.transform.position;
        Vector3 BottomPointPosition = BottomPoint.transform.position;

        if (ActivateDrawingBowString)
        {
            Line02.gameObject.SetActive(true);
            Line01.SetPosition(0, TopPointPosition);
            Line01.SetPosition(1, ArrowPosition);
            Line02.SetPosition(0, ArrowPosition);
            Line02.SetPosition(1, BottomPointPosition);
        }
        else
        {
            Line01.SetColors(C1, C1);
            Line02.gameObject.SetActive(false);
            Line01.SetPosition(0, TopPointPosition);
            Line01.SetPosition(1, BottomPointPosition);
        }
    }


    private float DifferenceAngleFunction(float Angle01, float Angle02)
    {
        float Angle = 0;
        if (Angle02 >= 0)
        {
            Angle = Angle01 - Angle02;
        }
        else if (Angle02 < 0)
        {
            Angle = Angle01 + Angle02;
        }
        return Angle;
    }
    private Vector3 ArrowPositionFunction()
    {
        Vector3 ClickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        ClickPosition.z = 0;

        Vector3 MinToMaxDir = (MaxBowString.transform.position - MinBowString.transform.position).normalized;
        float MinToMaxAngle = Mathf.Atan2(MaxBowString.transform.position.y - MinBowString.transform.position.y, MaxBowString.transform.position.x - MinBowString.transform.position.x);
        float MinToClickAngle = Mathf.Atan2(ClickPosition.y - MinBowString.transform.position.y, ClickPosition.x - MinBowString.transform.position.x);

        float DifferenceAngle = Mathf.Abs(DifferenceAngleFunction(MinToMaxAngle, MinToClickAngle));

        if (DifferenceAngle >= Mathf.PI * 0.5f)
        {
            ClickPosition = MinBowString.transform.position + (MinToMaxDir * 0.01f);
        }
        else if (AllowFizzle == false)
        {
            float FizzleDistance = Vector3.Distance(MaxBowString.transform.position, MinBowString.transform.position);
            float CurrentDistance = Vector3.Distance(ClickPosition, MinBowString.transform.position);

            if (CurrentDistance > FizzleDistance)
            {
                Vector3 MinToClickDir = (ClickPosition - MinBowString.transform.position).normalized;
                ClickPosition = MinBowString.transform.position + (MinToClickDir * FizzleDistance);
                Line01.SetColors(C1, C2);
                Line02.SetColors(C1, C2);
                if(stretch == 0)
                {
                    playAudio.PlayOneShot(BowStretch);
                    stretch = 1;
                }

                
            } else if(CurrentDistance < FizzleDistance)
            {
                Line01.SetColors(C1, C1);
                Line02.SetColors(C1, C1);
                stretch = 0;
            }
        }

        return ClickPosition;
    }

    private void GenerateArrow()
    {
        CurrentArrow = GameObject.Instantiate(Arrow);
        CurrentArrow.transform.rotation = Bow.transform.rotation;

        Vector3 ArrowPosition = ArrowPositionFunction();
        CurrentArrow.transform.position = ArrowPosition;
        ActivateDrawingBowString = true;
    }

    private void MoveGeneratedArrow()
    {
        if (!AllowFizzle || EndZone.OverlapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition)))
        {
            Vector3 ArrowPosition = ArrowPositionFunction();
            CurrentArrow.GetComponent<Rigidbody2D>().MovePosition(ArrowPosition);
            CurrentArrow.GetComponent<Rigidbody2D>().MoveRotation(Bow.transform.eulerAngles.z);
            GenerateBowString(ArrowPosition);
        }
        else
        {
            Fire(true);
        }

    }

    private void Fire(bool Fizzle)
    {
        playAudio.PlayOneShot(ArrowThrow);
        stretch = 0;
        CountDownTimer = RestTime;
        float FinalSpeed;
        Vector3 ArrowPosition = ArrowPositionFunction();

        if (Fizzle)
        {
            FinalSpeed = Random.Range(-FizzleSpeed, FizzleSpeed);
            
        }
        else
        {
            float MaxDistance = Vector3.Distance(MaxBowString.transform.position, MinBowString.transform.position);
            float CurrentDistance = Vector3.Distance(ArrowPosition, MinBowString.transform.position);
            float BoostSpeed = CurrentDistance / MaxDistance;
            FinalSpeed = FireSpeed * BoostSpeed;
        }


        CurrentArrow.GetComponent<Rigidbody2D>().isKinematic = false;
        CurrentArrow.GetComponent<Rigidbody2D>().velocity = Bow.transform.rotation * new Vector2(FinalSpeed, 0);

        GenerateBowString(MinBowString.transform.position);
        ActivateDrawingBowString = false;
    }

    

    void Update()
    {

        float moveUD = Input.GetAxis("Mouse Y") * mouseSensitivityY * Time.deltaTime;
        Vector3 mouse = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        transform.position = new Vector3(-6.5f, mouse.y, transform.position.z);


        if (!ActivateDrawingBowString)
        {
            GenerateBowString(MinBowString.transform.position);
        }
        CountDownTimer -= Time.deltaTime;
        if (Input.GetMouseButtonDown(0))
        {
            if (CountDownTimer <= 0)
            {
                
                if (StartZone.OverlapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition)))
                {
                    GenerateArrow();
                }
            }
            
        }
        else if (Input.GetMouseButton(0))
        {
            if (ActivateDrawingBowString == true)
            {
                MoveGeneratedArrow();
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (ActivateDrawingBowString == true)
            {
                Fire(false);
            }
        }
        
    }
}
