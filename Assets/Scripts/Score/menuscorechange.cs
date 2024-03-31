using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class menuscorechange : MonoBehaviour
{
    // Start is called before the first frame update
    public Text text;
    public Text text2;
    void Start()
    {
        Debug.Log(bdmvmt2.highs);
        int val = kfmvmt2.score;
        text.text = val.ToString();
        text2.text = bdmvmt2.highs.ToString();  
       
    }

    
}
