using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
	public GameObject player;
	public Sprite checkpointOff;
	public Sprite checkpointOn;

	private SpriteRenderer checkpointSpriteRenderer;
	public bool checkpointReached;
    [SerializeField] GameObject[] drips;
    private int feet;
    private int hands;
    private bool pelvis;
    private bool torso;

    // Start is called before the first frame update
    void Start()
    {
        checkpointSpriteRenderer = GetComponent<SpriteRenderer>();

        if(checkpointReached){
        	checkpointSpriteRenderer.sprite = checkpointOn;
        }
        else checkpointSpriteRenderer.sprite = checkpointOff;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other){
    	if(other.tag == "Player"){
    		checkPointSet(other.gameObject);
    	}
    }

    public void checkPointSet(GameObject player){
        checkpointSpriteRenderer.sprite = checkpointOn;
        checkpointReached = true;
        player.GetComponent<PlayerManager>().setCheckpoint(gameObject);

        feet = player.GetComponent<PartsTracker>().getFeet();
        hands = player.GetComponent<PartsTracker>().getHands();
        pelvis = player.GetComponent<PartsTracker>().hasPelvis();
        torso = player.GetComponent<PartsTracker>().hasTorso();

        gameObject.GetComponent<Collider2D>().enabled = false;
        // Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), player.GetComponent<Collider2D>(), true);

    }

    public int getFeet(){
        return feet;
    }
    public int getHands(){
        return hands;
    }
    public bool hasPelvis(){
        return pelvis;
    }
    public bool hasTorso(){
        return torso;
    }

    public void resetDrips(){
        foreach(GameObject drip in drips){
            drip.GetComponent<Chindrip>().Reset();
        }
    }
}
