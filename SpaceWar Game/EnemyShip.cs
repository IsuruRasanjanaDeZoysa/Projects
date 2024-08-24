using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShip : MonoBehaviour
{
    public RuntimeAnimatorController explosion;
    public float speed ;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0.0f, speed, 0.0f);

        if (transform.position.y < -5.0f)
        {
            Destroy(this.gameObject);
            ShipGameMode.enemiesCount -= 1;
            if(ShipGameMode.enemiesCount == 0) { ShipGameMode.enemiesExists = false; }
        }
    }

    void OnCollisionStay(Collision collisionInfo)
    {
        
        if (collisionInfo.gameObject.CompareTag("Bullet"))
        {
            StartCoroutine(destroyActor(collisionInfo.gameObject));
        }
    }

    public IEnumerator destroyActor(GameObject bullet)
    {
        ShipGameMode.numofDesEnemyShips += 1;
        if (bullet != null)
        {
            Destroy(bullet);
        }
        GetComponent<Collider>().enabled = false;
        GetComponent<Animator>().runtimeAnimatorController = explosion;
        yield return new WaitForSeconds(2.0f);
        ShipGameMode.enemiesCount -= 1;
        if (ShipGameMode.enemiesCount == 0) { ShipGameMode.enemiesExists = false; }
        if (this!=null)
        {
            Destroy(this.gameObject);
        }       
    }
}
