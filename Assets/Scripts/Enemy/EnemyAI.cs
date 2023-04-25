using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyRayCastOrigin;
    public Rigidbody2D r;
    private float speed = 90f;
    private RaycastHit2D rcHit;

    private void Start()
    {
        r = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        DrawEnemyRayCast();
        EnemyMovement();
        if (rcHit)
        {
            Debug.Log(gameObject.name + "Enemy hit " + rcHit.collider.name);
            // Rotate at a random direction when enemy hits something.
            float randomTurnDir = Random.Range(-180, 180);
            transform.Rotate(0, 0, randomTurnDir);
        }
    }

    private void DrawEnemyRayCast()
    {
        // Create a raycast and check what it hits
        float rayCastLength = 1f;
        Debug.DrawRay(enemyRayCastOrigin.transform.position, 
            transform.TransformDirection(Vector2.right) * rayCastLength, Color.magenta);

        rcHit = Physics2D.Raycast(enemyRayCastOrigin.transform.position,
            transform.TransformDirection(Vector2.right), rayCastLength);
    }

    private void EnemyMovement()
    {
        r.AddRelativeForce(Vector2.right * speed * Time.deltaTime);
    }


}
