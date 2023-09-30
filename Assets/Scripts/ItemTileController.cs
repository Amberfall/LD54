using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTileController : MonoBehaviour
{

    Suckable suckable;

    // Start is called before the first frame update
    void Start()
    {
        // Set the item tile to be invisible
        Renderer renderer = gameObject.GetComponent<Renderer>();
        renderer.enabled = false;
    }

    public void SetSuckable(Suckable suckable)
    {
        this.suckable = suckable;
        // Set it to be visible once the suckable is set
        Renderer renderer = gameObject.GetComponent<Renderer>();
        renderer.enabled = true;
        // Now tell the bag to drop it in from the top
    }

    // Update is called once per frame
    void Update()
    {

    }
}