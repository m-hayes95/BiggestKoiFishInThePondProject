using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float speed = 3f;

    private void FixedUpdate()
    {
        Debug.Log("Player's Current Size = " + transform.localScale);
        transform.Translate(PlayerMovementVector() * speed * Time.deltaTime);
    }

    private Vector3 PlayerMovementVector()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 moveDir = new Vector3(h, v, 0);
        return moveDir;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            if (collision.transform.localScale.x > transform.localScale.x)
            {
                // If enemy is bigger, take 1 hit point.
                //Debug.Log("Player has been hit");
            }

            if (collision.transform.localScale.x <= transform.localScale.x)
            {
                // If enemy is smaller, eat
                //Debug.Log("Player has eaten smaller enemy");
                Destroy(collision.gameObject);
                PlayerEatsEnemy();
            }

        }
    }

    private void PlayerEatsEnemy()
    {
        // Set new player size when they eat a smaller or same size fish.
        Debug.Log("Player Size increase");
        Vector3 currentPlayerSize = transform.localScale;
        float playerSizeMultipler = 1.5f;
        Vector3 newPlayerSizeAfterEating = 
            new Vector3(currentPlayerSize.x * playerSizeMultipler, 
            currentPlayerSize.y * playerSizeMultipler, 
            currentPlayerSize.z);
        transform.localScale = newPlayerSizeAfterEating;
    }
}
