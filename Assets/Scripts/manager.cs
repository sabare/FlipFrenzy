using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class manager : MonoBehaviour
{

    public void nextlvl(int k){

        SceneManager.LoadScene(k);
    }
}
