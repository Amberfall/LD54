using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagController : MonoBehaviour
{

    public int bagSize = 15;

    private List<GameObject> tiles = new List<GameObject>();
    public GameObject itemTilePrefab;

    public float tile_x_pos = 0.3f;
    public float tile_x_width = 0.4f;

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
            float y_size = tiles[i].GetComponent<ItemTileController>().get_y_size(this.bagSize);
            float tile_top = tiles[i].GetComponent<ItemTileController>().y_pos + y_size;
            if (tile_top > top_pos)
                top_pos = tile_top;
        }

        tile.GetComponent<ItemTileController>().Setup(suckable, top_pos);

        tiles.Add(tile);

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

            float bottom_pos = tile_controller.y_pos;
            if (bottom_pos < last_top_pos)
            {
                // Set the rect transform to be at 0
                tile_controller.y_pos = last_top_pos;
                bottom_pos = last_top_pos;
            }

            bool falling = bottom_pos > last_top_pos || last_falling;

            if (falling)
            {
                tile_controller.y_pos = bottom_pos - 0.01f;
                if (tile_controller.y_pos < last_top_pos)
                    tile_controller.y_pos = last_top_pos;
            }

            tile_controller.update_state(falling);

            last_top_pos = bottom_pos + tile_controller.get_y_size(this.bagSize);
            tile_controller.update_position(this.bagSize, tile_x_pos, tile_x_width);
            // tile_controller.was_falling = falling;
        }
    }

    // Update is called once per frame
    void Update()
    {
        PositionTiles();
    }
}
