using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunterScript : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator animator;
    public float speed = 1;
    public GameObject player;
    Vector2 savedlocalScale;

    public float HunterHealth;
    public float randomDamage;
    bool hunterdead = false;
    public bool hurtPlayer = false;
    public bool seekPlayer = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        savedlocalScale = transform.localScale;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (Input.GetKey(KeyCode.H))        //place holder for hurting th eenemy                <-------- if player will hit them
        {
            StartCoroutine(hurtTheEnemy());
        }


        if (rb.velocity.x < 0.001f)
        {
            animator.SetFloat("speed", Mathf.Abs(speed));
        }
        if (rb.velocity.x > 0.001f)
        {
            animator.SetFloat("speed", Mathf.Abs(speed));
        }

        //setting the rotation for the enemy depending on the side they are facing              
        if (hurtPlayer == false && hunterdead == false && seekPlayer == true)
        {
            animator.SetBool("attack", false);
            speed = 1;
            if (player.transform.position.x > rb.transform.position.x)
            {
                rb.velocity = new Vector2(speed, 0);
                transform.localScale = new Vector2(savedlocalScale.x, savedlocalScale.y);
            }
            if (player.transform.position.x < rb.transform.position.x)
            {
                rb.velocity = new Vector2(-speed, 0);
                transform.localScale = new Vector2(-savedlocalScale.x, savedlocalScale.y);
            }
        }


        if (hurtPlayer == true)
        {
            animator.SetBool("attack", true);
            speed = 0;
        }
        //
        if (rb.velocity.x == 0.0f && animator.GetBool("attack") == false)
        {
            animator.SetFloat("speed", Mathf.Abs(0));

        }



    }


    void damage()
    {   //this creates random damage for the enemy
        if (HunterHealth <= 0)
        {
            speed = 0;
            animator.SetBool("death", true);
            animator.SetFloat("speed", Mathf.Abs(0));
            rb.velocity = new Vector2(speed, 0);
            hunterdead = true;
        }
        randomDamage = Random.Range(5.0f, 15.0f);
        HunterHealth -= randomDamage;

    }

    public IEnumerator hurtTheEnemy()
    {
        animator.SetBool("hurt", true);
        damage();
        yield return new WaitForSeconds(0.4f);

        Debug.Log("after 0.4 seconds");

        animator.SetBool("hurt", false);
    }
}
