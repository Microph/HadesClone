using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody2D rbody;
    private Vector2 direction;
    private float moveSpeed;
    private int attackPoint;

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
        Vector2 movement = dir * GetDirectionMovementSpeed(dir, moveSpeed);
        Vector2 newPos = currentPos + movement * Time.fixedDeltaTime;
        rbody.MovePosition(newPos);
    }

    private float GetDirectionMovementSpeed(Vector2 dir, float speed)
    {
        Vector2 normDir = dir.normalized;
        float angle = Vector2.Angle(Vector2.up, normDir) * Mathf.Deg2Rad;
        float finalSpeed = speed * (0.5f + (0.5f * Mathf.Sin(angle)) ); //TODO: replace 0.5 with current Y iso scales setting
        return finalSpeed;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(
            col.GetComponent<IDamageable>() is IDamageable iDamageable
            && col.gameObject.tag != "Player"
        )
        {
            iDamageable.OnBeingDamaged(attackPoint);
            Destroy(gameObject);
        }
        else if(col.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }
    }
}
