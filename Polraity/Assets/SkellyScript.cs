using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkellyScript : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator animator;
    public float speed = 2;
    public Transform player;
    Vector2 savedlocalScale;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        savedlocalScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
       

        if (rb.velocity.x < 0.001f)
        { 
            animator.SetFloat("speed", Mathf.Abs(speed));
        }
        if (rb.velocity.x > 0.001f)
        {
            animator.SetFloat("speed", Mathf.Abs(speed));
        }


        if(player.transform.position.x > rb.transform.position.x)
        {
            rb.velocity = new Vector2(speed, 0);
            transform.localScale = new Vector2(savedlocalScale.x, savedlocalScale.y);
        }
        if (player.transform.position.x < rb.transform.position.x)
        {
            rb.velocity = new Vector2(-speed, 0);
            transform.localScale = new Vector2(-savedlocalScale.x, savedlocalScale.y);
        }


        if (rb.velocity.x == 0.0f)
        {
            animator.SetFloat("speed", Mathf.Abs(0));
        }
    }
}
