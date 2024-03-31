using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boardmvmt : MonoBehaviour
{
    float velocity;
    
    Vector2 minPos = new Vector2(-11, 4.64f);
    Vector2 maxPos = new Vector2(11, 4.64f);
    float interpolationRatio = 0f;
    Vector2 pos;
    bool hasHit = false;
    public knifemvmt km;
    public Transform tf;

    void Start()
    {
        velocity = Random.Range(0.1f, 0.6f);
    }

    // Update is called once per frame
    void Update()
    {   
        if(!hasHit){
            interpolationRatio = Mathf.PingPong(velocity* Time.time,1);
            //Debug.Log(velocity);
            GetComponent<Transform>().position =  Vector2.Lerp(minPos, maxPos, interpolationRatio);
        }
        
    }

    void OnCollisionEnter2D(Collision2D collision) {

        //Debug.Log(collision.collider);
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
            //Debug.Log("Hit");
                    
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
        velocity = Random.Range(0.1f, 0.6f);
        hasHit = false;
    }
}
