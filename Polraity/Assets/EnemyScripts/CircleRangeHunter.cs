using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleRangeHunter : MonoBehaviour
{

    public HunterScript hunter;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            hunter.hurtPlayer = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            hunter.hurtPlayer = false;
        }
    }
}
