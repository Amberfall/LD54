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
    [SerializeField] private float _timeToShoot = 0.20f;
    private float _time;
    [SerializeField] private float _suckableShootSpeed = 30;

    [SerializeField] private ParticleSystem _ps;
    [SerializeField] private GameObject _dustPuff;


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
            AudioManager.Instance.PlayVacuum();
        }
        else
        {
            _ps.Stop();
            AudioManager.Instance.StopVacuum();
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
            filoSuckable.isDash = false;
            suckables.RemoveAt(suckables.Count - 1);
            filoSuckable.gameObject.SetActive(true);
            filoSuckable.transform.position = _gunTip.position;

            // Check For Power Ups
            int n = CheckForPowerUp(PowerUpType.damage_multiplier);
            filoSuckable.damage = (n > 0 && !filoSuckable.isPowerUp) ? 3 * n * filoSuckable.baseDamage : filoSuckable.baseDamage;

            filoSuckable.Shoot(transform.rotation * Vector2.right * _suckableShootSpeed);

            _time = Time.time;

            AudioManager.Instance.PlaySfx(AudioManager.Sfx.PlayerShoot);
            CameraController.instance.CameraShake(0.2f, 1.5f);

            Instantiate(_dustPuff, _gunTip.position, transform.rotation);
        }
    }
    public bool TryToShootEnemyToDash(Vector3 direction)
    {
        bool canDash = false;
        if (suckables.Count > 0)
        {
            canDash = true;
            Suckable fifoSuckable = suckables[0];
            fifoSuckable.isDash = true;
            suckables.RemoveAt(0);
            fifoSuckable.gameObject.SetActive(true);
            fifoSuckable.transform.position = Player.instance.transform.position - direction;

            // Check For Power Ups
            int n = CheckForPowerUp(PowerUpType.damage_multiplier);
            fifoSuckable.damage = (n > 0 && !fifoSuckable.isPowerUp) ? 3 * n * fifoSuckable.baseDamage : fifoSuckable.baseDamage;

            fifoSuckable.Shoot(-direction * _suckableShootSpeed);
            Instantiate(_dustPuff, _gunTip.position, Quaternion.Euler(0, 0, Vector2.SignedAngle(Vector2.right, -direction)));
        }
        return canDash;
    }

    public bool SuckedRequest(Suckable suckable)
    {
        bool canFitInBag = suckable.size + GetSuckablesTotalSize() < maxBagSpace;
        if (canFitInBag)
        {
            suckable.gameObject.SetActive(false);
            suckables.Add(suckable);
            AudioManager.Instance.PlaySfx(AudioManager.Sfx.VacuumSuck);
            if (suckable.isPowerUp)
            {
                AudioManager.Instance.PlaySfx(AudioManager.Sfx.UpgradeEaten);
            }
            Instantiate(_dustPuff, _gunTip.position, transform.rotation);
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

    public int CheckForPowerUp(PowerUpType powerUpType)
    {
        int number = 0;
        foreach (var suckable in suckables)
        {
            if (suckable.isPowerUp)
            {
                if (suckable.GetComponent<PowerUp>().powerUpType == powerUpType)
                {
                    number++;
                }
            }
        }
        return number;
    }

}
