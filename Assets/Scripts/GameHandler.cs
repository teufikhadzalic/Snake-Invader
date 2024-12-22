
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey;
using CodeMonkey.Utils;

public class GameHandler : MonoBehaviour {
    private static GameHandler instance;
    [SerializeField] private Snake snake;
    private LevelGrid levelGrid;
    private static int score;

    private void Awake(){
      instance = this;
    }


    private void Start() {
      levelGrid = new LevelGrid(20,20);
      snake.Setup(levelGrid);
      levelGrid.Setup(snake);

        Debug.Log("GameHandler.Start");

       /* GameObject snakeHeadGameObject = new GameObject();
        SpriteRenderer snakeSpriteRenderer = snakeHeadGameObject.AddComponent<SpriteRenderer>();
        snakeSpriteRenderer.sprite = GameAssets.i.snakeHeadSprite; 
        */
    }

    public static int GetScore(){
      return score;
    }

    public static void AddScore(){
      score += 100;
    }

  
    
}
