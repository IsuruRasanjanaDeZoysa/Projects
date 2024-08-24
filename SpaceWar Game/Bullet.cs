using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0.0f, speed, 0.0f);
        if (transform.position.y > 8.0f)
        {
            Destroy(this.gameObject);
        }
    }

}
