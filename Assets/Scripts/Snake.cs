using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using CodeMonkey;
public class Snake : MonoBehaviour {

    private enum Direction{
        Left,
        Right,
        Up,
        Down


    }
    private enum State{
        Alive,
        Dead
    }
    private State state;
    private Direction gridMoveDirection; 
    private Vector2Int gridPosition;
    private float gridMoveTimer;
    private float gridMoveTimerMax;
    private LevelGrid levelGrid;
    private int snakeBodySize;
    private List<SnakeMovePosition> snakeMovePositionList;
    private List<SnakeBodyPart> snakeBodyPartList;

    public void Setup(LevelGrid levelGrid){
        this.levelGrid =levelGrid;

    }



    private void Awake() {
    gridPosition = new Vector2Int(10, 10);
    gridMoveTimerMax = 0.2f; // Dikurangi dari 1f menjadi 0.8f untuk membuat ular lebih cepat
    gridMoveTimer = gridMoveTimerMax;
    gridMoveDirection = Direction.Right;
    snakeBodySize = 0;
    snakeMovePositionList = new List<SnakeMovePosition>();
    snakeBodyPartList = new List<SnakeBodyPart>();
    state = State.Alive;
}

    private void Update() {
        switch(state){
        case State.Alive:
         HandleInput();
         HandleGridMovement();
         break;
         case State.Dead:
         break;

     }
    }

    private void HandleInput(){
         if (Input.GetKeyDown(KeyCode.UpArrow)) {
        if(gridMoveDirection!= Direction.Down) { 
        gridMoveDirection = Direction.Up;
        }

    }
    if (Input.GetKeyDown(KeyCode.DownArrow)) {
        if(gridMoveDirection != Direction.Up) { 
        gridMoveDirection = Direction.Down;
        }
    }
    if (Input.GetKeyDown(KeyCode.LeftArrow)) {
        if(gridMoveDirection != Direction.Right) { 
        gridMoveDirection = Direction.Left;
        }
    }
    if (Input.GetKeyDown(KeyCode.RightArrow)) {
        if(gridMoveDirection != Direction.Left) { 
        gridMoveDirection = Direction.Right;
    }
    }
    }

    private void HandleGridMovement(){
    
     gridMoveTimer += Time.deltaTime;
        if (gridMoveTimer >= gridMoveTimerMax) {
            
            gridMoveTimer -= gridMoveTimerMax;

            SnakeMovePosition previousSnakeMovePosition = null;
            if(snakeMovePositionList.Count > 0){
                previousSnakeMovePosition = snakeMovePositionList[0];
            }
            SnakeMovePosition snakeMovePosition = new SnakeMovePosition(previousSnakeMovePosition, gridPosition, gridMoveDirection);
            snakeMovePositionList.Insert(0,snakeMovePosition);
            

            Vector2Int gridMoveDirectionVector;
                 switch (gridMoveDirection) {
                 default:
                  case Direction.Right: gridMoveDirectionVector = new Vector2Int(+1, 0); break;
                  case Direction.Left: gridMoveDirectionVector = new Vector2Int(-1, 0); break;
                  case Direction.Up: gridMoveDirectionVector = new Vector2Int(0, +1); break;
                  case Direction.Down: gridMoveDirectionVector = new Vector2Int(0, -1); break;
            }
            gridPosition += gridMoveDirectionVector;
            gridPosition = levelGrid.ValidateGridPosition(gridPosition);

            

            bool snakeAteFood = levelGrid.SnakeAte(gridPosition);
             if (snakeAteFood){
                //snake gede
                snakeBodySize++;
                CreateSnakeBody();
             }



            
            if (snakeMovePositionList.Count >= snakeBodySize + 1){
                snakeMovePositionList.RemoveAt(snakeMovePositionList.Count-1);
            }
            UpdateSnakeBodyParts();
          
           foreach (SnakeBodyPart snakeBodyPart in snakeBodyPartList){
            Vector2Int snakeBodyPartGridPosition = snakeBodyPart.GetGridPosition();
            if (gridPosition == snakeBodyPartGridPosition){
                //you are dead
                CMDebug.TextPopup("Youre dead", transform.position);
                state = State.Dead;
            }


           }


              transform.position = new Vector3(gridPosition.x, gridPosition.y);
              transform.eulerAngles = new Vector3(0, 0, GetAngleFromVector(gridMoveDirectionVector)- 90);
              
             
        }
      
    }
    
    private void CreateSnakeBody(){
        snakeBodyPartList.Add(new SnakeBodyPart(snakeBodyPartList.Count));


    }

    private void UpdateSnakeBodyParts(){
         for (int i = 0; i < snakeBodyPartList.Count; i++) {
                 snakeBodyPartList[i].SetSnakeMovePosition(snakeMovePositionList[i]);
         }

    }

    
    private float GetAngleFromVector(Vector2Int dir) {
    float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
    if (n < 0) n += 360;
    return n;
}
  public Vector2Int GetGridPosition(){
    return gridPosition;
  } 

// data posisi head and body snake 
    public List<Vector2Int> GetFullSnakeGridPositionList() {
    List<Vector2Int> gridPositionList = new List<Vector2Int>() { gridPosition };
    foreach (SnakeMovePosition snakeMovePosition in snakeMovePositionList) {
        gridPositionList.Add(snakeMovePosition.GetGridPosition());
    }
    return gridPositionList;
}

private class SnakeBodyPart{
    private SnakeMovePosition snakeMovePosition;
    private Transform transform;
    public SnakeBodyPart(int bodyIndex){
         GameObject snakeBodyGameObject = new GameObject("Snake Body", typeof(SpriteRenderer));
         snakeBodyGameObject.GetComponent<SpriteRenderer>().sprite = GameAssets.i.snakeBodySprite;
         snakeBodyGameObject.GetComponent<SpriteRenderer>().sortingOrder = -bodyIndex;
         transform = snakeBodyGameObject.transform;

    }

    public void SetSnakeMovePosition(SnakeMovePosition snakeMovePosition) {
    
    this.snakeMovePosition = snakeMovePosition;

    transform.position = new Vector3(snakeMovePosition.GetGridPosition().x, snakeMovePosition.GetGridPosition().y);

    float angle;
switch (snakeMovePosition.GetDirection()) {
    default:
    case Direction.Up: // Currently going Up
        switch (snakeMovePosition.GetPreviousDirection()) {
            default: 
                angle = 0; break;
            case Direction.Left: // Previously was going Left
                angle = 0 + 45; break;
            case Direction.Right: // Previously was going Right
                angle = 0 - 45; break;
        }
        break;

    case Direction.Down: // Currently going Down
        switch (snakeMovePosition.GetPreviousDirection()) {
            default: 
                angle = 180; break;
            case Direction.Left: // Previously was going Left
                angle = 180 + 45; break;
            case Direction.Right: // Previously was going Right
                angle = 180 - 45; break;
        }
        break;

    case Direction.Left: // Currently going to the Left
        switch (snakeMovePosition.GetPreviousDirection()) {
            default: 
                angle = -90; break;
            case Direction.Down: // Previously was going Down
                angle = -90 + 45; break;
            case Direction.Up: // Previously was going Up
                angle = -90 - 45; break;
        }
        break;
        case Direction.Right: // Currently going to the Right
    switch (snakeMovePosition.GetPreviousDirection()) {
        default:
            angle = 90; break;
        case Direction.Down: // Previously was going Down
            angle = 45; break;
        case Direction.Up: // Previously was going Up
            angle = -45; break;
    }
    break;
}

     transform.eulerAngles = new Vector3(0, 0, angle);
    }
    public Vector2Int GetGridPosition(){
        return snakeMovePosition.GetGridPosition();
    }

}

    private class SnakeMovePosition {
    private SnakeMovePosition previousSnakeMovePosition;
    private Vector2Int gridPosition;
    private Direction direction;

    public SnakeMovePosition(SnakeMovePosition previousSnakeMovePosition, Vector2Int gridPosition, Direction direction) {
        this.previousSnakeMovePosition = previousSnakeMovePosition;
        this.gridPosition = gridPosition;
        this.direction = direction;
    }

    public Vector2Int GetGridPosition() {
        return gridPosition;
    }

    public Direction GetDirection() {
        return direction;
    }

    public Direction GetPreviousDirection() {
        if (previousSnakeMovePosition == null) {
            return Direction.Right;
        } else {
            return previousSnakeMovePosition.direction;
        }
     }
    }

    
}