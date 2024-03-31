using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class chsmvmt : MonoBehaviour
{
    public Vector2 startPos;
    public Vector2 direction;
    public Text m_Text;
    string message;

    public float pmeter = 0.03f;
    private float val =0;
    public bool isGroundTouch = true;
    public float x =-1f;
    public float angle  = 0f;
    public float oldangle  = 0f;

    private bool canSwipe = false;
    public camShake cam;
    public manager manager;

    private bool firstTime = true;
    private int lvl = 0;    

    void Update()
    {
        
        m_Text.text = "Touch : " + message + "in direction" + direction + angle +" * "+ Mathf.Sqrt(direction.x*direction.x+direction.y*direction.y);
      


        if(!isGroundTouch){

            if(direction.x>0)GetComponent<Transform>().Rotate(0,0,x);
            else GetComponent<Transform>().Rotate(0,0,-x);
        
        }
        Debug.Log(GetComponent<Rigidbody2D>().centerOfMass);

        
        
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
                    
                    
                    if(Mathf.Sqrt(direction.x*direction.x+direction.y*direction.y)>90){

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

        isGroundTouch=true;
        if(collision.gameObject.tag == "stage") {

            canSwipe = true;
            if(val==1){

                StartCoroutine("findAngle");
            }

            
        }


        if(collision.gameObject.tag == "floor" && firstTime){

            isGroundTouch=true;
            canSwipe = false;
            firstTime = false;
            StopCoroutine("findAngle");
            StartCoroutine("waitForstable");
        }
    }


    IEnumerator findAngle(){

        oldangle = Vector3.Angle(Vector3.up, transform.up);
        yield return new WaitForSeconds(0.15f);
        angle = Vector3.Angle(Vector3.up, transform.up);

        if(oldangle!=angle)StartCoroutine("findAngle");

        if(angle>175 || angle<5 ){

            Debug.Log("Safer");
            yield return new WaitForSeconds(0.25f);
            lvl+=1;
            lvlchange();
        }

        else {

            lvl = Mathf.Min(1, lvl-1);
            lvlchange();
        }
        
    }

    IEnumerator waitForstable(){

        StartCoroutine(cam.badShake(0.15f,0.1f));
        yield return new WaitForSeconds(0.5f);
        GetComponent<Rigidbody2D>().position = new Vector3(0f,1.4f,6.0f);
        GetComponent<Rigidbody2D>().rotation = 0f;
        val = 0f;
        firstTime = true;
        yield return new WaitForSeconds(0.5f);
    }


    void lvlchange(){

        switch(lvl){

            case 1:
                //GetComponent<Rigidbody2D>().centerOfMass = new Vector2(0,0);
                break;
        }
    }

     /*private void OnDrawGizmos()

    {

        Gizmos.color = Color.magenta;

        Gizmos.DrawSphere(transform.position + transform.rotation*new Vector2(0f,0.2f),0.5f);

    }*/


}