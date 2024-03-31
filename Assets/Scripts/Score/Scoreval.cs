using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scoreval : MonoBehaviour
{
    public Text text;
    void Start(){

    }
    // Update is called once per frame
    public void scoreChange(int score)
    {
        text.text = score.ToString();
    }

    
}
