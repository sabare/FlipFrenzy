using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class panelShake : MonoBehaviour
{
    public IEnumerator badShake(float dur, float mag){

        Vector3 orgPos = GetComponent<RectTransform>().position;
        Debug.Log(orgPos);
        float elapsed = 0f;

        while(elapsed<dur){

            float time =Mathf.PingPong(Time.time*2f, 0.075f);
       

            GetComponent<RectTransform>().position = new Vector3(orgPos.x+time, orgPos.y, orgPos.z);

            elapsed += Time.deltaTime;

            yield return null;
        }
        GetComponent<RectTransform>().position = new Vector3 ( 0f, 3.4f, 90.9f);
    }

    void Update(){

        Debug.Log(GetComponent<RectTransform>().position);
    }
}
