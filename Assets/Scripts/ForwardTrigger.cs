using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForwardTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Ground"))
        {
            Main.wallTriggered = true;
        }
        if (collision.gameObject.tag.Equals("Flag"))
        {
            Main.ableToGrabFlag = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Ground"))
        {
            Main.wallTriggered = false;
        }
        if (collision.gameObject.tag.Equals("Flag"))
        {
            Main.ableToGrabFlag = false;
        }
    }
}
