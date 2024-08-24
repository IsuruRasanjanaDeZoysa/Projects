using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    public GameObject bulletTemplate;
    public float health = 100.0f;
    public ShipGameMode gameMode;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameMode.gameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                GameObject bullet = Instantiate(bulletTemplate,
                        transform.position + new Vector3(0.0f, 1.0f, 0.0f),
                        transform.rotation);
                GetComponent<AudioSource>().Play();
            }
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += new Vector3(-0.01f, 0.0f, 0.0f);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += new Vector3(+0.01f, 0.0f, 0.0f);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += new Vector3(0.0f, 0.01f, 0.0f);
        }
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += new Vector3(0.0f, -0.01f, 0.0f);
        }
    }

    void OnCollisionStay(Collision collisionInfo)
    {
        if (collisionInfo.gameObject.CompareTag("EnemyShip"))
        {
            health = health - 10.0f;
            EnemyShip enemy = collisionInfo.gameObject.GetComponent<EnemyShip>();
            StartCoroutine(enemy.destroyActor(null));

            if (health <= 0.0f)
            {
                //Set game over in the game mode object
                gameMode.gameOver = true;
                //Disable further collisions with the ship
                Debug.Log("gameOver");
                GetComponent<Collider>().enabled = false;
                //Make the ship invisible
                GetComponent<SpriteRenderer>().enabled = false;  

            }
        }
    }


}
