using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagController : MonoBehaviour
{

    // public static Gun gun_instance;
    private List<GameObject> tiles = new List<GameObject>();
    private List<GameObject> tiles_heading_out = new List<GameObject>();
    public GameObject itemTilePrefab;

    public float tile_x_pos = 0.15f;
    public float tile_x_width = 0.1f;

    int suckables_size = 0;

    int maxBagSpace = 10;

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

        float top_pos = 1.0f;

        for (int i = 0; i < tiles.Count; i++)
        {
            float y_size = tiles[i].GetComponent<ItemTileController>().get_y_size(maxBagSpace);
            float tile_top = tiles[i].GetComponent<ItemTileController>().state.y_pos + y_size;
            if (tile_top > top_pos)
                top_pos = tile_top;
        }

        tile.GetComponent<ItemTileController>().Setup(suckable, top_pos);

        tiles.Add(tile);

        PositionTiles();
    }

    public void Eject()
    {
        if (tiles.Count == 0)
            return;

        GameObject tile = tiles[tiles.Count - 1];
        tiles.RemoveAt(tiles.Count - 1);

        tiles_heading_out.Add(tile);
        tile.GetComponent<ItemTileController>().Eject();

        PositionTiles();
    }

    private void PositionTiles()
    {
        if (tiles.Count == 0)
            return;

        float last_top_pos = 0.0f;
        bool last_falling = false;

        for (int i = 0; i < tiles.Count; i++)
        {
            ItemTileController tile_controller = tiles[i].GetComponent<ItemTileController>();

            float bottom_pos = tile_controller.state.y_pos;
            if (bottom_pos < last_top_pos)
            {
                // Set the rect transform to be at 0
                tile_controller.state.y_pos = last_top_pos;
                bottom_pos = last_top_pos;
            }

            bool falling = bottom_pos > last_top_pos || last_falling;

            // if (falling)
            // {
            //     tile_controller.state.y_pos = bottom_pos - 0.01f;
            //     if (tile_controller.state.y_pos < last_top_pos)
            //         tile_controller.state.y_pos = last_top_pos;
            // }

            tile_controller.update_state(last_top_pos);

            last_top_pos = bottom_pos + tile_controller.get_y_size(maxBagSpace);
            tile_controller.update_position(maxBagSpace, tile_x_pos, tile_x_width);
            // tile_controller.was_falling = falling;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // if (gun_instance.suckables.Count > suckables_size)
        // {
        //     for (int i = suckables_size; i < gun_instance.suckables.Count; i++)
        //     {
        //         AddSuckable(gun_instance.suckables[i]);
        //     }
        // }

        // if (gun_instance.suckables.Count < suckables_size)
        // {
        //     for (int i = suckables_size - 1; i >= gun_instance.suckables.Count; i--)
        //     {
        //         tiles[i].GetComponent<ItemTileController>().Squish();
        //     }
        // }

        // suckables_size = gun_instance.suckables.Count;
        PositionTiles();

        for (int i = tiles_heading_out.Count - 1; i >= 0; i--)
        {
            ItemTileController tile_controller = tiles_heading_out[i].GetComponent<ItemTileController>();
            tiles_heading_out[i].GetComponent<ItemTileController>().update_state(0.0f);

            tile_controller.update_position(maxBagSpace, tile_x_pos, tile_x_width);
            if (tile_controller.cleanup)
            {
                Destroy(tiles_heading_out[i]);
                tiles_heading_out.RemoveAt(i);
            }
        }
    }
}
