using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialPart : MonoBehaviour
{
    public Part part;
    public GameObject character;

    private PartsTracker tracker;
    private bool clickable;
    private Dial dial;

    // Start is called before the first frame update
    void Start()
    {
        tracker = character.GetComponent<PartsTracker>();
        dial = transform.parent.GetComponent<Dial>();
        clickable = tracker.CanRemove(part);
        if (clickable)
        {
            GetComponent<SpriteRenderer>().color = Color.white;
            Debug.Log("Clickable");
        }
        else
        {
            GetComponent<SpriteRenderer>().color = Color.grey;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnEnable()
    {
        if(part != null){
            clickable = tracker.CanRemove(part);
            if (clickable)
            {
                GetComponent<SpriteRenderer>().color = Color.white;
            }
            else
            {
                GetComponent<SpriteRenderer>().color = Color.grey;
            }
        }
    }

    void OnDisable()
    {
        this.transform.localScale = new Vector3(1f, 1f, 0);
    }

    void OnMouseEnter()
    {
        if (clickable)
        {
            this.transform.localScale = new Vector3(1.5f, 1.5f, 0);
        }
    }

    void OnMouseDown()
    {
        if (clickable)
        {
            tracker.Select(part);
            dial.Clicked();
        }
    }

    void OnMouseExit()
    {
        if (clickable)
        {
            this.transform.localScale = new Vector3(1f, 1f, 0);
        }
    }
}
