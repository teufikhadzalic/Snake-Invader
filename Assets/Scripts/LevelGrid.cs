using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey;
using CodeMonkey.Utils;
public class LevelGrid
{
    private Vector2Int foodGridPosition; 
    private GameObject foodGameObject;
    private int width;
    private int height;
    private Snake snake;
    private Health snakeHealth;

    public LevelGrid(int width, int height){
    this.width = width;
    this.height = height;
  

    
    }
    public void Setup(Snake snake){
    this.snake =snake; 
    snakeHealth = snake.GetComponent<Health>();
      SpawnFood();
    }
 
     
    private void SpawnFood(){
        do{
    foodGridPosition = new Vector2Int(Random.Range(0, width), Random.Range(0,height));
        }
        while (snake.GetFullSnakeGridPositionList().IndexOf(foodGridPosition) != -1);

    foodGameObject = new GameObject ( "Food", typeof(SpriteRenderer));
    foodGameObject.GetComponent<SpriteRenderer>().sprite = GameAssets.i.foodSprite;
    foodGameObject.transform.position = new Vector3(foodGridPosition.x, foodGridPosition.y);

    }

    public bool SnakeAte(Vector2Int snakeGridPosition){
        snakeHealth = snake.GetComponent<Health>();
        if(snakeGridPosition== foodGridPosition){
            Object.Destroy(foodGameObject);
            SpawnFood();
            GameHandler.AddScore();
            snakeHealth.Heal();
            return true;
        } else{
            return false;
        }
        
    }
   public Vector2Int ValidateGridPosition(Vector2Int gridPosition) {
    if (gridPosition.x < 0) {
        gridPosition.x = width - 1; // Wrap to the right edge
    }
    if (gridPosition.x > width -1){
        gridPosition.x = 0;
    }
    if (gridPosition.y < 0) {
        gridPosition.y = height - 1; // Wrap to the bottom edge
    }
    if (gridPosition.y > height -1){
        gridPosition.y = 0;
    }
    return gridPosition;
}

}
