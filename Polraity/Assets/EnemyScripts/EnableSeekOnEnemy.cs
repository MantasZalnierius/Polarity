using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableSeekOnEnemy : MonoBehaviour
{
    public SkellScript skelly;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            skelly.seekPlayer = true;
        }
    }
    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Player"))
    //    {
    //        skelly.seekPlayer = false;
    //    }
    //}
}
