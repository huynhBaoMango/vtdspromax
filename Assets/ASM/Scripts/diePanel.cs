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
    void Start()
    {
        FindAnyObjectByType<AudioManager>().Stop("theme1");
        FindAnyObjectByType<AudioManager>().Stop("theme2");
        FindAnyObjectByType<AudioManager>().Play("end");
        int wave = FindAnyObjectByType<Spawner>().wave-1;
        endgameText.text = "You survived " + wave + " waves.";
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene("Start");
        }
    }

}
