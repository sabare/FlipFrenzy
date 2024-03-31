
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class mvmto : MonoBehaviour
{
    public Vector2 startPos;
    public Vector2 direction;

    public float pmeter = 0.028f;
    private float touchGrnddesk =0;
    public float revSpeed = -360f;
    public float angle  = 0f;
    private int score = 0;

    public bool isGroundTouch = true;
    private bool canSwipe = false;
    private bool firstTime = true;
    

    public camShake cam;
    public camShake cam2;
    public manager manager;
    public ParticleSystem part;
    public Transform tf;
    public Scoreval scval;

    void Update()
    {
        tf.position = new Vector2(GetComponent<Transform>().position.x , GetComponent<Transform>().position.y-1f);
        if(!isGroundTouch){

            if(direction.x>0)GetComponent<Rigidbody2D>().angularVelocity = revSpeed;
            else GetComponent<Rigidbody2D>().angularVelocity = -revSpeed;
        }
        
        
        
        if (Input.touchCount > 0 && canSwipe)
        {
            Touch touch = Input.GetTouch(0);
            switch (touch.phase)
            {
                
                case TouchPhase.Began:
                    
                    startPos = touch.position;
                    direction=new Vector2(0,0);
                    break;

                 
                case TouchPhase.Moved:
                    
                    direction = touch.position - startPos;
                    break;

                case TouchPhase.Ended:
                    
                    if(Mathf.Sqrt(direction.x*direction.x+direction.y*direction.y)>90 && direction.y>0){

                        isGroundTouch=false;
                        canSwipe =false;
                        touchGrnddesk = 1;
                        fun();
                    }
                    break;
            }

        }


        
    }

    public void fun(){
        
        GetComponent<Rigidbody2D>().velocity = new Vector2(direction.x*pmeter, direction.y*pmeter);
    
    }

    void OnCollisionEnter2D(Collision2D collision) {

        isGroundTouch=true;
        if(collision.gameObject.tag == "stage") {

            ContactPoint2D contact = collision.contacts[0];
            Vector2 pos = contact.point;
            canSwipe = true;
            if(touchGrnddesk == 1){
                
                touchGrnddesk=0;
                StartCoroutine("findAngle");
            }

            
        }


        if(collision.gameObject.tag == "floor" && firstTime){

            canSwipe = false;
            firstTime = false;
            StopCoroutine("findAngle");
            StartCoroutine("waitForstable");
        }
    }


    IEnumerator findAngle(){

        
        while(!GetComponent<Rigidbody2D>().IsSleeping())yield return null;
        angle = Vector3.Angle(Vector3.up, transform.up);

        if(angle>175 || angle<5 ){

            StartCoroutine(cam.goodShake(0.15f,0.1f));
            StartCoroutine(cam2.goodShake(0.15f,0.1f));
            part.Play();
            score++;
            
        }
        else {

            StartCoroutine("waitForstable");
            
        }

        scval.scoreChange(score); 
    }

    IEnumerator waitForstable(){

        while(!GetComponent<Rigidbody2D>().IsSleeping())yield return null;
        StartCoroutine(cam.badShake(0.15f,0.1f));
        StartCoroutine(cam2.badShake(0.15f,0.1f));
        score = 0;
        scval.scoreChange(score);
        GetComponent<Rigidbody2D>().position = new Vector3(0f,1.4f,6.0f);
        GetComponent<Rigidbody2D>().rotation = 0f;
        touchGrnddesk = 0f;
        firstTime = true;
    }

}