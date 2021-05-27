using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractPressurePlate : MonoBehaviour {
    
    private float timer;
    [SerializeField] DoorScript[] doors;
    [SerializeField] float maxTime = 1;
    [SerializeField] bool onTimer;
    [SerializeField] Sprite active;
    [SerializeField] Sprite inactive;
    private int inTrigger; 

    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update() {
        if (timer > 0 && onTimer) {
        	timer -= Time.deltaTime;
        	if (timer <= 0f) {
                foreach(DoorScript door in doors){
                    door.CloseDoor();
                    spriteRenderer.sprite = inactive;
                }
            }
        }
        // isActive = false;

    }


    void OnTriggerEnter2D(Collider2D other) {
    	if(other.tag == "Player" || other.tag == "BodyPart"){
            inTrigger++;
            foreach(DoorScript door in doors){
    		  door.OpenDoor();
    		  spriteRenderer.sprite = active;
            }
           

    	}
    	// if (collision.gameObject.GetComponent<Collider2D>() != null) {
    	// 	// Player hit collider
    	// 	door.OpenDoor();
    	// }
    }

    // void OnTriggerStay2D(Collider2D other){
    //     if(other.tag == "Player" || other.tag == "BodyPart"){
    //         isActive = true;
    //     }
    // }

    // void OnCollisionStay2D(Collision2D collision) {
    // 	if (collision.gameObject.GetComponent<Collider2D>() != null) {
    // 		timer = 1f;
    // 	}
    // }

    void OnTriggerExit2D(Collider2D other){

        if (other.tag == "Player" || other.tag == "BodyPart") {
            inTrigger--;
            if(inTrigger <= 0){
                timer = maxTime;
            }
            
        }
    }
}
