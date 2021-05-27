using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconManager : MonoBehaviour
{
	private bool displayIcon = false;
	private SpriteRenderer collectIcon;
	[SerializeField] Sprite qIcon;
	[SerializeField] Sprite qCrossIcon;
    // Start is called before the first frame update
    void Start()
    {
        collectIcon = gameObject.GetComponent<SpriteRenderer>();
        collectIcon.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        collectIcon.enabled = displayIcon;
    }

    public void IconToggle(bool enabled){
    	displayIcon = enabled;
    }

    public void CanInteract(bool enabled){
        if(enabled){
            collectIcon.sprite = qIcon;
        }
        else collectIcon.sprite = qCrossIcon;
    }
}
