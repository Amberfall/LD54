using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RopeNode
{
    public Vector2 positionCurrent;
    public Vector2 positionPrevious;
    public RopeNode(Vector2 position)
    {
        positionCurrent = position;
        positionPrevious = position;
    }
}

[RequireComponent(typeof(LineRenderer))]
public class Rope : MonoBehaviour
{
    private LineRenderer _lr;
    private List<RopeNode> _ropeNodes = new List<RopeNode>();
    [Header("Rope Settings")]
    [SerializeField] private Transform _startPoint;
    [SerializeField] private Transform _endPoint;
    [SerializeField] private float _ropeLength;
    [SerializeField] private float _segmentLength;
    private int _nodeNumber;
    [SerializeField] private float _lineWidth;
    [SerializeField] private float _gravityScale;
    const float gravityValue = 9.81f;
    private Vector2 _gravityAcceleration;
    [SerializeField] private float _friction;
    [Header("Simulation Settings")]
    [SerializeField] private int _constraintSubSteps;


    const float globalYOffset = 24f;
    const float ppu = 16f;
    [SerializeField] private Transform _bottomTransform;



    private void Awake()
    {
        _lr = GetComponent<LineRenderer>();
    }

    void Start()
    {
        _nodeNumber = Mathf.FloorToInt(_ropeLength / _segmentLength);
        _lr.startWidth = _lineWidth;
        _lr.endWidth = _lineWidth;
        _lr.positionCount = _nodeNumber;
        /* Collider stuff */



        /* Initializing the rope nodes */
        _gravityAcceleration = Vector2.down * gravityValue * _gravityScale;
        if (_endPoint == null)
        {
            Vector3 ropeStartPoint = _startPoint.position;
            for (int i = 0; i < _nodeNumber; i++)
            {
                _ropeNodes.Add(new RopeNode(ropeStartPoint));
                ropeStartPoint.y -= _segmentLength;
            }
        }
        else
        {
            Vector3 direction = _endPoint.position - _startPoint.position;
            if (direction.magnitude < _ropeLength)
            {
                Vector3 midPoint = new Vector3((_startPoint.position.x + _endPoint.position.x), (_startPoint.position.y + _endPoint.position.y) - Mathf.Sqrt((direction.x * direction.x + _ropeLength * _ropeLength))) * 0.5f;
                for (int i = 0; i < _nodeNumber; i++)
                {
                    if (i < _nodeNumber / 2)
                    {
                        _ropeNodes.Add(new RopeNode(_startPoint.position + (midPoint - _startPoint.position).normalized * _segmentLength * i));
                    }
                    else
                    {
                        _ropeNodes.Add(new RopeNode(midPoint + (_endPoint.position - midPoint).normalized * _segmentLength * i));
                    }
                }
            }
            else
            {
                float newSegmentLength = direction.magnitude / _nodeNumber;
                Vector3 nDirection = direction.normalized;
                for (int i = 0; i < _nodeNumber; i++)
                {
                    _ropeNodes.Add(new RopeNode(_startPoint.position + nDirection * newSegmentLength * i));
                }
            }
        }
    }

    void Update()
    {
        DrawRope();
        _lr.sortingOrder = (int)(ppu * (globalYOffset - _bottomTransform.position.y)) + 1;
    }
    private void FixedUpdate()
    {
        SimulateRope(Time.fixedDeltaTime);
    }

    private void DrawRope()
    {
        for (int i = 0; i < _nodeNumber; i++)
        {
            _lr.SetPosition(i, _ropeNodes[i].positionCurrent);
        }
    }

    private void SimulateRope(float dt)
    {
        for (int i = 0; i < _nodeNumber; i++)
        {
            Vector2 currentPos = _ropeNodes[i].positionCurrent;
            float wv = 0;
            Vector2 velocity = _ropeNodes[i].positionCurrent - _ropeNodes[i].positionPrevious + Vector2.right * wv;
            _ropeNodes[i].positionCurrent = 2 * _ropeNodes[i].positionCurrent - _ropeNodes[i].positionPrevious + (_gravityAcceleration - velocity * _friction) * dt * dt;
            _ropeNodes[i].positionPrevious = currentPos;
        }
        for (int i = 0; i < _constraintSubSteps; i++)
        {
            ApplyConstraint();
        }
    }

    private void ApplyConstraint()
    {
        for (int i = 0; i < _nodeNumber - 1; i++)
        {
            Vector2 direction = _ropeNodes[i].positionCurrent - _ropeNodes[i + 1].positionCurrent;
            float error = direction.magnitude - _segmentLength;

            _ropeNodes[i].positionCurrent -= direction.normalized * error * 0.5f;
            _ropeNodes[i + 1].positionCurrent += direction.normalized * error * 0.5f;
        }
        if (_startPoint != null)
        {
            _ropeNodes[0].positionCurrent = _startPoint.position;
        }
        if (_endPoint != null)
        {
            _ropeNodes[_nodeNumber - 1].positionCurrent = _endPoint.position;
        }
    }
}
