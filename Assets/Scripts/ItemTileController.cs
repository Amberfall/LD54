using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTileController : MonoBehaviour
{

    public abstract class State
    {
        public abstract State step(bool falling);
        public abstract float Width();
        public abstract float Height();
    }

    class Squishing : State
    {
        public float max_squish = 0.2f;
        public float squishedness = 0.0f;

        public override State step(bool falling)
        {
            squishedness += 0.005f;
            if (squishedness >= 1.0f)
            {
                return new Settled();
            }

            return this;
        }

        public override float Width()
        {
            float to_be_squared = (squishedness - 1);
            float sin = Mathf.Sin(to_be_squared * to_be_squared * Mathf.PI);
            return 1.0f + 0.1f * sin * sin;
        }

        public override float Height()
        {
            float to_be_squared = (squishedness - 1);
            float cos = Mathf.Cos(to_be_squared * to_be_squared * Mathf.PI);
            return 0.8f + 0.2f * cos * cos;
        }
    }

    class Settled : State
    {
        public override State step(bool falling)
        {
            if (falling)
            {
                return new Squishing();
            }
            return this;
        }

        public override float Width()
        {
            return 1.0f;
        }

        public override float Height()
        {
            return 1.0f;
        }
    }

    State state = new Settled();

    public float y_pos = 0.0f;

    Suckable suckable = null;

    // Start is called before the first frame update
    void Start()
    {
        // Set the item tile to be invisible
        // Renderer renderer = gameObject.GetComponent<Renderer>();
        // renderer.enabled = false;
    }

    float get_nominal_y_size(int bag_size)
    {
        return (float)this.suckable.size / (float)bag_size;
    }

    public void Setup(Suckable suckable, float y_pos)
    {
        this.suckable = suckable;
        this.y_pos = y_pos;
    }

    public void Squish()
    {
        this.state = new Squishing();
    }

    public float get_y_factor()
    {
        return this.state.Height();
    }

    public float get_y_size(int bag_size)
    {
        return get_nominal_y_size(bag_size) * this.state.Height();
    }

    public void SetPosition(float y_min, float nominal_y_size)
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        // Change the anchor y min and anchor y max
        // Add a tiny bit of padding
        float y_size = nominal_y_size * get_y_factor();
    }

    public void update_state(bool falling)
    {
        this.state = this.state.step(falling);
    }

    public void update_position(int bagSize, float x_center, float nominal_x_width)
    {

        RectTransform rectTransform = GetComponent<RectTransform>();

        float y_size = this.get_y_size(bagSize);

        float x_width = nominal_x_width * this.state.Width();
        float x_min = x_center - x_width / 2.0f;
        float x_max = x_center + x_width / 2.0f;

        rectTransform.anchorMin = new Vector2(x_min, y_pos + 0.01f);
        rectTransform.anchorMax = new Vector2(x_max, y_pos + y_size - 0.01f);
    }

    // Update is called once per frame
    void Update()
    {
    }
}