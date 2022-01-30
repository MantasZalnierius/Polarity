using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class circleRangeSorc : MonoBehaviour
{

    public SorcererScript sorc;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            sorc.hurtPlayer = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            sorc.hurtPlayer = false;
        }
    }
}
