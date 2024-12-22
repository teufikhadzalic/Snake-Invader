using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreWindow : MonoBehaviour {
    private Text scoreText;
    private Text healthText;
    public Health snakeHealth;

    private void Awake() {
        scoreText = transform.Find("scoreText").GetComponent<Text>();
        healthText = transform.Find("healthText").GetComponent<Text>();
    }

    private void Update(){
        scoreText.text = "Score: "+GameHandler.GetScore().ToString();
        healthText.text = "Health: "+snakeHealth.health.ToString();
    }
}


