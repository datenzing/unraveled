using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
	[SerializeField] bool rope;
	[SerializeField] bool wood;
	[SerializeField] bool nails;
	[SerializeField] GameObject canvasListener;
    // Start is called before the first frame update
    
	public void itemAcquired(Collectable collectable){
		switch(collectable){
			case Collectable.Rope:
				rope = true;
				canvasListener.GetComponent<CanvasItemManager>().itemCollected(collectable);
				break;
			case Collectable.Wood:
				wood = true;
				canvasListener.GetComponent<CanvasItemManager>().itemCollected(collectable);
				break;
			case Collectable.Nails:
				nails = true;
				canvasListener.GetComponent<CanvasItemManager>().itemCollected(collectable);
				break;
			default:
				break;
		}
	}

	public bool haveAllCollectables(){
		return rope && wood && nails;
	}

	public bool haveRope(){return rope;}
	public bool haveWood(){return wood;}
	public bool haveNails(){return nails;}
}
