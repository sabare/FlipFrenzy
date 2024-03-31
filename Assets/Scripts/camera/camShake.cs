using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camShake : MonoBehaviour
{

    public IEnumerator badShake(float dur, float mag){

        Vector2 orgPos = transform.localPosition;
        
        float elapsed = 0f;

        while(elapsed<dur){

            float time =Mathf.PingPong(Time.time*2f, 0.075f);
       

            transform.localPosition = new Vector2(orgPos.x+time, orgPos.y);

            elapsed += Time.deltaTime;

            yield return null;
        }
        transform.localPosition = new Vector2(0,0);
    }

    public IEnumerator goodShake(float dur, float mag){

        Vector2 orgPos = transform.localPosition;
        
        float elapsed = 0f;

        while(elapsed<dur){

            float time =Mathf.PingPong(Time.time*2f, 0.075f);
    

            transform.localPosition = new Vector2(orgPos.x, orgPos.y+time);

            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = new Vector2(0,0f);
    }
}
