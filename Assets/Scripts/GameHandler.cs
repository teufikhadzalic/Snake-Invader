
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey;
using CodeMonkey.Utils;

public class GameHandler : MonoBehaviour {
    [SerializeField] private Snake snake;
    private LevelGrid levelGrid;
    private void Start() {
      levelGrid = new LevelGrid(20,20);
      snake.Setup(levelGrid);
      levelGrid.Setup(snake);






        Debug.Log("GameHandler.Start");




          //   int number = 0;
   //     FunctionPeriodic.Create(() => {
    //        CMDebug.TextPopupMouse("Ding! " + number);
     //       number++;
   //     }, 3f);
   // }

    // Update is called once per frame
   // void Update() {

        GameObject snakeHeadGameObject = new GameObject();
        SpriteRenderer snakeSpriteRenderer = snakeHeadGameObject.AddComponent<SpriteRenderer>();
        snakeSpriteRenderer.sprite = GameAssets.i.snakeHeadSprite;
    }

    // Update is called once per frame
    
}
