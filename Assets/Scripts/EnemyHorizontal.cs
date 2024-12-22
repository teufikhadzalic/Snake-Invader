using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHorizontal : MonoBehaviour
{
    private Rigidbody2D rb;
    private float gridMoveTimer = 0f;
    private float gridMoveTimerMax = 0.2f;
    bool moveLeft;
    
    void Start() {
        // Inisialisasi awal
        gameObject.SetActive(true);
        rb = GetComponent<Rigidbody2D>();
        SpawnEnemy();
    }

    public void SpawnEnemy() {
        // Spawn di kiri & diatas Player
        rb.position = new Vector2(0, 30);
        moveLeft = false;
    }

    void Update()
    {
        gridMoveTimer += Time.deltaTime;
        if (gridMoveTimer >= gridMoveTimerMax) {
            gridMoveTimer = 0;
            if(rb.position.x < 0f) {
                moveLeft = false;
            }
            if(rb.position.x > 20f) {
                moveLeft = true;
            }

            switch(moveLeft) {
                case true:
                    rb.position += Vector2.left;
                    break;
                case false:
                    rb.position += Vector2.right;
                    break;
            }
        }
    }
}
