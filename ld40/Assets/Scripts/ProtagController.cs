﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtagController : MonoBehaviour {
    Rigidbody body;
    float moveForce;
    float maxVelocity;
    List<GameObject> keys;

    GameObject playerInventory;
    private bool faded;

	// Use this for initialization
	void Start () {
        body = GetComponent<Rigidbody>();
        maxVelocity = 30f;
    
        keys = new List<GameObject>();
        playerInventory = GameObject.Find("InventoryGrid");

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

        fadeOut();
            
 //       Debug.Log(keys.Count);
 //         Debug.Log(faded);

    }

    void OnCollisionEnter(Collision coll) {
            
                if(coll.gameObject.tag == "collectible")
		{
			 Debug.Log("collect it ");
                
                playerInventory.GetComponent<CanvasGroup>().alpha = 1f;
                faded = false;
                Destroy(coll.gameObject);
                
                if(keys.Count > 0)
                {
                GameObject newSlot = Instantiate(GameObject.Find("Slot"));
                newSlot.transform.parent = GameObject.Find("Slot Panel").transform;
                newSlot.transform.localScale = new Vector3(1, 1, 1);
                keys.Add(newSlot);
                } else {
                        keys.Add(GameObject.Find("Slot"));
                        }
                
                
            //if(coll.gameObject.tag == "key")

		}                 
          
    }
         // fading out the alpha of the canvas
         void fadeOut()  {
                            
                        if(faded)
                            return;
                        
                        if(!faded) 
                                {
                                playerInventory.GetComponent<CanvasGroup>().alpha -= .01f * Time.deltaTime * 60;
                                // Debug.Log("it is fading");
                                }

                         if(playerInventory.GetComponent<CanvasGroup>().alpha <= .001f)
                                {
                                    faded = true;
                                }                
                          } 
}
