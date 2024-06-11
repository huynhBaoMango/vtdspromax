using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class getHighScore : MonoBehaviour
{
    public TMP_Text highscoretext;
    void Start()
    {
        int highScore = PlayerPrefs.GetInt("highScore");
        highscoretext.text = highScore+"";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
