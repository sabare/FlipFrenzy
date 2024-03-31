
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class cardmvmt : MonoBehaviour
{
    public Vector2 startPos;
    public Vector2 direction;
    public Text m_Text;
    string message;

    public float pmeter = 0.028f;
    private float val =0;
    public float revSpeed = -360f;
    public float angle  = 0f;
    private int score = 0;
    private int position = 0;
    public bool isGroundTouch = true;
    private bool canSwipe = false;
    private bool firstTime = true;
    

    public camShake cam;
    public camShake cam2;
    public manager manager;
    public ParticleSystem part;
    public Transform tf;
    public Scoreval scval;
    public spriteChange spr;
    public spriteChange spr1;
    void Update()
    {
         m_Text.text = message + " " + GetComponent<Rigidbody2D>().IsSleeping()+ Vector3.Angle(Vector3.up, transform.up);
;
        //tf.position = new Vector2(GetComponent<Transform>().position.x , GetComponent<Transform>().position.y-1f);
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
                    message = "Begun ";
                    break;

                 
                case TouchPhase.Moved:
                    
                    direction = touch.position - startPos;
                    message = "Moving ";
                    break;

                case TouchPhase.Ended:
                    
                    message = "Ending ";
                    GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
                    
                    if(Mathf.Sqrt(direction.x*direction.x+direction.y*direction.y)>90 && direction.y>0){

                        isGroundTouch=false;
                        canSwipe =false;
                        val = 1;
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

        
        if(collision.gameObject.tag == "stage") {

            ContactPoint2D contact = collision.contacts[0];
            Vector2 pos = contact.point;
            canSwipe = true;
            if(val==1){
                angle = Vector3.Angle(Vector3.up, transform.up);
                if(angle<65 & angle>25 || angle<155 && angle>115){
                    StartCoroutine(cam.goodShake(0.15f,0.1f));
                    StartCoroutine(cam2.goodShake(0.15f,0.1f));
                    GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
                    tf.position = pos;
                    part.Play();
                    score++;
                    position+=1;
                    position%=14;
                    spr.changeIt(position);
                    spr1.changeSize(position);
                }
                else {

                    if(!isGroundTouch)StartCoroutine("waitForstable");
                             
                }

                scval.scoreChange(score);
                
            }

            
        }


        if(collision.gameObject.tag == "floor" && firstTime){

            canSwipe = false;
            firstTime = false;
            
            StartCoroutine("waitForstable");
        }
        isGroundTouch=true;
    }


    /*IEnumerator findAngle(){

        
        while(!GetComponent<Rigidbody2D>().IsSleeping())yield return null;
        angle = Vector3.Angle(Vector3.up, transform.up);

        if(angle>175 || angle<5 ){

            StartCoroutine(cam.goodShake(0.15f,0.1f));
            StartCoroutine(cam2.goodShake(0.15f,0.1f));
            part.Play();
            score++;
            
        }
        else {


            StartCoroutine(cam.badShake(0.15f,0.1f));
            StartCoroutine(cam2.badShake(0.15f,0.1f));
            score=0;
            
        }

        scval.scoreChange(score); 
    }*/

    IEnumerator waitForstable(){

        yield return new WaitForSeconds(0.35f);
        StartCoroutine(cam.badShake(0.15f,0.1f));
        StartCoroutine(cam2.badShake(0.15f,0.1f));
        score = Mathf.Max(0, score-1);;
        scval.scoreChange(score);
        yield return new WaitForSeconds(0.35f);
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        position = Mathf.Max(0, position-1);
        spr.changeIt(position);
        spr1.changeSize(position);
        GetComponent<Rigidbody2D>().position = new Vector3(0f,1.4f,6.0f);
        GetComponent<Rigidbody2D>().rotation = 0f;
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        val = 0f;
        firstTime = true;
    }

}