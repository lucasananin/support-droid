using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    // Referencias;
    WeaponBehaviour _weaponScript = null;

    // Mensagens;
    private void Start()
    {
        _weaponScript = GetComponentInParent(typeof(WeaponBehaviour)) as WeaponBehaviour;
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Fire"))
        {
            HealthBehaviour _healthScript = other.GetComponent(typeof(HealthBehaviour)) as HealthBehaviour;

            if (_healthScript != null)
            {
                if (_weaponScript.GetWeaponID() == _healthScript.GetHealthID())
                {
                    _healthScript.HealthRegenerate(_weaponScript.GetCureValue());
                }
            }
        }
    }
}
