using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.Animations;

public class EnemyAI : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyRayCastOrigin;
    public Rigidbody2D r;
   
    private float speed =2f, runAwaySpeed = 10f, distance = .5f, range = 5f;
    private RaycastHit2D rcHit;

    Vector2 newWaypoint;

    private void Start()
    {
        r = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        DrawEnemyRayCast();

        Vector3 scale = transform.localScale;
        if (newWaypoint.x > transform.position.x)
        {
            scale.x = Mathf.Abs(scale.x) * -1;
        }
        else
        {
            scale.x = Mathf.Abs(scale.x);
        }

        transform.localScale = scale;

        transform.position = Vector2.MoveTowards(transform.position, newWaypoint, speed * Time.deltaTime);
        
        
        if (Vector2.Distance(transform.position, newWaypoint) < distance)
        {
            SetNewMoveTowardsVector();

        }

        
         // If enmemy runs into player, quickly run away in a random direction.
        if (rcHit)
        {
            Debug.Log(gameObject.name + "Enemy hit " + rcHit.collider.name);
            float randomTurnDir = Random.Range(-180, 180);
            transform.Rotate(0, 0, randomTurnDir);
            SetNewMoveTowardsVector();
            EnemyScaredRunAway();
        }

       
    }

    private void DrawEnemyRayCast()
    {
        // Create a raycast and check what it hits
        float rayCastLength = 2f;
        Debug.DrawRay(enemyRayCastOrigin.transform.position, 
            transform.TransformDirection(Vector2.right) * rayCastLength, Color.magenta);

        rcHit = Physics2D.Raycast(enemyRayCastOrigin.transform.position,
            transform.TransformDirection(Vector2.right), rayCastLength);
    }

    private void EnemyScaredRunAway()
    {
        r.AddRelativeForce(Vector2.right * runAwaySpeed * Time.deltaTime);
    }

    private void SetNewMoveTowardsVector()
    {
        newWaypoint = new Vector2(Random.Range(-range,range), Random.Range(-range, range));
    }

    private void OnDrawGizmos()
    {
        float sphereRadius = 0.15f;
        //Vector3 drawnSphereLocation = new Vector3(newWaypoint.x, newWaypoint.y, 0);
        Gizmos.DrawSphere(newWaypoint, sphereRadius);
    }

}
