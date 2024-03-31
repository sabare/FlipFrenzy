using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextPos : MonoBehaviour
{
    public Transform tf;
    public kfmvmt2 km;
    int curr = 0;
    Vector2 localPos;
    /*void Start(){
        tf = GetComponent<Transform>();
    }*/
    // Update is called once per frame
    void Update()
    {
        if(km.streakHit!=curr){

            curr=km.streakHit;
            localPos = new Vector2(Random.Range(-590f, 650f), Random.Range(-390f, 130f));
            tf.position = localPos;
        }
    }
}
