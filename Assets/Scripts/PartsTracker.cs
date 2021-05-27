using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartsTracker : MonoBehaviour
{

    GameObject character;
    Animator anim;

    public GameObject poof;

	// True if character currently has these parts
    [SerializeField] private int hands = 2;
    [SerializeField] private int feet = 2;
	[SerializeField] private bool pelvis = true;
	[SerializeField] private bool torso = true;

    private Part selected;
    private bool active;
    private BoxCollider2D collider;

    // Object pointers
    private GameObject hand1Object;
    private GameObject hand2Object;
    private GameObject foot1Object;
    private GameObject foot2Object;
    private GameObject pelvisObject;
    private GameObject torsoObject;

    // Start is called before the first frame update
    void Start()
    {
        character = gameObject;
        anim = gameObject.GetComponent<Animator>();
        collider = gameObject.GetComponent<BoxCollider2D>();
        selected = Part.None;
    }

    // Update is called once per frame
    void Update()
    {
        active = character.GetComponent<HandThrower>().isActive();
        if (Input.GetKeyDown(KeyCode.E) && !selected.Equals(Part.None) && !active && (!character.GetComponent<CharacterController2D>().isFalling() || character.GetComponent<CharacterController2D>().InWater()))
        {
            Drop();
        }
    }

    public bool Floats()
    {
        return feet == 0;
    }

    public bool CanRemove(Part part)
    {
        // if(character.GetComponent<CharacterController2D>().isFalling()){return false;}
    	switch (part)
    	{
    		case Part.Hand:
                return hands > 0;

    		case Part.Foot:
                return feet > 0;

    		case Part.Pelvis:
                return feet == 0 && pelvis;

    		case Part.Torso:
                return !pelvis && (hands == 0) && torso;

    		default:
    			return false;
    	}
    }

    public bool CanAdd(Part part)
    {
    	switch (part)
    	{
    		case Part.Hand:
                return torso && (hands < 2);

    		case Part.Foot:
                return pelvis && (feet < 2);

    		case Part.Pelvis:
    			return torso && !pelvis;

    		case Part.Torso:
    			return !torso;

            case Part.None:
                return hands < 2 || feet < 2;

    		default:
    			return false;
    	}
    }

    public void Drop()
    {
        active = true;
        float dropSide = 1f;
        float dropX = 0f;

        if(character.GetComponent<SpriteRenderer>().flipX){
            dropSide = -1f;
        }
    	switch (selected)
    	{

    		case Part.Hand:
                character.GetComponent<HandThrower>().CallThrow();
                hands -= 1;
                anim.SetInteger("Arms", hands);
                if(hands == 0) selected = Part.None;
                if (!pelvis)
                {
                    if (hands == 1)
                    {
                        collider.offset = new Vector2(0f, 1.75f);
                        collider.size = new Vector2(5.25f, 3.5f);
                    }
                    else if (hands == 0)
                    {
                        collider.offset = new Vector2(0f, 1f);
                        collider.size = new Vector2(5.25f, 2f);
                    }
                }

		    	break;

    		case Part.Foot:
                feet -= 1;
                anim.SetInteger("Feet", feet);
                // Figure out how far to drop the foot
                if(feet > 0){
                    dropX = 3f;
                }
                else{
                    dropX = 0f;
                }

                dropX *= dropSide;

                GameObject dropped = character.GetComponent<PartDropper>().DropPart(Part.Foot, dropX, -.5f, 0, 0);
                if (feet == 1)
                {
                    foot1Object = dropped;
                }
                else if (feet == 0)
                {
                    foot2Object = foot1Object;
                    foot1Object = dropped;
                    selected = Part.None;
                    GetComponent<Rigidbody2D>().mass = 20;
                    collider.offset = new Vector2(-0.2f, 3.25f);
                    collider.size = new Vector2(3.5f, 6.5f);
                    
                }

		    	break;

    		case Part.Pelvis:
    			if (CanRemove(Part.Pelvis)) {
                    dropX = 0f * dropSide;
                    anim.SetBool("Pelvis", false);
                    pelvisObject = character.GetComponent<PartDropper>().DropPart(Part.Pelvis, dropX, 0, 0, 0);
    				pelvis = false;
                    selected = Part.None;
                    if (hands == 2)
                    {
                        collider.offset = new Vector2(-0.2f, 3.25f);
                        collider.size = new Vector2(3.5f, 6.5f);
                    }
                    else
                    {
                        collider.offset = new Vector2(0f, 1.75f);
                        collider.size = new Vector2(5.25f, 3.5f);
                    }
    			}
                break;

    		case Part.Torso:
    			if (CanRemove(Part.Torso)) {
                    dropX = -2f * dropSide;
                    anim.SetBool("Torso", false);
                    StartCoroutine(PopOffTorso(0.7f, dropX));
                    GetComponent<Rigidbody2D>().mass = 5;
    				torso = false;
                    selected = Part.None;
                    collider.offset = new Vector2(0.25f, 1f);
                    collider.size = new Vector2(2.3f, 2f);                  
    			}
                break;
    			
    		default:
    			break;
    	}
        active = false;
        // selected = Part.None;
    }

    public void Add(Part part){
        switch(part){
            case Part.Hand:
                hands += 1;
                Destroy(hand1Object);
                if (hands == 1)
                {
                    hand1Object = hand2Object;
                    if (!pelvis)
                    {
                        collider.offset = new Vector2(0f, 1.75f);
                        collider.size = new Vector2(5.25f, 3.5f);
                    }
                }
                else if (hands == 2 && !pelvis)
                {
                    collider.offset = new Vector2(-0.2f, 3.25f);
                    collider.size = new Vector2(3.5f, 6.5f);
                }
                anim.SetInteger("Arms", hands);
                break;
            case Part.Foot:
                feet += 1;
                Destroy(foot1Object);
                if (feet == 1)
                {
                    foot1Object = foot2Object;
                }
                GetComponent<Rigidbody2D>().mass = 100;
                collider.offset = new Vector2(-0.2f, 5f);
                collider.size = new Vector2(3.5f, 10f);
                anim.SetInteger("Feet", feet);
                break;
            case Part.Pelvis:
                pelvis = true;
                Destroy(pelvisObject);
                anim.SetBool("Pelvis", true);
                collider.offset = new Vector2(-0.2f, 3.25f);
                collider.size = new Vector2(3.5f, 6.5f);
                break;  
            case Part.Torso:
                torso = true;
                Destroy(torsoObject);
                anim.SetBool("Torso", true);
                collider.offset = new Vector2(0f, 1.75f);
                collider.size = new Vector2(5.25f, 3.5f);
                GetComponent<Rigidbody2D>().mass = 20;
                break;
            case Part.None:
                if (!pelvis)
                {
                    Add(Part.Pelvis);
                    // anim.SetBool("Pelvis", true);
                }
                if (!torso)
                {
                    Add(Part.Torso);
                    // anim.SetBool("Torso", true);
                }
                for (int i = hands; i < 2; i++)
                {
                    Add(Part.Hand);
                    // hands += 1;
                }
                for (int i = feet; i < 2; i++)
                {
                    Add(Part.Foot);
                    // feet += 1;
                }
                anim.SetTrigger("ResetBody");


                
                break;

            default:
                break;
          

        }
        StartCoroutine(CreatePoof());
    }

    public void Select(Part part)
    {
        selected = part;
    }

    public bool isActive(){ return active;}

    IEnumerator PopOffTorso(float time, float dropDist){
        yield return new WaitForSeconds(time);

        torsoObject = character.GetComponent<PartDropper>().DropPart(Part.Torso, dropDist, 0, 0, 0);
    }

    IEnumerator CreatePoof(){
        GameObject poofObject = Instantiate(poof, transform.position + poof.GetComponent<PoofObject>().Offset(), transform.rotation);
        Debug.Log("Object created at: " + poofObject.transform);
        yield return new WaitForSeconds(5);
        Destroy(poofObject);
    }

    public void SetHandObject(GameObject handObject)
    {
        if (hands == 1)
        {
            hand1Object = handObject;
        }
        else if (hands == 0)
        {
            hand2Object = hand1Object;
            hand1Object = handObject;
        }
    }

    public int getFeet(){return feet;}
    public int getHands(){return hands;}
    public bool hasPelvis(){return pelvis;}
    public bool hasTorso(){return torso;}
}
