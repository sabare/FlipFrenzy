using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bdmvmt2 : MonoBehaviour
{
    float velocity;
    float interpolationRatio = 0f;
    bool hasHit = false;

    Vector2 minPos = new Vector2(9f, 6.45f);
    Vector2 maxPos = new Vector2(9f, 1.2f);
    Vector2 pos;
    Vector2 ch_pos;
    Vector2 inPos;

    public kfmvmt2 km;
    public Transform tf;
    float timeDur = 0f;
    public GameObject FloatingTextPrefab;
    public static int highs = 0;
    string scorec;
     
    void Start()
    {
        //PlayerPrefs.SetInt("HighScore", 0);
        highs = PlayerPrefs.GetInt("HighScore");
        velocity = Random.Range(0.1f, 0.8f);
    }


    void Update()
    {   
        inPos = GetComponent<Transform>().position;
        Debug.Log(km.streakHit);
        if(!hasHit){

         if(km.hitCount>=4 && km.hitCount<7){
            interpolationRatio = Mathf.PingPong(velocity* Time.time,1);
            
            GetComponent<Transform>().position =  Vector2.Lerp(minPos, maxPos, interpolationRatio);
        }
        else if(km.hitCount>=7){

            timeDur+=Time.deltaTime;
            if(timeDur>1.8f && !hasHit){
                
                StartCoroutine("lvlate");
                timeDur = 0f;
            }
        }
        }
        
    }

    void OnCollisionEnter2D(Collision2D collision) {

        
        if(collision.collider.ToString() == "coll (UnityEngine.BoxCollider2D)") {   

            ContactPoint2D contact = collision.contacts[0];
            pos = contact.point;
            //Debug.Log(pos);
            tf.position = pos;
            // if(val==1){
                // angle = Vector3.Angle(Vector3.up, transform.up);
                // if(angle>40 && angle<145){
            if(km.canDo){
                ShowScore();
                km.StartCoroutine("findAngle");
                hasHit = true;
            }
            
            StartCoroutine("change");
            
        }
  
    }

    IEnumerator change(){

        
        yield return new WaitForSeconds(1f);

        if(hasHit && km.hitCount<4){

            StartCoroutine("lvlate");
            //GetComponent<Transform>().position = new Vector2(7.4f, Random.Range(-1.3f, 4.2f));
        }
        velocity = Random.Range(0.1f, 0.8f);
        hasHit = false;
    }

    IEnumerator lvlate(){
        float ctime=2f;
        ch_pos = new Vector2(9f, Random.Range(1.2f, 6.45f));
        while(ctime<=2.5f){
            ctime+=Time.deltaTime;
            
        
            GetComponent<Transform>().position =  Vector2.Lerp(inPos, ch_pos, ((ctime*ctime*ctime)-8)/7.625f);
            yield return null;
        }
        yield return null;


    }

    void ShowScore(){

        if(FloatingTextPrefab){

            GameObject prefab = Instantiate(FloatingTextPrefab, tf.position, Quaternion.identity);
            
            if(Mathf.Abs(GetComponent<Transform>().position.y - tf.position.y)<=0.2f){scorec = "+10";kfmvmt2.score+=km.streak*10;}

            else if(Mathf.Abs(GetComponent<Transform>().position.y - tf.position.y)<=0.4f &&
             Mathf.Abs(GetComponent<Transform>().position.y - tf.position.y)>0.2f){scorec = "+7";kfmvmt2.score+=km.streak*7;}

            else if(Mathf.Abs(GetComponent<Transform>().position.y - tf.position.y)<=0.8f &&
             Mathf.Abs(GetComponent<Transform>().position.y - tf.position.y)>0.4f){scorec = "+5";kfmvmt2.score+=km.streak*5;}

            else if(Mathf.Abs(GetComponent<Transform>().position.y - tf.position.y)<=1.5f &&
             Mathf.Abs(GetComponent<Transform>().position.y - tf.position.y)>0.8f){scorec = "+3";kfmvmt2.score+=km.streak*3;}

            if(kfmvmt2.score>PlayerPrefs.GetInt("HighScore", 0))PlayerPrefs.SetInt("HighScore", kfmvmt2.score);
            highs = PlayerPrefs.GetInt("HighScore");
            //Debug.Log(highs);
            prefab.GetComponentInChildren<TextMesh>().text = scorec.ToString();
        }
    }
}
