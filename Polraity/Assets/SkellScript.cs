using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkellScript : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator animator;
    public float speed = 2;
    public GameObject player;
    Vector2 savedlocalScale;

    public float skellyHeath;
    public float randomDamage;
    bool skellydead = false;
    bool hurtPlayer = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        savedlocalScale = transform.localScale;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (Input.GetKey(KeyCode.H))        //place holder for hurting th eenemy 
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

        //
        if (rb.velocity.x == 0.0f)
        {
            animator.SetFloat("speed", Mathf.Abs(0));

        }


        if (hurtPlayer == true)
        {
            Debug.Log("hurt");
            animator.SetBool("death", true);

        }


    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            hurtPlayer = true;
        }
    }
    void damage()
    {   //this creates random damage for the enemy
        if (skellyHeath <= 0)
        {
            speed = 0;
            animator.SetBool("death", true);
            animator.SetFloat("speed", Mathf.Abs(0));
            rb.velocity = new Vector2(speed, 0);
            skellydead = true;
        }
        randomDamage = 10.0f;
        skellyHeath -= randomDamage;

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
