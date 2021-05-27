using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chindrip : MonoBehaviour
{
    [SerializeField] int maxFruit = 3;
    private int numFruits;

    [SerializeField] bool exhaustible = true;
    [SerializeField] Sprite[] sprites;
    [SerializeField] Part part;
    [SerializeField] GameObject character;

    private SpriteRenderer spriteRenderer;
    // private PartsTracker tracker;
    private IconManager iconManager;

    void Start()
    {
        // tracker = character.GetComponent<PartsTracker>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        numFruits = maxFruit;
        int index;
        if(numFruits >= 3){index = 3;}
        else index = numFruits;
        spriteRenderer.sprite = sprites[index];
    }

    public bool CanEat()
    {
        return numFruits > 0;
    }

    public void Eat()
    {
        // tracker.Add(part);
        if (exhaustible)
        {
            int index;
            numFruits--;
            if(numFruits >= 3){ index = 3;}
            else index = numFruits;
            spriteRenderer.sprite = sprites[index];
        }
    }

    public void Reset()
    {
        numFruits = maxFruit;
    }

    public Part getPart(){
        return part;
    }

    // public int getFruitNum(){return numFruits;}
}
