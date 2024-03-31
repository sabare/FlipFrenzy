using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class life : MonoBehaviour
{
    
    public GameObject k1;
    public GameObject k2;
    public GameObject k3;
    public kfmvmt2 km;
    
    void Update(){
    
        if(km.life_c==2)k3.SetActive(false);
        else if(km.life_c==1)k2.SetActive(false);
        else if(km.life_c==0) k1.SetActive(false);
    }

}
