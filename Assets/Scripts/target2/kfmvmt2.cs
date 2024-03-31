
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class kfmvmt2 : MonoBehaviour
{
    public Vector2 startPos;
    public Vector2 direction;
    public Text m_Text;
    //string message;

    public float pmeter = 0.033f;
    private float val =0;
    public float revSpeed = -360f;
    public float angle  = 0f;
    public static int score = 0;
    public int hitCount = 0;
    public int streakHit = 0;
    public bool isGroundTouch = true;
    private bool canSwipe = false;
    private bool firstTime = true;
    public bool canDo = true;
    public int life_c = 3;
    private bool isRunning = false;
    public bool issRunning = false;

    public int streak = 1;

    public camShake cam;
    public camShake cam2;
    public manager manager;
    public ParticleSystem part;
    public AudioSource slice;
    
    public Scoreval scval;
    public Vector2 pos;
    public panelShake ps;

    Rigidbody2D rb;
    LineRenderer lr;
    void Start(){
        life_c=3;
        score = 0;
        scval.scoreChange(score);
        rb = GetComponent<Rigidbody2D>();
        lr = GetComponent<LineRenderer>();
    }
    void Update()
    {
        //Debug.Log(life_c);
        if(life_c==0){
            //rb.constraints = RigidbodyConstraints2D.FreezeAll;  
           //manager.nextlvl(4);
            StartCoroutine("GameOver");
            
        }

        if(streakHit<3){streak = 1;m_Text.text = "";}
        else if(streakHit>=3 && streakHit<7){streak = 2;m_Text.text = "X2";}
        else if(streakHit>=7 && streakHit<10){streak =3;m_Text.text = "X3";}
        else {streak =4;m_Text.text = "X4";}
        
        if(!isGroundTouch){

            if(direction.x>0)rb.angularVelocity = revSpeed;
            else rb.angularVelocity = -revSpeed;
        }
        
        
        if (Input.touchCount > 0 && canSwipe)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                
                case TouchPhase.Began:
                    
                    startPos = touch.position;
                    direction=new Vector2(0,0);
                    //message = "Begun ";
                    break;

                 
                case TouchPhase.Moved:
                    
                    direction = touch.position - startPos;
                    //message = "Moving ";

                    if(Mathf.Sqrt(direction.x*direction.x+direction.y*direction.y)>50 && direction.y>0){

                        Vector2 _velocity = new Vector2(Mathf.Min(direction.x*pmeter, 20), Mathf.Min(direction.y*pmeter, 16));

                        Vector2[] trajectory = Plot(rb, GetComponent<Transform>().position, _velocity, 100);

                        lr.positionCount = trajectory.Length;

                        Vector3[] positions = new Vector3[trajectory.Length];
                        for(int i=0;i<positions.Length; i++){

                            positions[i] = trajectory[i];
                        }                      

                        lr.SetPositions(positions);
                    }

                    break;

                case TouchPhase.Ended:
                    
                    //message = "Ending ";
                    rb.constraints = RigidbodyConstraints2D.None;
                    lr.positionCount = 0;
                    if(Mathf.Sqrt(direction.x*direction.x+direction.y*direction.y)>50 && direction.y>0){

                        isGroundTouch=false;
                        canSwipe =false;
                        val = 1;
                        slice.Play();
                        
                        fun();
                    }
                    break;
            }

        }


        
    }

    public void fun(){
        
        rb.velocity = new Vector2(Mathf.Min(direction.x*pmeter, 20), Mathf.Min(direction.y*pmeter, 16));
     
    }




    void OnCollisionEnter2D(Collision2D collision) {

        
        if(collision.gameObject.tag == "stage" && !isRunning) {

            ContactPoint2D contact = collision.contacts[0];
            pos = contact.point;
            canSwipe = true;
            if(val==1){
                //life_c++;
                val=0f;
                // if(!isRunning)
                StartCoroutine("waitForstable");
            }

            
        }

        if((collision.collider.ToString() == "colli (UnityEngine.BoxCollider2D)" ||
           collision.collider.ToString() == "colli (1) (UnityEngine.BoxCollider2D)" ||
           collision.collider.ToString() == "colli (2) (UnityEngine.BoxCollider2D)") && firstTime && !isRunning){

            canSwipe = false;
            firstTime = false;
            canDo = false;
            // if(!isRunning)
            StartCoroutine("waitForstable");
        }

        if(collision.gameObject.tag == "floor" && firstTime && !isRunning){

            canSwipe = false;
            firstTime = false;
            
            StartCoroutine("waitForstable");
            
        }
        isGroundTouch=true;

        
    }

    
    IEnumerator findAngle(){
        issRunning=true;
        StartCoroutine(cam.goodShake(0.15f,0.1f));
        StartCoroutine(cam2.goodShake(0.15f,0.1f));
        val=0f;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        part.Play();
        hitCount++;
        streakHit++;
        scval.scoreChange(score);
        yield return new WaitForSeconds(0.5f);
        rb.position = new Vector3(-9f,3.25f,0);
        rb.rotation = 180f;
        rb.constraints = RigidbodyConstraints2D.None;
        issRunning = false;
        
    }

    IEnumerator waitForstable(){
        isRunning = true;
        yield return new WaitForSeconds(0.35f);
        StartCoroutine(cam.badShake(0.15f,0.1f));
        StartCoroutine(cam2.badShake(0.15f,0.1f));
        StartCoroutine(ps.badShake(0.15f,0.1f));
        
        //score = 0;
        hitCount = 0;
        streakHit = 0;
        scval.scoreChange(score);
        life_c--;
        yield return new WaitForSeconds(0.35f);
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        rb.position = new Vector3(-9f,3.25f,0);
        rb.rotation = 180f;
        rb.constraints = RigidbodyConstraints2D.None;
        val = 0f;
        firstTime = true;
        canDo = true;
        isRunning = false;
    }

    IEnumerator GameOver(){

        yield return new WaitForSeconds(1.0f);
        manager.nextlvl(3);
    }

    public static Vector2[] Plot(Rigidbody2D rigidbody, Vector2 pos, Vector2 velocity, int steps){

        Vector2[] results = new Vector2[steps];
    
        float timestep = Time.fixedDeltaTime / Physics2D.velocityIterations;
        Vector2 gravityAccel = Physics2D.gravity * rigidbody.gravityScale * timestep * timestep;
        float drag = 1f - timestep * rigidbody.drag;
        Vector2 moveStep = velocity * timestep;
    
        for (int i = 0; i < steps; ++i){

            moveStep += gravityAccel;
            moveStep *= drag;
            pos += moveStep;
            results[i] = pos;
        }
    
        return results;
    }



}