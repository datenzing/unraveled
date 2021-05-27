using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtBox : MonoBehaviour
{

	public GameObject Player;
    [SerializeField] int damageValue = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerStay2D(Collider2D other){
    	// Debug.Log("you've entered the spikes");
    	if(other.tag == "Player"){
    		// Debug.Log("You dead, tell player to respawn");
            other.gameObject.GetComponent<PlayerManager>().damagePlayer(damageValue);
    	}
    }

    void OnCollisionStay2D(Collision2D other){
        // Debug.Log("you've entered the spikes");
        if(other.collider.tag == "Player"){
            // Debug.Log("You dead, tell player to respawn");
            other.gameObject.GetComponent<PlayerManager>().damagePlayer(damageValue);
        }
    }

}
