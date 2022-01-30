using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableSeekOnHunter : MonoBehaviour
{
    public HunterScript hunter;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            hunter.seekPlayer = true;
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
