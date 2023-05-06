using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.Animations;

public class EnemyAI : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyRayCastOrigin;
    public Rigidbody r;
   
    private float speed =2f, runAwaySpeed = 10f, distance = .5f, maxDistance = 10f;
    
    private RaycastHit rcHit;

    Vector3 newWaypoint;
    Vector3 rayCastVector;

    private void Start()
    {
        r = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        DrawEnemyRayCast();

        transform.position = Vector3.MoveTowards(transform.position, newWaypoint, speed * Time.deltaTime);
        transform.LookAt(newWaypoint);
        
        if (Vector3.Distance(transform.position, newWaypoint) < distance)
        {
            SetNewMoveTowardsVector();
        }

        // If enmemy runs into player, quickly run away in a random direction.
        if (Physics.Raycast(enemyRayCastOrigin.transform.position, rayCastVector, out rcHit))
        {
            Debug.Log(gameObject.name + "Enemy hit " + rcHit.collider.name);

            if (rcHit.collider != null && rcHit.collider.CompareTag("Player"))
            {
                float randomTurnDir = Random.Range(-180, 180);
                transform.Rotate(0, randomTurnDir, 0);
                SetNewMoveTowardsVector();
                //EnemyScaredRunAway();
            }
        }
    }

    private void DrawEnemyRayCast()
    {
        // Create a raycast and check what it hits
        float rayCastLength = 2f;
        rayCastVector = enemyRayCastOrigin.transform.TransformDirection(Vector3.forward) * rayCastLength;

        Debug.DrawRay(enemyRayCastOrigin.transform.position, 
            rayCastVector, Color.magenta);

        
        //rcHit = Physics.Raycast(enemyRayCastOrigin.transform.position,
            //transform.TransformDirection(new Vector3(0,0,1)), rayCastLength);
    }

    private void EnemyScaredRunAway()
    {
        r.AddForceAtPosition(Vector3.forward * runAwaySpeed, transform.position, ForceMode.Acceleration);
    }

    private void SetNewMoveTowardsVector()
    {
        newWaypoint = new Vector3(Random.Range(-maxDistance, maxDistance), 0, Random.Range(-maxDistance, maxDistance));
    }

    private void OnDrawGizmos()
    {
        float sphereRadius = 0.1f;
        //Vector3 drawnSphereLocation = new Vector3(newWaypoint.x, newWaypoint.y, 0);
        Gizmos.DrawSphere(newWaypoint, sphereRadius);
    }

}
