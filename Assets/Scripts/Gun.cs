using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerSuckingEvent : UnityEvent<bool> { }
public class Gun : MonoBehaviour
{
    public static Gun instance;
    public static TriggerSuckingEvent trigerSuckingEvent = new TriggerSuckingEvent();
    public static UnityEvent shootEvent = new UnityEvent();
    [SerializeField] private BoxCollider2D _suckingCollider;
    [SerializeField] private Transform _gunTip;
    [SerializeField] private float _timeToShoot = 0.25f;
    private float _time;

    [SerializeField] private ParticleSystem _ps;


    [Header("Bag Stuff")]
    public int maxBagSpace = 10;
    public List<Suckable> suckables = new List<Suckable>();

    private void Awake()
    {
        instance = this;
        _suckingCollider = GetComponent<BoxCollider2D>();
        trigerSuckingEvent.AddListener(OnSuckEvent);
        shootEvent.AddListener(OnShoot);
        _time = Time.time;
    }

    private void OnSuckEvent(bool isSucking)
    {
        _suckingCollider.enabled = isSucking;
        if (isSucking)
        {
            _ps.Play();
        }
        else
        {
            _ps.Stop();
        }
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(40, 40, 100, 100), suckables.Count.ToString() + "/" + maxBagSpace.ToString());
    }

    public void OnShoot()
    {
        if (suckables.Count > 0 && Time.time - _time > _timeToShoot)
        {
            Suckable filoSuckable = suckables[suckables.Count - 1];
            filoSuckable.gameObject.SetActive(true);
            filoSuckable.transform.position = _gunTip.position;
            filoSuckable.Shoot(transform.rotation * Vector2.right * 15);
            suckables.RemoveAt(suckables.Count - 1);
            _time = Time.time;
        }
    }

    public bool SuckedRequest(Suckable suckable)
    {
        bool canFitInBag = suckable.size + GetSuckablesTotalSize() < maxBagSpace;
        if (canFitInBag)
        {
            suckable.gameObject.SetActive(false);
            suckables.Add(suckable);
        }
        return canFitInBag;
    }

    public Vector2 GetGunTipPoisition()
    {
        return _gunTip.position;
    }

    public int GetSuckablesTotalSize()
    {
        int size = 0;
        foreach (Suckable suckable in suckables)
        {
            size += suckable.size;
        }
        return size;
    }
}
