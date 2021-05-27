using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPart : MonoBehaviour
{
	public Part bodyPart;
	[SerializeField] GameObject player;
    // Start is called before the first frame update
    void Start()
    {
    	// if(player != null){
    	// 	Collider2D pCollider = player.GetComponent<Collider2D>();
    	// 	if(player.tag == "Player"){
    	// 		Debug.Log("In if statement");
    	// 		Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), pCollider, true);
    	// 	}
    		
    	// }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Part getBodyPart(){
    	return bodyPart;
    }
}
