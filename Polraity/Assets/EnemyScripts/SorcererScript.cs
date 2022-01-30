using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SorcererScript : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator animator;
    public GameObject player;
    Vector2 savedlocalScale;

    public float sorcererHealth;
    public float randomDamage;
    bool sorcererDead = false;
    public bool hurtPlayer = false;
    public bool seekPlayer = false;
    public float Bulletspeed = 2;

    public GameObject directionCircle;
    public Transform leftPosition;
    public Transform rightPosition;
    public temporatyPlayer playerSpeed;
    public Transform firePoint;
    public GameObject bullet;
    float shootingTime;
    float theta;

    public GameObject deathParticle;
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


        //setting the rotation for the enemy depending on the side they are facing              
        if (hurtPlayer == false && sorcererDead == false)
        {
            animator.SetBool("attack", false);
            if (player.transform.position.x > rb.transform.position.x)
            {
                transform.localScale = new Vector2(savedlocalScale.x, savedlocalScale.y);
            }
            if (player.transform.position.x < rb.transform.position.x)
            {
                
                transform.localScale = new Vector2(-savedlocalScale.x, savedlocalScale.y);
            }
        }


        if (hurtPlayer == true)
        {
            animator.SetBool("attack", true);
           
        }
       
        if (rb.velocity.x == 0.0f && animator.GetBool("attack") == false)
        {
        

        }



    }
    private void Update()
    {


            Vector3 dir = player.transform.position - firePoint.transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            firePoint.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        



        angleVisionCone();
       

    }

    void damage()
    {   //this creates random damage for the enemy
        if (sorcererHealth <= 0)
        {
 
           
            sorcererDead = true;
            GameObject particles = Instantiate(deathParticle, rb.transform.position, deathParticle.transform.rotation);
            Destroy(particles, 3.0f);
            Destroy(this.gameObject);
        }
        randomDamage = Random.Range(5.0f, 15.0f);
        sorcererHealth -= randomDamage;

    }

    public IEnumerator hurtTheEnemy()
    {

        damage();
        yield return new WaitForSeconds(0.4f);

        Debug.Log("after 0.4 seconds");


    }


    void shoot()
    {
        StartCoroutine(wait());
        if (shootingTime <= 0)
        {
            shootingTime = 0.8f;



            GameObject bulletspawn = Instantiate(bullet, firePoint.position, firePoint.rotation);
            Rigidbody2D rbBullet = bulletspawn.GetComponent<Rigidbody2D>();
            rbBullet.AddForce(firePoint.up * Bulletspeed, ForceMode2D.Impulse);

       
            Destroy(bulletspawn, 3.0f);
           
        }
        shootingTime -= Time.deltaTime;
    }

    void angleVisionCone()
    {
        Vector2 enemyDirection = directionCircle.transform.position - this.transform.position;
        Vector2 playerDirection = player.transform.position - this.transform.position;
       

        Vector2 firstNormal = new Vector2(rb.position.x - leftPosition.transform.position.x, rb.position.y - leftPosition.transform.position.y);
        Vector2 secondNormal = new Vector2(rb.position.x - rightPosition.transform.position.x, rb.position.y - rightPosition.transform.position.y);
        float FOV = Mathf.Acos(Vector2.Dot(firstNormal, secondNormal) /
                                            (firstNormal.magnitude * secondNormal.magnitude));
        FOV *= Mathf.Rad2Deg;

        theta = Vector2.Angle(playerDirection, enemyDirection);
            if (theta < FOV / 2)
                {

                      // rb.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0);   ///  <----- debug
            if (hurtPlayer == true)
                 {
                       shoot();
                  }
                }
            else
             {
                 //  rb.GetComponent<SpriteRenderer>().color = new Color(0, 1, 0);   /// <----- debug
             }
           
  
        

    }


    IEnumerator wait()
    {
        yield return new WaitForSeconds(2.0f);
    
    }
}
