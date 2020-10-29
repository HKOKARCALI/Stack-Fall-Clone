using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static ScoreManager intance;

    public int score;
    public Text scoreTXT;

    private void Awake()
    {

        makeSingleton();

    }

    private void makeSingleton()
    {
        if (intance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            intance = this;
            DontDestroyOnLoad(this);
        }
    }

    void Start()
    {
        addScore(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addScore(int value)
    {
        score += value;
        if (score > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", score);
        }
        Debug.Log("score>> " +  score);
    }

    public void ResetScore()
    {
        score = 0;
    }
}
