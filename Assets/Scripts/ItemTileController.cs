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
        // Renderer renderer = gameObject.GetComponent<Renderer>();
        // renderer.enabled = false;
    }

    public void SetSuckable(Suckable suckable)
    {
        this.suckable = suckable;
        // Set it to be visible once the suckable is set
        // Renderer renderer = gameObject.GetComponent<Renderer>();
        // renderer.enabled = true;
        // Now tell the bag to drop it in from the top
    }

    public void SetYPosition(float min_y, float max_y)
    {
        // Y position of 1 is the top of the bag

        RectTransform rectTransform = GetComponent<RectTransform>();
        // Change the anchor y min and anchor y max
        // Add a tiny bit of padding
        rectTransform.anchorMin = new Vector2(rectTransform.anchorMin.x, min_y + 0.01f);
        rectTransform.anchorMax = new Vector2(rectTransform.anchorMax.x, max_y - 0.01f);
    }

    // Update is called once per frame
    void Update()
    {

    }
}