using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoofObject : MonoBehaviour
{
	// public GameObject player;
	public Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
    	// Vector3 playerLocation = player.transform.position;

     //    transform.position = playerLocation + offset;
     //    Debug.Log("player position: " + playerLocation);
     //    Debug.Log("poof position: " + transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector3 Offset(){return offset;}
}
