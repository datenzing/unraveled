using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandThrower : MonoBehaviour
{
	public GameObject hand;
    Animator anim;
	[SerializeField] float xOffset;
	[SerializeField] float yOffset;

	private bool active = false;
	private GameObject activeHand;
	private Rigidbody2D characterRB;
	private Collider2D characterCollider;
    private CharacterController2D controller;
    private PartsTracker tracker;
    private bool waterCheck;
    private Vector2[] walkerOffset;
    private Vector2[] rollerTwoOffset;
    private Vector2[] rollerOneOffset;
    private Vector2[] hopperOffset;
    private Vector2[] scorpionOffset;
    private Vector2 offset;

    // Start is called before the first frame update
    void Start()
    {
        characterRB = GetComponent<Rigidbody2D>();
        characterCollider = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
        controller = GetComponent<CharacterController2D>();
        tracker = GetComponent<PartsTracker>();

        walkerOffset = new Vector2[2];
        walkerOffset[0] = new Vector2(2f, 6.5f);
        walkerOffset[1] = new Vector2(-2f, 6.5f);

        rollerTwoOffset = new Vector2[2];
        rollerTwoOffset[0] = new Vector2(-1.5f, 4.5f);
        rollerTwoOffset[1] = new Vector2(1f, 4f);

        rollerOneOffset = new Vector2[2];
        rollerOneOffset[0] = new Vector2(1f, 4f);
        rollerOneOffset[1] = new Vector2(-1f, 4f);

        hopperOffset = new Vector2[2];
        hopperOffset[0] = new Vector2(-1.5f, 4.5f);
        hopperOffset[1] = new Vector2(1f, 4.5f);

        scorpionOffset = new Vector2[2];
        scorpionOffset[0] = new Vector2(0.75f, 0.75f);
        scorpionOffset[1] = new Vector2(-0.5f, 1f);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CallThrow()
    {
        if (active)
        {
            CancelThrow();
        }
        else
        {
            DeactivateCharacter();
            CreateHand();
        }
    }

    public void DeactivateCharacter()
    {
        waterCheck = controller.InWater();
    	active = true;
        controller.SetMovement(false);
        characterRB.bodyType = RigidbodyType2D.Static;
        characterCollider.enabled = false;
    }

    public void CancelThrow()
    {
    	Destroy(activeHand);
    	ReactivateCharacter();
    }

    public void ReactivateCharacter()
    {
    	characterCollider.enabled = true;
    	characterRB.bodyType = RigidbodyType2D.Dynamic;
        gameObject.GetComponent<PartsTracker>().SetHandObject(activeHand);
    	active = false;
        controller.SetMovement(true);

    }

    void CreateHand()
    {
        offset = GetOffset();
    	activeHand = Instantiate(hand, characterRB.position + offset, transform.rotation);
        activeHand.GetComponent<Hand>().AssignHand(gameObject);
    	SpringJoint2D sj = hand.GetComponent<SpringJoint2D>();
        sj.connectedBody = characterRB;
        anim.SetTrigger("InitiateAction");
    }

    public bool isActive(){ return active;}

    public Vector2 GetOffset()
    {
        int offsetSide;
        if (controller.IsFacingRight())
        {
            offsetSide = 1;
        }
        else
        {
            offsetSide = 0;
        }

        if (tracker.getFeet() > 0)
        {
            return walkerOffset[offsetSide];
        }
        else if (tracker.hasPelvis())
        {
            if (tracker.getHands() == 2)
            {
                return rollerTwoOffset[offsetSide];
            }
            else if (tracker.getHands() == 1)
            {
                return rollerOneOffset[offsetSide];
            }
        }
        else if (tracker.getHands() == 2)
        {
            return hopperOffset[offsetSide];
        }
        else if (tracker.getHands() == 1)
        {
            return scorpionOffset[offsetSide];
        }
        return Vector2.zero;
    }
}
