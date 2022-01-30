using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletCollision : MonoBehaviour
{
    public Rigidbody2D rb;
    public GameObject particles;
    public Transform posPart;
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
            //Destroy(collision.gameObject);

        }
    }

    IEnumerator spawnParticle()
    {
       

       GameObject particle = Instantiate(particles, posPart.transform.position, rb.transform.rotation);
       Destroy(particle, 3.0f);
       yield return new WaitForSeconds(0.0f);
        
    }
}