using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
	[SerializeField] float duration = 5;
	[SerializeField] GameObject loc1;
	[SerializeField] GameObject loc2;
	private bool inLoop;
	private Vector3 sourcePosition;
	private Vector3 targetPosition;
	
	// private bool moving = true;
    
    void Start(){    	
    	targetPosition = loc1.transform.position;
    	sourcePosition = loc2.transform.position;
    	transform.position = sourcePosition;
    	// MovePlatform(vec1, vec2);
    	StartCoroutine(LerpPosition(sourcePosition, targetPosition, duration));
    	// Debug.Log("After Coroutine");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
    	if(!inLoop){
    		StartCoroutine(LerpPosition(sourcePosition, targetPosition, duration));
    	}

        // if(moving){
        // 	transform.position += (velocity * Time.deltaTime);
        // }
    }

    private void OnCollisionEnter2D(Collision2D collision){
    	if (collision.collider.tag == "Player" || collision.collider.tag == "BodyPart"){
    		// moving = true;
    		collision.collider.transform.SetParent(transform);
    	}
    }

    private void OnCollisionExit2D(Collision2D collision){
    	if(collision.collider.tag == "Player" || collision.collider.tag == "BodyPart"){
    		collision.collider.transform.SetParent(null);
    	}
    }

    private void swapDirections(){
    	Vector3 temp = targetPosition;
    	targetPosition = sourcePosition;
    	sourcePosition = temp;
    }

    //interpolate the platform from the sourcePosition to the targetPosition in time duration
    IEnumerator LerpPosition(Vector3 sourcePosition, Vector3 targetPosition, float duration){
    	inLoop = true;
    	float time = 0;
    	Vector3 startPosition = transform.position;

    	while(time < duration){
    		transform.position = Vector3.Lerp(startPosition, targetPosition, time/duration);
    		time += Time.deltaTime;
    		yield return null;
    	}
    	transform.position = targetPosition;
    	swapDirections();
    	inLoop = false;

    	// StartCoroutine(LerpPosition(targetPosition, sourcePosition, duration));
    }

    // private void MovePlatform(Vector3 vec1, Vector3 vec2){
    // 	Vector3 startPosition = vec1;
    // 	Vector3 endPosition = vec2;
    // 	Vector3 temp;
    // 	while(true){
    // 		LerpPosition(endPosition, duration);
    // 		temp = endPosition;
    // 		endPosition = startPosition;
    // 		startPosition = temp;

    // 	}
    // }

    // private void LerpPosition(Vector3 targetPosition, float duration){
    // 	float time = 0;
    // 	Vector3 startPosition = transform.position;

    // 	while(time < duration){
    // 		transform.position = Vector3.Lerp(startPosition, targetPosition, time/duration);
    // 		time += Time.deltaTime;
    // 		yield return null;
    // 	}
    // 	transform.position = targetPosition;
    // }
}
