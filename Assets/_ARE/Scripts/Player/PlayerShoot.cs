using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [Header("Sound")]
    [SerializeField] private AudioClip _shootingSoundClip;

    [Header("Cameras")]
    [SerializeField] private CinemachineVirtualCamera _combatCamera;
    [SerializeField] private CinemachineVirtualCamera _freeLook;

    [Header("Bullet Variables")]
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float bulletColdown;
    [SerializeField] private int ammo;

    [Header("Initial Setup")]
    [SerializeField] private Transform bulletSpawnTransform, attackPoint;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject _weapon;


    [Header("Reticle")]
    [SerializeField] private GameObject _reticle;

    PlayerActionsInput _playerActionsInput;
    bool canShoot = true;

    private void Awake()
    {
        _playerActionsInput = GetComponent<PlayerActionsInput>();
    }

    private void Update()
    {
        if (_playerActionsInput.AttackPressed &&
            _playerActionsInput.AimingPressed &&
            canShoot &&
            ammo > 0) Shoot();

        UpdateCamera(_playerActionsInput.AimingPressed);
    }

    private void Shoot()
    {
        canShoot = false;
        _playerActionsInput.AttackPressed = false;

        SoundFXManager.instance.PlaySoundFXClip(_shootingSoundClip, transform, 1f);

        Vector3 forceDirection;
        RaycastHit hit;

        if (Physics.Raycast(_combatCamera.transform.position, _combatCamera.transform.forward, out hit, 500f))
        {
            forceDirection = (hit.point - bulletSpawnTransform.position).normalized;
        }
        else
        {
            forceDirection = _combatCamera.transform.forward;
        }

        // Cria e dispara a bala
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnTransform.position, Quaternion.identity, GameObject.FindGameObjectWithTag("WorldObjectHolder").transform);
        bullet.GetComponent<Rigidbody>().AddForce(forceDirection * bulletSpeed, ForceMode.Impulse);

        ammo--;

        Invoke(nameof(ReseShoot), bulletColdown);
    }

    private void ReseShoot()
    {
        canShoot = true;
    }

    private void UpdateCamera(bool isAimingPressed)
    {
        _combatCamera.gameObject.SetActive(isAimingPressed);
        _reticle.SetActive(isAimingPressed);
        _weapon.SetActive(isAimingPressed);
        _freeLook.gameObject.SetActive(!isAimingPressed);
    }

    public void GiveAmmo(int numberOfAmmo)
    {
        ammo += numberOfAmmo;
    }
}
