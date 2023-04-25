using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyRayCastOrigin;
    public Rigidbody2D rigidbody2D;
    private float speed = 100f;

    private void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        DrawEnemyRayCast();
        EnemyMovement();
    }

    private void DrawEnemyRayCast()
    {
        // Create a raycast and check what it hits
        float rayCastLength = 1f;
        Debug.DrawRay(enemyRayCastOrigin.transform.position, 
            transform.TransformDirection(Vector2.right) * rayCastLength, Color.magenta);

        RaycastHit2D hit = Physics2D.Raycast(enemyRayCastOrigin.transform.position,
            transform.TransformDirection(Vector2.right), rayCastLength);
        

        if (hit)
        {
            Debug.Log(gameObject.name + "Enemy hit " + hit.collider.name);
        }
    }

    private void EnemyMovement()
    {
        rigidbody2D.AddRelativeForce(Vector2.right * speed * Time.deltaTime);
    }


}
