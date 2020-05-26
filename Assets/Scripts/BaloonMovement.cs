using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaloonMovement : MonoBehaviour
{
    public static float floatStrength = 0.1f; // change to your liking 
    public float TimeOnSreen = 10f;
   // public float floatTime = 10f;
    public float DestroyTime = 100f;
    float TimeRemaining;
    Animator anim;
   
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        TimeRemaining = TimeOnSreen;
       
    }

    private void Update()
    {
        TimeRemaining -= 1f * Time.deltaTime;
        
        if(TimeRemaining <=0)
        {
            DestroyGameobject();
        }
        //Debug.Log(floatStrength);
    }

    private void FixedUpdate()
    {
        GetComponent<Rigidbody2D>().AddForce(Vector3.up * floatStrength);
    }

    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Arrow" || collision.tag == "sonar" || collision.tag == "lightning")
        {
            
            anim.SetBool("Pop", true);
            GameObject.Find("GameManager").GetComponent<GameManager>().UpdateScore(1);
            GameObject.Find("GameManager").GetComponent<GameManager>().BaloonsPopedUpdate(1);
            GameManager.enemyDie = true;
            DestroyGameobject();
            
        }
    }

  void DestroyGameobject()
    {
        Destroy(gameObject, DestroyTime*Time.deltaTime);
    }
}
