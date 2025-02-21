using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
      
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Ground"))
        {
            Main.trig = true;
        }
        
        if (collision.gameObject.tag.Equals("Flag"))
        {
            Main.able_tograb_flag = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Ground"))
        {
            Main.trig = false;
        }
        if (collision.gameObject.tag.Equals("Flag"))
        {
            Main.able_tograb_flag = false;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
