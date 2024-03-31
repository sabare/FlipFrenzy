using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spriteChange : MonoBehaviour
{
    public Sprite[] spriteArray;
   
    
    public void changeIt(int k){

        GetComponent<SpriteRenderer>().sprite = spriteArray[k];
    }

    public void changeSize(int k){

        float val = k;
        GetComponent<Transform>().localScale = new Vector2(Mathf.Lerp(0.2f, 0.7186285f, 1f - val/14), 0.6115f);
        //Debug.Log(1f - val/3);
    }
}
