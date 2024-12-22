using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour
{
    [Header("Bullet Stats")]
    public float bulletSpeed;
    public int damage;
    private Rigidbody2D rb;
    private float gridMoveTimer = 0f;
    private float gridMoveTimerMax = 0.2f;

    IObjectPool<Bullet> objectPool;
    public IObjectPool<Bullet> ObjectPool {get => objectPool; set => objectPool = value;}

    IEnumerator FireRoutine() {
        yield return new WaitForSeconds(5.0f);
        // Kembalikan bullet kedalam pool
        // Refer to ReturnToPool(Bullet bullet)
        objectPool.Release(this);
    }

    public void Fire() {
        // Aplikasi speed kedalam bullet
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(FireRoutine());
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        other.gameObject.GetComponent<Hitbox>().Damage(this);
        objectPool.Release(this);
    }

    
    void Update()
    {
        gridMoveTimer += Time.deltaTime;
        if (gridMoveTimer >= gridMoveTimerMax) {
            gridMoveTimer = 0;
            rb.position += Vector2.down;
        }
    }
}
