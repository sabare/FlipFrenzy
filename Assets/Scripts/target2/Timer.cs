using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject parent;
    public Image TimeBar;
    public float maxTime = 3.3f;
    float timeLeft;
    public kfmvmt2 km;
    int curr=0  ;
    void Start()
    {
        timeLeft = maxTime;
    }

    // Update is called once per frame
    void Update()
    {   

        if(km.streakHit>15)maxTime = 2.5f;
        else if(km.streakHit<3)maxTime = 5f;
        else maxTime = -0.2f*(km.streakHit-3) + 5f;
        /*if(km.streakHit<3){maxTime=4.3f;}
        else if(km.streakHit>=3 && km.streakHit<5){maxTime=4f;}
        else if(km.streakHit>=5 && km.streakHit<7){maxTime = 3.7f;}
        else if(km.streakHit>=7 && km.streakHit<9){maxTime = 3.4f;}
        else if(km.streakHit>=9 && km.streakHit<11){maxTime = 3.1f;}
        else if(km.streakHit>=11 && km.streakHit<13){maxTime = 2.7f;}
        else if(km.streakHit>=13 && km.streakHit<15){maxTime = 2.4f;}
        else {maxTime = 2.1f;}*/



        if(!km.issRunning){
        if(km.streak<=1){parent.SetActive(false);curr=km.streakHit;}
        else{

            parent.SetActive(true);
            if(timeLeft>0){
                timeLeft-=Time.deltaTime;
                TimeBar.fillAmount = timeLeft/maxTime;
                if(km.streakHit>curr){

                    curr = km.streakHit;
                    timeLeft=maxTime;
                }
            }
            else{

                km.streak = 1;
                km.streakHit = 0;
                //km.hitCount= 0;
                timeLeft=maxTime;
            }
        }
    }
    }
}
