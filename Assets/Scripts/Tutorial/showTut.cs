using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class showTut : MonoBehaviour
{
    // Start is called before the first frame update
    public int count = 0;
    public Text text;
    public GameObject obj;
    public GameObject obj1;


    // Update is called once per frame
    void Update()
    {
        if(count==0){

            text.text = "Welcome to FlipFrenzy, press on right side to continue and to go back press on left side";
            obj1.SetActive(false);
            obj.SetActive(false);
        }

        if(count==1){

            text.text = "This where the score gets updated";
            obj.transform.position = new Vector2(-2.9f, 7.09f);
            obj.SetActive(true);
        }
        else if(count==2){

            text.text = "These are the lives, you get 3 chances. Each gets lost everytime you miss the target";
            obj.transform.position = new Vector2(8.31f, 7.18f);
        }
        else if(count==3){

            text.text = "This is the score multiplier, when you hit the target 3 times in a streak, you get the multiplier";
            obj.transform.position = new Vector2(3.6f, 7.22f);
            obj.transform.eulerAngles = Vector3.forward * 90;
            
        }
        else if(count==4){

            text.text = "When you get the streak, you have to hit the target again within a time limit to continue the streak.";
            obj.transform.position = new Vector2(3f, 7.24f);
            obj.transform.eulerAngles = Vector3.forward * 150;
        }
        else if(count==5){

            text.text = "This is the time limit, the time reduces everytime the streak progress";
            obj.transform.position = new Vector2(3f, 7.24f);
            obj.SetActive(true);
            

        }
        else if(count==6){

            text.text = "Now the main part, Remember all you have to do is Swipe from knife towards the target board";
            obj.SetActive(false);
            obj1.SetActive(false);
            obj.transform.eulerAngles = Vector3.forward * 90;
            obj.transform.position = new Vector2(-1.4f, 0.3f);
        }
        else if(count==7){

            text.text = "There would be a projectile to predict where the knife would hit, press next to enter play Area";
            obj.transform.position = new Vector2(-1.65f, -0.19f);
            obj.SetActive(true);
            obj1.SetActive(true);
        }
    }
}
