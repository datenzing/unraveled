using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
	private Animator anim;
	private Collider2D doorCollider;
	private Vector2 offsetClosed = new Vector2(0, -8);
	private Vector2 offsetOpen = new Vector2(0, 8);
    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        anim.SetBool("Open", false);

        doorCollider = gameObject.GetComponent<BoxCollider2D>();
        doorCollider.offset = offsetClosed;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenDoor(){
    	anim.SetBool("Open", true);
    	doorCollider.offset = offsetOpen;
        doorCollider.enabled = false;

    }

    public void CloseDoor(){
    	anim.SetBool("Open", false);
    	doorCollider.offset = offsetClosed;
        doorCollider.enabled = true;
    }
}
