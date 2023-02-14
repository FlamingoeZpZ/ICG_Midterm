using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Projectile : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float speed;

    // Update is called once per frame
    void Update()
    {
        transform.position += Time.deltaTime * speed * transform.forward;
    }

    private void OnCollisionEnter(Collision collision)
    {
        print("I hit: " + collision.gameObject.name);
        if (collision.transform.root.TryGetComponent(out Character c))
        {
            c.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
