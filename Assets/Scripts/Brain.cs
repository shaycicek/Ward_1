using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Brain : MonoBehaviour
{
    public Rigidbody rb;
    public Character ch;
    public Weapon wp;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        ch = GetComponent<Character>();
        wp = ch.myWeapon;
    }
    public virtual void Follow()
    {
        Vector3 direction = ch.FindClosestEnemy(transform.position, ch.sightRange).position - transform.position;
        transform.LookAt(ch.FindClosestEnemy(transform.position, ch.sightRange).position);
        rb.MovePosition(transform.position + direction.normalized * ch.movementSpeed * Time.fixedDeltaTime);
    }

    public virtual void Avoid()
    {

    }

    public virtual void Attack()
    {

    }

    public virtual void Idle()
    {

    }
    
}
