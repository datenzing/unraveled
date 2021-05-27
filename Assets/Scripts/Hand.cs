using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
	private Rigidbody2D rb;
	private SpringJoint2D sj;
	private Vector2 jointPosition;
	private LineRenderer lr;
    private GameObject character;
    private Animator anim;

	private float releaseDelay;
    [SerializeField] float unlockDelay = 1f;
	[SerializeField] float maxDragDistance = 1f;
	private bool isPressed;
    private bool thrown;
    private CircleCollider2D handCollider;
    private Vector2 rightOffset;
    private Vector2 leftOffset;
    private Vector2 offset;

	void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
		sj = GetComponent<SpringJoint2D>();
		lr = GetComponent<LineRenderer>();
        handCollider = gameObject.GetComponent<CircleCollider2D>();
	}

    void Update()
    {
        if (isPressed)
        {
        	DragBall();
        }
    }

    public void AssignHand(GameObject character)
    {
        offset = character.GetComponent<HandThrower>().GetOffset();
		lr.enabled = false;
		sj.connectedBody = character.GetComponent<Rigidbody2D>();
        sj.connectedAnchor = offset;
        jointPosition = sj.connectedBody.position + sj.connectedAnchor;
		releaseDelay = 1 / (sj.frequency * 4);
        this.character = character;
        thrown = false;
        anim = character.GetComponent<Animator>();
        
        SetLineRendererPositions();
    }

    private void DragBall()
    {
     	SetLineRendererPositions();
       	Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
       	float distance = Vector2.Distance(mousePosition, jointPosition);
        	if (distance > maxDragDistance) {
       		Vector2 direction = (mousePosition - jointPosition).normalized;
       		rb.position = jointPosition + direction * maxDragDistance;
       	}
       	else
       	{
       		rb.position = mousePosition;
       	}
        bool rightSide = rb.position.x > jointPosition.x;
        Vector2 relativePosition = rb.position - jointPosition;
        float tan = relativePosition.y / relativePosition.x;
        int location;
        if (tan > .577f)
        {
            if (rightSide) {
                location = 45;
            } else {
                location = -45;
            }
        }
        else if (tan < -.577f)
        {
            if (rightSide) {
                location = -45;
            } else {
                location = 45;
            }
        }
        else
        {
            if (rightSide) {
                location = 0;
            } else {
                location = 0;
            }
        }
    }

    private void SetLineRendererPositions()
    {
    	Vector3[] positions = new Vector3[2];
    	positions[0] = rb.position;
    	positions[1] = jointPosition;
    	lr.SetPositions(positions);
    }

    private void OnMouseDown()
    {
        if (!thrown)
        {
    	   isPressed = true;
    	   rb.isKinematic = true;
    	   lr.enabled = true;
        }
    }

    private void OnMouseUp()
    {
    	isPressed = false;
    	rb.isKinematic = false;
        anim.SetTrigger("HandThrown");
        Physics2D.IgnoreCollision(handCollider, character.GetComponent<Collider2D>(), true);
    	StartCoroutine(Release());
    }

    private IEnumerator Release()
    {
    	yield return new WaitForSeconds(releaseDelay);
    	sj.enabled = false;
    	lr.enabled = false;
        thrown = true;
        StartCoroutine(Unlock());
    }

    private IEnumerator Unlock()
    {
        yield return new WaitForSeconds(unlockDelay);
        Physics2D.IgnoreCollision(handCollider, character.GetComponent<Collider2D>(), false);
        character.GetComponent<HandThrower>().ReactivateCharacter();
        gameObject.layer = 8;
    }
}
