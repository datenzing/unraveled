using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
	public CharacterController2D controller;

	public float runSpeed = 40f;
    private Animator anim;
    private PartsTracker tracker;

	float horizontalMove = 0f;
	bool jump = false;

    void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 100, 30), "Reset Scene"))
        {
            SceneManager.LoadScene("GameScene");
        }
    }

    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        tracker = gameObject.GetComponent<PartsTracker>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        if (Input.GetButtonDown("Jump") && tracker.getFeet() > 0)
        {
        	jump = true;
            anim.SetTrigger("Jump");
        }
    }

    void FixedUpdate()
    {
    	controller.Move(horizontalMove * Time.fixedDeltaTime, jump);
    	jump = false;
        anim.SetFloat("MoveSpeed", Mathf.Abs(horizontalMove));
    }

    public bool isJumping(){return jump;}
}
