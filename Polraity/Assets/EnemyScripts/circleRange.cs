using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class circleRange : MonoBehaviour
{

    public SkellScript skelly;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            skelly.hurtPlayer = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            skelly.hurtPlayer = false;
        }
    }
}
