using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public Rigidbody rb;
    public float liveTime = 50f;
    private bool isDamage;
    private Vector3 oldPosition;
    public float damage;
    private Team team;

    private void Awake()
    {
        Destroy(gameObject, liveTime);
        rb = GetComponent<Rigidbody>();
    }


    private void FixedUpdate()
    {
        transform.LookAt(transform.position + transform.position  - oldPosition);
        oldPosition = transform.position;
    }
    public void Setup(float _damage, Team _team)
    {
        damage = _damage;
        team = _team;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (isDamage) return;
        isDamage = true;
        Health health = other.collider.GetComponent<Health>();
        if(health != null && team != health.GetTeam())  Hit(health);
        rb.velocity = Vector3.zero;
        Destroy(gameObject, 1f);
    }

    private void Hit(Health health)
    {
        health.TakeDamage(damage);
    }

}
