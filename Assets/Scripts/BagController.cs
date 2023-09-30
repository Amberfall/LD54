using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagController : MonoBehaviour
{

    public int bagSize = 5;

    private List<GameObject> tiles = new List<GameObject>();
    public GameObject itemTilePrefab;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void AddSuckable(Suckable suckable)
    {
        // GameObject tile = Instantiate(itemTilePrefab) as GameObject;
        // Add tile to this, not just anywhere
        GameObject tile = Instantiate(itemTilePrefab, transform) as GameObject;
        tile.transform.SetParent(this.transform);

        tile.GetComponent<ItemTileController>().SetSuckable(suckable);
        tiles.Add(tile);

        reposition_tiles();
    }

    private void reposition_tiles()
    {
        int i = 0;
        for (i = 0; i < tiles.Count; i++)
        {
            // Set the rect transform to be at 0
            tiles[i].GetComponent<ItemTileController>().SetYPosition(
                (float)i / (float)this.bagSize,
                (float)(i + 1) / (float)this.bagSize
            );
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
