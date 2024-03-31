using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nextControl : MonoBehaviour
{
    // Start is called before the first frame update

    public showTut st;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount>0){
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began){
                Vector3 Posi = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
                if(Posi.x>=2)st.count = Mathf.Min(7,st.count+1);
                else if (Posi.x<-2) st.count = Mathf.Max(0, st.count-1);
            }
        }
    }
}
