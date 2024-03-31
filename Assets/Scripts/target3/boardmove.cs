using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boardmove : MonoBehaviour
{
    float velocity;
    
    Vector2 minPos = new Vector2(7.4f, 4.64f);
    Vector2 maxPos = new Vector2(7.4f, -1.3f);
    float interpolationRatio = 0f;
    Vector2 pos;
    bool hasHit = false;
    public knifemove km;
    public Transform tf;

    void Start()
    {
        velocity = Random.Range(0.1f, 0.6f);
    }

    // Update is called once per frame
    void Update()
    {   
        if(!hasHit && km.score>3){
            interpolationRatio = Mathf.PingPong(velocity* Time.time,1);
            
            GetComponent<Transform>().position =  Vector2.Lerp(minPos, maxPos, interpolationRatio);
        }
        
    }

    void OnCollisionEnter2D(Collision2D collision) {

       
        if(collision.collider.ToString() == "coll (UnityEngine.BoxCollider2D)") {

            ContactPoint2D contact = collision.contacts[0];
            pos = contact.point;
            tf.position = pos;
            // if(val==1){
                // angle = Vector3.Angle(Vector3.up, transform.up);
                // if(angle>40 && angle<145){
            if(km.canDo){
                km.StartCoroutine("findAngle");
                hasHit = true;
            }

            StartCoroutine("change");
   
                    
                // }

            // }

            
        }

        
        /*if(collision.gameObject.tag == "floor" && firstTime){

            canSwipe = false;
            firstTime = false;
            
            StartCoroutine("waitForstable");
        }
        isGroundTouch=true;
        */        
    }

    IEnumerator change(){

        
        yield return new WaitForSeconds(1f);
        if(hasHit && km.score<=3){

            GetComponent<Transform>().position = new Vector2(7.3f, Random.Range(-1.3f, 4.2f));
            GetComponent<Transform>().eulerAngles  = new Vector3(0, 0, Random.Range(-100f, -170f));
        }
        velocity = Random.Range(0.1f, 0.6f);
        hasHit = false;
    }
}
