using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SuckEvent : UnityEvent<bool> { }
public class Gun : MonoBehaviour
{
    public static Gun instance;
    public static SuckEvent suckEvent = new SuckEvent();
    public static UnityEvent shootEvent = new UnityEvent();
    private bool _isSucking;
    private BoxCollider2D _bc;
    [SerializeField] private LayerMask _whatIsSuckable;
    [SerializeField] private float _suckingSpeed;
    [SerializeField] private Transform _gunCanon;



    public int maxBagSpace = 10;
    public int bagSpaceOccupied;
    public List<Suckable> suckables = new List<Suckable>();

    private void Awake()
    {
        instance = this;
        _bc = GetComponent<BoxCollider2D>();
        suckEvent.AddListener(OnSuckEvent);
        shootEvent.AddListener(OnShoot);
    }

    void Start()
    {
        bagSpaceOccupied = 0;
    }

    void Update()
    {


    }

    private void OnSuckEvent(bool isSucking)
    {
        _bc.enabled = isSucking;
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(40, 40, 100, 100), suckables.Count.ToString() + "/" + maxBagSpace.ToString());
    }

    public void OnShoot()
    {
        if (suckables.Count > 0)
        {
            suckables[suckables.Count - 1].gameObject.SetActive(true);
            suckables[suckables.Count - 1].transform.position = _gunCanon.position;
            suckables[suckables.Count - 1].Shoot((transform.rotation * Vector2.right) * 10);
            suckables.RemoveAt(suckables.Count - 1);
        }
    }

}
