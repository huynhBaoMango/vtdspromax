using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.Experimental.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class diePanel : MonoBehaviour
{
    public Text endgameText;
    public static bool isDiePanelActive = false;
    void Start()
    {
        FindAnyObjectByType<AudioManager>().Stop("theme1");
        FindAnyObjectByType<AudioManager>().Stop("theme2");
        FindAnyObjectByType<AudioManager>().Play("end");
        int wave = FindAnyObjectByType<Spawner>().wave-1;
        endgameText.text = "You survived " + wave + " waves.";
        isDiePanelActive = true;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            isDiePanelActive = false;
            SceneManager.LoadScene("Start");
        }
    }

}
