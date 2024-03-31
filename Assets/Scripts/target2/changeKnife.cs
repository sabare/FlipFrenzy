using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeKnife : MonoBehaviour
{
    public Sprite knife1;
    public Sprite knife2;
    public kfmvmt2 km;
    public SpriteRenderer sr;
    public Transform tf;
    void Update()
    {
        if(km.streak==4){

            sr.sprite = knife2;
            tf.localScale =  new Vector2(0.48f,0.48f);
        }
        else {

            sr.sprite = knife1;
            tf.localScale =  new Vector2(0.0173f,0.0173f);
        }
    }
}
