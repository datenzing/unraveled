using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColliderManager : MonoBehaviour
{
	[SerializeField] IconManager iconListener;
	[SerializeField] ItemManager itemListener;
	private PartsTracker pt;
	private bool canInteract;
	private GameObject interactedObject;
    // Start is called before the first frame update
    void Start()
    {
        pt = gameObject.GetComponent<PartsTracker>();
    }

    // Update is called once per frame
    void Update()
    {
        if(canInteract){
        	if(Input.GetKeyDown("q")){
        		switch(interactedObject.tag){
                    //Collect your body part
                    case "BodyPart":
                        Part bodyPart = interactedObject.GetComponent<BodyPart>().getBodyPart();
                        pt.Add(bodyPart);
                        Destroy(interactedObject);
                        break;
                    // Collect the item
                    case "Item":
                        Collectable item = interactedObject.GetComponent<Item>().getItemType();
                        itemListener.itemAcquired(item);
                        Debug.Log("Items so far: Wood: " + itemListener.haveWood() + " Rope: " + itemListener.haveRope() + " Nails: " + itemListener.haveNails());
                        Destroy(interactedObject);
                        break;
                    //Fix the bridge with all the necessary items
                    case "ItemCheckpoint":
                        interactedObject.GetComponent<BridgeManager>().changeBridgeState(true);
                        break;
                    case "Chindrip":
                        Part chindripPart = interactedObject.GetComponent<Chindrip>().getPart();
                        pt.Add(chindripPart);
                        Debug.Log("Trying to eat drip");
                        interactedObject.GetComponent<Chindrip>().Eat();
                        break;
                    default:
                        break;
        		}
                interactedObject = null;
        	}
        }
    }

    // void OnCollisionEnter2D(Collision2D other){
    // 	// interactedObject = other.gameObject;
    //  //    other.collider.tag == "Player"
    // 	// if(other.collider.tag == "BodyPart"){
    // 	// 	Part bodyPart = other.gameObject.GetComponent<BodyPart>().getBodyPart();
    // 	// 	iconListener.IconToggle(true);
    // 	// 	canInteract = pt.CanAdd(bodyPart);
    // 	// 	iconListener.CanInteract(canInteract);

    // 	// }
    // }

    // void OnCollisionStay2D(Collision2D other){
    // 	interactedObject = other.gameObject;
    // 	if(other.collider.tag == "BodyPart"){
    // 		Part bodyPart = other.gameObject.GetComponent<BodyPart>().getBodyPart();
    // 		iconListener.IconToggle(true);
    // 		canInteract = pt.CanAdd(bodyPart);
    // 		iconListener.CanInteract(canInteract);

    // 	}
    // }

    // void OnCollisionExit2D(Collision2D other){
    // 	interactedObject = null;
    // 	if(other.collider.tag == "Item"){
    // 		iconListener.IconToggle(false);
    // 		canInteract = false;
    // 	}
    // 	else if(other.collider.tag == "BodyPart"){
    // 		iconListener.IconToggle(false);
    // 		canInteract = false;
    // 	}
    // }

    // void OnTriggerEnter2D(Collider2D other){
    // 	interactedObject = other.gameObject;
    // 	if(other.tag == "Item"){
    // 		iconListener.IconToggle(true);
    // 		canInteract = true;
    // 		iconListener.CanInteract(true);
    // 	}
    // 	else if(other.tag == "BodyPart"){
    // 		Part bodyPart = other.gameObject.GetComponent<BodyPart>().getBodyPart();
    // 		iconListener.IconToggle(true);
    // 		canInteract = pt.CanAdd(bodyPart);
    // 		iconListener.CanInteract(canInteract);
    //     }
    //     else if(other.tag == "ItemCheckpoint"){
    //         canInteract = itemListener.haveAllCollectables();
    //         iconListener.IconToggle(true);
    //         iconListener.CanInteract(canInteract);
    //     }
    // }

    void OnTriggerStay2D(Collider2D other){
        // interactedObject = other.gameObject;
        interactedObject = getTriggerHierarchy(other);
        if(interactedObject.tag == "Item"){
            iconListener.IconToggle(true);
            canInteract = true;
            iconListener.CanInteract(true);
        }
        else if(interactedObject.tag == "BodyPart"){
            Part bodyPart = interactedObject.gameObject.GetComponent<BodyPart>().getBodyPart();
            iconListener.IconToggle(true);
            canInteract = pt.CanAdd(bodyPart);
            iconListener.CanInteract(canInteract);

        }
        else if(interactedObject.tag == "ItemCheckpoint"){
            if(!interactedObject.gameObject.GetComponent<BridgeManager>().isFixed()){
                canInteract = itemListener.haveAllCollectables();
                iconListener.IconToggle(true);
            }
            else{
                canInteract = false;
                iconListener.IconToggle(false);
            }
            iconListener.CanInteract(canInteract);
        }

        else if (interactedObject.tag == "Chindrip") {
            // canInteract = interactedObject.gameObject.GetComponent<Chindrip>().CanEat();
            Chindrip chindrip = interactedObject.gameObject.GetComponent<Chindrip>();
            canInteract = pt.CanAdd(chindrip.getPart()) && chindrip.CanEat();
            iconListener.IconToggle(true);
            iconListener.CanInteract(canInteract);
        }
    }

    void OnTriggerExit2D(Collider2D other){
    	interactedObject = null;
    	if(other.tag == "Item"){
    		iconListener.IconToggle(false);
    	}
    	else if(other.tag == "BodyPart"){
    		iconListener.IconToggle(false);

    	}
        else if(other.tag == "ItemCheckpoint"){
            iconListener.IconToggle(false);
        }
        else if(other.tag == "Chindrip"){
            iconListener.IconToggle(false);
        }
        canInteract = false;

    }

    private GameObject getTriggerHierarchy(Collider2D other){
        if(interactedObject != null){
            GameObject tempObject = interactedObject;
            switch(tempObject.tag){
                case "Item":
                    break;
                case "BodyPart":
                    if(other.tag == "Item"){
                        tempObject = other.gameObject;
                    }
                    break;
                case "ItemCheckpoint":
                    if(other.tag == "Item" || other.tag == "BodyPart"){
                        tempObject = other.gameObject;
                    }
                    break;
                case "Chindrip":
                    if (other.tag == "Item" || other.tag == "BodyPart" || other.tag == "ItemCheckpoint")
                    {
                        tempObject = other.gameObject;
                    }
                    break;
                default:
                    tempObject = other.gameObject;
                    break;

            }
            return tempObject;
        }
        else return other.gameObject;
    }
      
}
