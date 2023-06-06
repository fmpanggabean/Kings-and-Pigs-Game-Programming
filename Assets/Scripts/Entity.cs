﻿using System;
using UnityEngine;
using UnityEngine.Events;

public class Entity : MonoBehaviour, IMove, IJump, IDamageable, IAttack
{
    [SerializeField] protected float speed;
    [SerializeField] protected float jumpPower;
    [SerializeField] protected Health health;

    private Vector3 direction;

    //events
    public Action OnAttack;
    public Action<float> OnPositionUpdate;

    // components
    private Rigidbody2D Rigidbody2D;

    private void Awake()
    {
        direction = new Vector3();
        Rigidbody2D = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        PositionUpdate();
    }
    internal Health GetHealth()
    {
        return health;
    }
    public void Jump()
    {
        Vector3 jumpVector = new Vector3(Rigidbody2D.velocity.x, jumpPower, 0);
        Rigidbody2D.velocity = jumpVector;
    }
    public void SetDirection(float direction)
    {
        this.direction.x = direction;

        SetOrientation(direction);
    }
    public void PositionUpdate()
    {
        transform.position += direction * Time.deltaTime * speed;

        OnPositionUpdate?.Invoke(direction.magnitude);
    }
    public void SetOrientation(float direction)
    {
        if (direction > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (direction < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }
    public void TakeDamage(int damage)
    {
        health.Reduce(damage);
    }
    public void Attack()
    {
        OnAttack?.Invoke();
    }
}