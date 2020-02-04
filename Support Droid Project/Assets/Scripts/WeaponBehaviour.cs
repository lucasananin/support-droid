using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBehaviour : MonoBehaviour
{
    // Referencias;
    [SerializeField] ParticleSystem[] _weaponsPS = null;

    // Valores;
    [SerializeField] int _weaponID = 0;
    [SerializeField] int _maxAmmo = 100;
    [SerializeField] int _currentAmmo = 0;
    [SerializeField] float _cureValue = 1;
    [SerializeField] float _fireRate = 0.1f;
    private float _nextFire = 0f;

    // Mensagens;
    private void Start()
    {
        _currentAmmo = _maxAmmo;
    }

    private void Update()
    {
        Shoot();
        SwapWeapon();
    }

    // Personalizados;
    private void Shoot()
    {
        _nextFire += Time.deltaTime;

        if (Input.GetButton("Fire1") && _nextFire >= _fireRate && _currentAmmo > 0)
        {
            _nextFire = 0f;
            _weaponsPS[_weaponID].Play();
            _currentAmmo--;
            UIManager.SetAmmo(_currentAmmo);
        }
    }

    private void SwapWeapon()
    {
        if (Input.GetButtonDown("Jump"))
        {
            _weaponID = (_weaponID == 0) ? 1 : 0;
        }
    }

    public void RefillAmmo()
    {
        _currentAmmo = _maxAmmo;
        UIManager.SetAmmo(_maxAmmo);
    }

    // Especiais;
    public int GetWeaponID()
    {
        return _weaponID;
    }

    public float GetCureValue()
    {
        return _cureValue;
    }

    public int GetCurrentAmmo()
    {
        return _currentAmmo;
    }

    public int GetMaxAmmo()
    {
        return _maxAmmo;
    }
}
