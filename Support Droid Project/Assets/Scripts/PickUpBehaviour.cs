using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpBehaviour : MonoBehaviour
{
    // Referencias;
    [SerializeField] Color32 _defaultColor = Color.white;
    [SerializeField] Color32 _newColor = Color.red;
    private Renderer _rend = null;

    // Valores;
    [SerializeField] float _rotSpeed = 10f;
    [SerializeField] float _smoothTime = 5f;
    [SerializeField] bool _hasDamaged = false;

    // Mensagens;
    private void Start()
    {
        _rend = GetComponentInChildren(typeof(Renderer)) as Renderer;
        _defaultColor = _rend.material.color;
    }

    private void Update()
    {
        RotateObject();
        UpdateColor();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            WeaponBehaviour _weaponScript = other.GetComponentInChildren(typeof(WeaponBehaviour)) as WeaponBehaviour;
            PickUpSpawner _spawnerScript = GameObject.Find("PickUpSpawner").GetComponent(typeof(PickUpSpawner)) as PickUpSpawner;

            if (_weaponScript != null)
            {
                if (_weaponScript.GetCurrentAmmo() != _weaponScript.GetMaxAmmo())
                {
                    _weaponScript.RefillAmmo();

                    if (_spawnerScript != null)
                    {
                        _spawnerScript.PickUpCollected();
                    }

                    Destroy(this.gameObject);
                }
                else
                {
                    _hasDamaged = true;
                }
            }
        }
    }

    private void OnDestroy()
    {
        if (_rend != null)
            Destroy(_rend.material);
    }

    // Personalizados;
    private void RotateObject()
    {
        transform.Rotate(0f, (_rotSpeed * Time.deltaTime) * 10f, 0f);
    }

    private void UpdateColor()
    {
        if (_hasDamaged)
        {
            _rend.material.color = _newColor;
            _hasDamaged = false;
        }
        else
        {
            if (_rend.material.color == _defaultColor) return;
            _rend.material.color = Color.Lerp(_rend.material.color, _defaultColor, _smoothTime * Time.deltaTime);
        }
    }
}
