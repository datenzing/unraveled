using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeManager : MonoBehaviour
{
	private SpriteRenderer renderer_Behind;
	private SpriteRenderer renderer_Front;
	private Collider2D bridgeCollider;
	[SerializeField] GameObject bridge_Behind;
	[SerializeField] GameObject bridge_Front;
	[SerializeField] Sprite bridgeFixedSprite_Behind;
	[SerializeField] Sprite bridgeDecayedSprite_Behind;
	[SerializeField] Sprite bridgeFixedSprite_Front;
	[SerializeField] Sprite bridgeDecayedSprite_Front;
	[SerializeField] bool bridgeFixed;
    // Start is called before the first frame update
    void Start()
    {
        // bridge_Behind = gameObject.transform.GetChild(0).gameObject;
        // bridge_Front = gameObject.transform.GetChild(1).gameObject;
        renderer_Behind = bridge_Behind.GetComponent<SpriteRenderer>();
        renderer_Front = bridge_Front.GetComponent<SpriteRenderer>();

        bridgeCollider = bridge_Behind.GetComponent<Collider2D>();
        // bridgeCollider.enabled = true;
        // bridge_Behind.AddComponent<CircleCollider2D>();
        changeBridgeState(bridgeFixed);
        // updateCollider(bridge_Behind);

    }

    private void updateCollider(){
    	Destroy(bridge_Behind.GetComponent<PolygonCollider2D>());
    	bridge_Behind.AddComponent<PolygonCollider2D>();
    	// bridgeCollider = bridge.AddComponent<PolygonCollider2D>();

    }

    public void changeBridgeState(bool bridgeFixed){
    	if(bridgeFixed){
        	renderer_Behind.sprite = bridgeFixedSprite_Behind;
        	renderer_Front.sprite = bridgeFixedSprite_Front;
        }
        else {
        	renderer_Behind.sprite = bridgeDecayedSprite_Behind;
        	renderer_Front.sprite = bridgeDecayedSprite_Front;
        }
        updateCollider();
        this.bridgeFixed = bridgeFixed;

    }

    public bool isFixed(){
    	return bridgeFixed;
    }
}
