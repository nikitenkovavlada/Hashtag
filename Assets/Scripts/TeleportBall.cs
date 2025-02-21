using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportBall : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Ground"))
        {
            TeleportPlayer();
        }
    }

    void TeleportPlayer()
    {
        GameObject.FindGameObjectWithTag("Player").transform.position = transform.position + new Vector3(0, 3, 0);
        Destroy(gameObject);    
    }
}
