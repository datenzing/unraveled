using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
	[SerializeField] Collectable item;
    
    public Collectable getItemType(){
    	return item;
    }
}
