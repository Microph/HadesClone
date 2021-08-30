using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody2D rbody;
    private Vector2 direction;
    private float moveSpeed;
    protected int attackPoint;

    public void Setup(Vector2 direction, float moveSpeed, int attackPoint)
    {
        this.direction = direction;
        this.moveSpeed = moveSpeed;
        this.attackPoint = attackPoint;
    }

    private void Awake()
    {
        rbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        //Move out from player parent
        //TODO: refactor this
        gameObject.transform.parent = null;
    }

    void FixedUpdate()
    {
        Move(direction, moveSpeed);
    }

    private void Move(Vector2 dir, float moveSpeed)
    {
        Vector2 currentPos = rbody.position;
        Vector2 movement = ScaleVectorToIsoView(dir) * moveSpeed;
        Vector2 newPos = currentPos + movement * Time.fixedDeltaTime;
        rbody.MovePosition(newPos);
    }

    private Vector2 ScaleVectorToIsoView(Vector2 dir)
    {
        return Vector2.Scale(dir, new Vector2(1, 0.5f)); //TODO: Get value from iso config
    }

    protected virtual void OnTriggerEnter2D(Collider2D col)
    {
        if(
            col.GetComponent<IDamageable>() is IDamageable iDamageable
            && col.gameObject.tag != "Player"
        )
        {
            iDamageable.TakeDamage(attackPoint);
            Destroy(gameObject);
        }
        else if(col.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }
    }
}
