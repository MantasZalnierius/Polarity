using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletCollision : MonoBehaviour
{
    public Rigidbody2D rb;
    public GameObject particles;
    public Transform posPart;
    public Runtime2DMovement player;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
      
        StartCoroutine(spawnParticle());
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("player shot");
            player.decreaseHealth(5);

        }
    }

    IEnumerator spawnParticle()
    {
       

       GameObject particle = Instantiate(particles, posPart.transform.position, rb.transform.rotation);
       Destroy(particle, 3.0f);
       yield return new WaitForSeconds(0.0f);
        
    }
}