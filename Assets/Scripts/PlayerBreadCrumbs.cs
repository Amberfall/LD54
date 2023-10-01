using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBreadCrumbs : MonoBehaviour
{
    public static PlayerBreadCrumbs instance;
    [SerializeField] private int _maxSize = 10;
    public List<Vector3> breadCrumbsPositions = new List<Vector3>();
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        breadCrumbsPositions.Add(Player.instance.transform.position);
    }
    public void DropBreadCrumb(Vector3 position)
    {
        if (breadCrumbsPositions.Count == _maxSize)
        {
            breadCrumbsPositions.RemoveAt(0);
        }
        breadCrumbsPositions.Add(position);
    }

    public Vector3 GetClosestBreadCrumb(Vector3 position)
    {
        if (breadCrumbsPositions.Count > 0)
        {
            Vector3 closestPosition = breadCrumbsPositions[0];
            foreach (Vector3 breadCrumbsPosition in breadCrumbsPositions)
            {
                if ((position - closestPosition).sqrMagnitude > (position - breadCrumbsPosition).sqrMagnitude)
                {
                    closestPosition = breadCrumbsPosition;
                }
            }
            return closestPosition;
        }
        else
        {
            return Vector2.zero;
        }
    }
}
