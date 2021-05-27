using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
	public Transform player;
	public Vector3 offset;
	private float offsetX;
	private float offsetY;
	private float lookingRight;
	private float lookingLeft;
	private float lookUp;
	private float lookDown;
	private float lookStandard;
	private bool facingRight;
	private bool inLoopLR = false;
	private bool inLoopUD = false;
	private bool lookingUD = false;
	private IEnumerator coroutineLeftRight;
	private IEnumerator coroutineUpDown;
    // Start is called before the first frame update
    void Start()
    {
        
        offsetX = offset.x;
        offsetY = offset.y;
        lookStandard = offset.y;
		lookingRight = offsetX;
        lookingLeft = offsetX * -2;
        facingRight = true;
        lookUp = 15;
        lookDown = -5;
    }

    // Update is called once per frame
    void Update()
    {
    	checkDirection();
    	offset = new Vector3(offsetX, offsetY, offset.z);
        transform.position = new Vector3(player.position.x + offset.x, player.position.y + offset.y, offset.z);
        // Camera follows the player with specified offset position
    }

    private void checkDirection(){

    	// Check if player is looking left and right and so adapt camera as such
    	float movDir = Input.GetAxisRaw("Horizontal");
    	if(movDir > 0 && !facingRight){
    		facingRight = true;
    		if(inLoopLR){
    			StopCoroutine(coroutineLeftRight);
    		}
    		coroutineLeftRight = moveCameraLeftRight(lookingLeft, lookingRight, 0.5f);
    		StartCoroutine(coroutineLeftRight);
    	}
    	else if(movDir < 0 && facingRight){
    		facingRight = false;
    		if(inLoopLR){
    			StopCoroutine(coroutineLeftRight);
    		}
    		coroutineLeftRight = moveCameraLeftRight(lookingRight, lookingLeft, 0.5f);
    		StartCoroutine(coroutineLeftRight);
    	}

    	float lookDir = Input.GetAxisRaw("Vertical");
    	if(!lookingUD){
	    	if(lookDir > 0){
	    		lookingUD = true;
	    		if(inLoopUD){
	    			StopCoroutine(coroutineUpDown);
	    		}
	    		coroutineUpDown = moveCameraUpDown(lookStandard, lookUp, 0.25f);
	    		StartCoroutine(coroutineUpDown);
	    	}
	    	else if(lookDir < 0){
	    		lookingUD = true;
	    		if(inLoopUD){
	    			StopCoroutine(coroutineUpDown);
	    		}
	    		coroutineUpDown = moveCameraUpDown(lookStandard, lookDown, 0.5f);
	    		StartCoroutine(coroutineUpDown);
	    	}
	    }
	    else{
			if(lookDir == 0){
				lookingUD = false;
				if(inLoopUD){
					StopCoroutine(coroutineUpDown);
				}
				coroutineUpDown = moveCameraUpDown(offsetY, lookStandard, 0.25f);
				StartCoroutine(coroutineUpDown);
			}
		}


    }

    private IEnumerator moveCameraLeftRight(float sourcePosition, float targetPosition, float duration){
    	inLoopLR = true;
    	float time = 0;
    	float startPosition = offsetX;
    	// float tempPosition;

    	while(time < duration){
    		offsetX = Mathf.Lerp(startPosition, targetPosition, time/duration);

    		time += Time.deltaTime;
    		yield return null;
    	}
    	offsetX = targetPosition;
    	// swapDirections();
    	inLoopLR = false;
    }

     private IEnumerator moveCameraUpDown(float sourcePosition, float targetPosition, float duration){
    	inLoopUD = true;
    	// Debug.Log("in moveCameraUpDown");
    	float time = 0;
    	float startPosition = offsetY;

    	while(time < duration){
    		offsetY = Mathf.Lerp(startPosition, targetPosition, time/duration);
    		// Debug.Log("offsetY: " + offsetY);

    		time += Time.deltaTime;
    		yield return null;
    	}
    	offsetY = targetPosition;
    	inLoopUD = false;
    }

    
}
