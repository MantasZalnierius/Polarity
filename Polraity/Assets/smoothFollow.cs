using System.Collections;
using System.Collections.Generic;
using UnityEngine;
											//attach this script to the camera
public class smoothFollow : MonoBehaviour
{
	public float speed = 15f;
	public float minDistance;
	public GameObject target;		// the thing we want to follow
	public Vector3 offset;
	
	
	private Vector3 targetPos;

	// Use this for initialization
	void Start()
	{
		targetPos = transform.position;
	}

	// Update is called once per frame
	void Update()
	{
		//lerping to the position of player 
		if (target)
		{			
			Vector3 posNoZ = transform.position + offset;												//offsetting the position the the cam
			Vector3 targetDirection = (target.transform.position - posNoZ);								//grabbing the direction the target is going 
			float interpVelocity = targetDirection.magnitude * speed;									//setting velocity
			targetPos = (transform.position) + (targetDirection.normalized * interpVelocity * Time.deltaTime);  //setting the target position aka players pos, this will track player pos 																											
			transform.position = Vector3.Lerp(transform.position, targetPos, 0.25f);							//using the lerp method to make it move towards the players pos 

		}
	}
}