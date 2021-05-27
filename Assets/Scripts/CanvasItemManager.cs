using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasItemManager : MonoBehaviour
{
	[SerializeField] GameObject nails;
	[SerializeField] GameObject rope;
	[SerializeField] GameObject wood;
	private Color alphaHalf = new Color(0.5f, 0.5f, 0.5f, 0.5f);
	private Color alphaFull = new Color(1f, 1f, 1f, 1f);
    // Start is called before the first frame update
    void Start()
    {
        nails.GetComponent<Image>().color = alphaHalf;
        rope.GetComponent<Image>().color = alphaHalf;
        wood.GetComponent<Image>().color = alphaHalf;
    }

    public void itemCollected(Collectable item){
    	switch(item){
    		case Collectable.Nails:
    			nails.GetComponent<Image>().color = alphaFull;
    			break;
    		case Collectable.Rope:
    			rope.GetComponent<Image>().color = alphaFull;
    			break;
    		case Collectable.Wood:
    			wood.GetComponent<Image>().color = alphaFull;
    			break;
    		default:
    			break;
    	}

    }
}
