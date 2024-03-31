
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class knifemove : MonoBehaviour
{
    public Vector2 startPos;
    public Vector2 direction;
    public Text m_Text;
    string message;

    public float pmeter = 0.033f;
    private float val =0;
    public float revSpeed = -360f;
    public float angle  = 0f;
    public int score = 0;

    public bool isGroundTouch = true;
    private bool canSwipe = false;
    private bool firstTime = true;
    public bool canDo = true;

    public camShake cam;
    public camShake cam2;
    public manager manager;
    public ParticleSystem part;
    
    public Scoreval scval;
    public Vector2 pos;
    public panelShake ps;

    Rigidbody2D rb;
    LineRenderer lr;
    void Start(){

        rb = GetComponent<Rigidbody2D>();
        lr = GetComponent<LineRenderer>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        

    }
    void Update()
    {
         m_Text.text = message + " " + rb.IsSleeping()+ Vector3.Angle(Vector3.up, transform.up);
        //tf.position = new Vector2(GetComponent<Transform>().position.x , GetComponent<Transform>().position.y-1f);
        if(!isGroundTouch){

            
            rb.angularVelocity = revSpeed;
           
        }
        Debug.Log(revSpeed);
        
        
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
                    Debug.Log(direction.y);

                    //rb.rotation = -45 + direction.y/10;

                    if(Mathf.Sqrt(direction.x*direction.x+direction.y*direction.y)>50 && direction.y>0){

                        Vector2 _velocity = new Vector2(Mathf.Min(direction.x*pmeter, 18), Mathf.Min(direction.y*pmeter, 17));

                        Vector2[] trajectory = Plot(rb, GetComponent<Transform>().position, _velocity, 150);

                        lr.positionCount = trajectory.Length;

                        Vector3[] positions = new Vector3[trajectory.Length];
                        for(int i=0;i<positions.Length; i++){

                            positions[i] = trajectory[i];
                        }                      

                        lr.SetPositions(positions);
                    }

                    break;

                case TouchPhase.Ended:
                    
                    message = "Ending ";
                    rb.constraints = RigidbodyConstraints2D.None;
                    
                    if(Mathf.Sqrt(direction.x*direction.x+direction.y*direction.y)>50 && direction.y>0){
                        
                        revSpeed = 80f/ (Mathf.Min(direction.y*pmeter, 17)/Physics2D.gravity.y);
                        isGroundTouch=false;
                        canSwipe =false;
                        val = 1;
                        lr.positionCount = 0;
                        rb.constraints = RigidbodyConstraints2D.None;

                        fun();
                    }
                    break;
            }

        }


        
    }

    public void fun(){
        
        rb.velocity = new Vector2(Mathf.Min(direction.x*pmeter, 18), Mathf.Min(direction.y*pmeter, 17));
        
    
    }


    void OnCollisionEnter2D(Collision2D collision) {

        
        if(collision.gameObject.tag == "stage") {

            ContactPoint2D contact = collision.contacts[0];
            pos = contact.point;
            canSwipe = true;
            if(val==1){
                // angle = Vector3.Angle(Vector3.up, transform.up);
                // if(angle>40 && angle<145){

                //     StartCoroutine("findAngle");
                    
                // }
                val=0f;
                StartCoroutine("waitForstable");

            }

            
        }

        if((collision.collider.ToString() == "colli (UnityEngine.BoxCollider2D)" ||
           collision.collider.ToString() == "colli (1) (UnityEngine.BoxCollider2D)" ||
           collision.collider.ToString() == "colli (2) (UnityEngine.BoxCollider2D)") && firstTime){

            canSwipe = false;
            firstTime = false;
            canDo = false;
            StartCoroutine("waitForstable");
        }

        if(collision.gameObject.tag == "floor" && firstTime){

            canSwipe = false;
            firstTime = false;
            
            StartCoroutine("waitForstable");
        }
        isGroundTouch=true;

        
    }

    
    IEnumerator findAngle(){

        StartCoroutine(cam.goodShake(0.15f,0.1f));
        StartCoroutine(cam2.goodShake(0.15f,0.1f));
        val=0f;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        //tf.position = pos;
        part.Play();
        score++;
        scval.scoreChange(score);
        yield return new WaitForSeconds(1f);
        rb.position = new Vector3(-5.5f,1.2f,0);
        //bm.moveBoard();
        rb.rotation = Random.Range(-75f, -5f);
        rb.constraints = RigidbodyConstraints2D.None;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        
    }

    IEnumerator waitForstable(){

        yield return new WaitForSeconds(0.35f);
        StartCoroutine(cam.badShake(0.15f,0.1f));
        StartCoroutine(cam2.badShake(0.15f,0.1f));
        StartCoroutine(ps.badShake(0.15f,0.1f));
        
        score = 0;
        scval.scoreChange(score);
        yield return new WaitForSeconds(0.35f);
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        rb.position = new Vector3(-5.5f,1.2f,0);
        //bm.moveBoard();
        rb.rotation = Random.Range(-75f, -5f);
        rb.constraints = RigidbodyConstraints2D.None;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        val = 0f;
        firstTime = true;
        canDo = true;
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