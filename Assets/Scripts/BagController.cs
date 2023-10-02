using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagController : MonoBehaviour
{

    Gun gun_instance;
    private List<GameObject> tiles = new List<GameObject>();
    private List<GameObject> tiles_heading_out = new List<GameObject>();
    public GameObject itemTilePrefab;

    public float tile_x_pos = 0.15f;
    public float tile_x_width = 0.1f;

    int suckables_size = 0;

    private Suckable bottom_suck = null;

    // Start is called before the first frame update
    void Start()
    {
        gun_instance = Gun.instance;
    }

    public void AddSuckable(Suckable suckable)
    {
        // GameObject tile = Instantiate(itemTilePrefab) as GameObject;
        // Add tile to this, not just anywhere
        GameObject tile = Instantiate(itemTilePrefab, transform) as GameObject;
        tile.transform.SetParent(this.transform);
        tile.GetComponent<ItemTileController>().SetSprite(suckable.suckableType);
        if (suckable.isPowerUp)
        {
            var type = suckable.GetComponent<PowerUp>().powerUpType;
            switch (type)
            {
                case PowerUpType.damage_multiplier:
                    tile.GetComponent<ItemTileController>().SetText("+50% DMG");
                    break;
                case PowerUpType.defensive:
                    tile.GetComponent<ItemTileController>().SetText("+20% DEF");
                    break;
                case PowerUpType.movement:
                    tile.GetComponent<ItemTileController>().SetText("+50% MVT");
                    break;
            }
        }
        else
        {
            tile.GetComponent<ItemTileController>().SetText(suckable.baseDamage.ToString() + " DMG");
        }


        float top_pos = 1.0f;

        for (int i = 0; i < tiles.Count; i++)
        {
            float y_size = tiles[i].GetComponent<ItemTileController>().get_y_size(gun_instance.maxBagSpace);
            float tile_top = tiles[i].GetComponent<ItemTileController>().state.y_pos + y_size;
            if (tile_top > top_pos)
                top_pos = tile_top;
        }

        tile.GetComponent<ItemTileController>().Setup(suckable, top_pos);

        tiles.Add(tile);

        PositionTiles();
    }

    public void Eject(int index, bool up)
    {
        if (index == -1)
            index = tiles.Count - 1;
        if (tiles.Count == 0)
            return;

        GameObject tile = tiles[index];
        tiles.RemoveAt(index);

        tiles_heading_out.Add(tile);
        tile.GetComponent<ItemTileController>().Eject(up);

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

            // tile_controller.state.y_pos = Mathf.Max(bottom_pos, last_top_pos);

            // bool falling = bottom_pos > last_top_pos || last_falling;

            tile_controller.update_state(last_top_pos);

            tile_controller.update_position(gun_instance.maxBagSpace, tile_x_pos, tile_x_width);

            last_top_pos = tile_controller.get_top_pos(gun_instance.maxBagSpace); // bottom_pos + tile_controller.get_y_size();
            // tile_controller.was_falling = falling;
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (gun_instance.suckables.Count > suckables_size)
        {
            for (int i = suckables_size; i < gun_instance.suckables.Count; i++)
            {
                AddSuckable(gun_instance.suckables[i]);
            }

            bottom_suck = gun_instance.suckables[0];
        }

        if (gun_instance.suckables.Count < suckables_size)
        {
            if (gun_instance.suckables.Count == 0 || bottom_suck == gun_instance.suckables[0])
            {
                Eject(-1, true);
            }
            else
            {
                Eject(0, false);
                if (gun_instance.suckables.Count > 0)
                    bottom_suck = gun_instance.suckables[0];
                else
                    bottom_suck = null;
            }
        }

        suckables_size = gun_instance.suckables.Count;

        // Now do positioning stuff
        PositionTiles();

        for (int i = tiles_heading_out.Count - 1; i >= 0; i--)
        {
            ItemTileController tile_controller = tiles_heading_out[i].GetComponent<ItemTileController>();
            tiles_heading_out[i].GetComponent<ItemTileController>().update_state(0.0f);

            tile_controller.update_position(gun_instance.maxBagSpace, tile_x_pos, tile_x_width);
            if (tile_controller.cleanup)
            {
                Destroy(tiles_heading_out[i]);
                tiles_heading_out.RemoveAt(i);
            }
        }
    }
}
