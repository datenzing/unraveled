using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartDropper : MonoBehaviour
{
	public GameObject foot;
	public GameObject pelvis;
    public GameObject torso;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // add: flip check, delay parameter
    public GameObject DropPart(Part part, float xOffset, float yOffset, float xVelocity, float yVelocity) {
        GameObject partObject;
        switch (part)
        {
            case Part.Foot:
                partObject = foot;
                break;

            case Part.Pelvis:
                partObject = pelvis;
                break;

            case Part.Torso:
                partObject = torso;
                break;

            default:
                return null;
        }

    	GameObject newPart = Instantiate(partObject, transform.position + new Vector3(xOffset, yOffset, 0), transform.rotation);

        //Have the player be able to walk through body parts
        if(part == Part.Foot){
            BoxCollider2D bodyCollider = newPart.GetComponent<BoxCollider2D>();
            Collider2D playerCollider = gameObject.GetComponent<Collider2D>();
            Physics2D.IgnoreCollision(bodyCollider, playerCollider, true);
        }
        

    	newPart.GetComponent<Rigidbody2D>().velocity = new Vector3(xVelocity, yVelocity, 0);
        return newPart;
    }
}
