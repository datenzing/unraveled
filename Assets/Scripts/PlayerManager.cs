using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
	private int health;
	[SerializeField] int maxHealth;
	[SerializeField] GameObject defaultCheckpoint;
	private PartsTracker partsTracker;
	private GameObject checkpoint;


    // Start is called before the first frame update
    void Awake()
    {

        health = maxHealth;
        partsTracker = gameObject.GetComponent<PartsTracker>();
        setCheckpoint(defaultCheckpoint);
    }

    void Update(){
    	if(Input.GetKeyDown("g")){
    		damagePlayer(maxHealth);
    	}
    }

    private void respawn(){
    	health = maxHealth;

    	if(checkpoint.GetComponent<Checkpoint>().hasTorso() && !partsTracker.hasTorso()){
    		partsTracker.Add(Part.Torso);
    	}
    	if(checkpoint.GetComponent<Checkpoint>().hasPelvis() && !partsTracker.hasPelvis()){
    		partsTracker.Add(Part.Pelvis);
    	}
    	for(int i = checkpoint.GetComponent<Checkpoint>().getHands() - partsTracker.getHands(); i > 0; i--){
    		partsTracker.Add(Part.Hand);
    	}
    	for(int i = checkpoint.GetComponent<Checkpoint>().getFeet() - partsTracker.getFeet(); i > 0; i--){
    		partsTracker.Add(Part.Foot);
    	}
        checkpoint.GetComponent<Checkpoint>().resetDrips();
    	transform.position = checkpoint.transform.position;


    }

    public void damagePlayer(int damage){
    	health -= damage;

    	if(health <= 0){
    		respawn();
    	}
    }

    public void setCheckpoint(GameObject checkpoint){
    	this.checkpoint = checkpoint;
    	// this.checkpoint.GetComponent<Checkpoint>().checkPointSet(gameObject);
    }
}
