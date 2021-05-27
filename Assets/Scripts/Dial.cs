using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dial : MonoBehaviour
{
	public GameObject character;
    public GameObject top;
    public GameObject left;
    public GameObject bottom;
    public GameObject right;
    public GameObject cross;

	private PartsTracker tracker;
    private bool activatable;

    // Start is called before the first frame update
    void Start()
    {
        tracker = character.GetComponent<PartsTracker>();
        activatable = true;

        top.SetActive(false);
        left.SetActive(false);
        bottom.SetActive(false);
        right.SetActive(false);
        cross.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        activatable = !tracker.isActive();
        if (Input.GetKeyDown(KeyCode.LeftShift) && activatable)
        {
            top.SetActive(true);
            left.SetActive(true);
            bottom.SetActive(true);
            right.SetActive(true);
            cross.SetActive(true);
        }
        
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            top.SetActive(false);
            left.SetActive(false);
            bottom.SetActive(false);
            right.SetActive(false);
            cross.SetActive(false);
        }
    }

    public void Clicked()
    {
        top.SetActive(false);
        left.SetActive(false);
        bottom.SetActive(false);
        right.SetActive(false);
        cross.SetActive(false);
    }

    public void NotReady()
    {
        activatable = false;
    }

    public void Ready()
    {
        activatable = true;
    }
}
