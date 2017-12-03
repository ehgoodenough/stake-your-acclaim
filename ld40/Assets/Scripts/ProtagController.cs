﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtagController : MonoBehaviour {
    Rigidbody body;
    float moveForce;
    float maxVelocity;
    List<GameObject> keys;

	// Use this for initialization
	void Start () {
        body = GetComponent<Rigidbody>();
        maxVelocity = 30f;
    
        keys = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 position = transform.position;
        Vector3 inputVelocity = Vector3.zero;
        Vector3 moveDirection = Vector3.zero;
        inputVelocity += new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        moveDirection = inputVelocity.normalized;
        float moveForce = Mathf.Max((maxVelocity - body.velocity.magnitude), 0);
        if (inputVelocity.magnitude > 0.99f)
        {
            body.drag = 1;
            body.AddForce(inputVelocity*moveForce*Time.deltaTime*60);
        } else
        {
            body.drag = 4;
        }
        if(body.velocity.magnitude > 0.1f)
        {
            transform.LookAt(transform.position + body.velocity);
        }

 //       Debug.Log(keys.Count);

    }

    void OnCollisionEnter(Collision coll) {
            
                if(coll.gameObject.tag == "collectible")
		{
			 Debug.Log("collect it ");
                Destroy(coll.gameObject);
                GameObject newSlot = Instantiate(GameObject.Find("Slot"));
                newSlot.transform.parent = GameObject.Find("Slot Panel").transform;
                newSlot.transform.localScale = new Vector3(1, 1, 1);

                keys.Add(newSlot);
                
            //if(coll.gameObject.tag == "key")
            //    {
            //       keys.Add(newSlot);
            //    }
		}                  
          
    }

}
