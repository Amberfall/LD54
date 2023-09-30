using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagView : MonoBehaviour
{

    private List<GameObject> tiles = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {

    }

    public void AddSuckable(Suckable suckable)
    {
        GameObject tile = Instantiate(Resources.Load("Prefabs/ItemTile") as GameObject, transform);
        tile.GetComponent<ItemTileController>().SetSuckable(suckable);
        tiles.Add(tile);

        reposition_tiles();
    }

    private void reposition_tiles()
    {
        int i = 0;
        foreach (GameObject tile in tiles)
        {
            tile.transform.localPosition = new Vector3(0, -i * 100, 0);
            i++;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
