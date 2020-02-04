using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBehaviour : MonoBehaviour
{
    // Referencias;
    [SerializeField] ParticleSystem _firstEffect = null;
    [SerializeField] ParticleSystem _secondEffect = null;

    // Valores;
    [SerializeField] int _healthID = 0;
    [SerializeField] float _damageRate = 0.1f;
    [SerializeField] float _damageInterval = 0.5f;
    [SerializeField] float _maxHealth = 100;
    [SerializeField] float _currentHealth = 0;
    [SerializeField] bool _isDestroyed = false;
    [SerializeField] bool _isRegenerating = false;
    [SerializeField] bool _isCrashing = false;
    private float _nextDamage = 0f;

    // Mensagens;
    private void Start()
    {
        _currentHealth = _maxHealth;
    }

    private void Update()
    {
        if (_isCrashing)
        {
            DamageTaken(1);
            DamageState();
        }
    }

    // Personalizados;
    private void DamageTaken(float _val)
    {
        if (_isDestroyed == true || _isRegenerating) return;

        _nextDamage += Time.deltaTime;

        if (_nextDamage >= _damageRate)
        {
            _nextDamage = 0f;
            _currentHealth -= _val;

            if (_currentHealth <= 0)
            {
                _isDestroyed = true;
                GameManager._destroyedDevices++;
            }

            _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);
        }
    }

    private void DamageState()
    {
        if (_currentHealth <= (_maxHealth * 0.4f) && !_secondEffect.isPlaying)
        {
            _secondEffect.Play();
        }
        else if (_currentHealth <= (_maxHealth * 0.8f) && !_firstEffect.isPlaying)
        {
            _firstEffect.Play();
        }
        else if (_currentHealth > (_maxHealth * 0.4f) && _secondEffect.isPlaying)
        {
            _secondEffect.Stop();
        }
        else if (_currentHealth > (_maxHealth * 0.8f) && _firstEffect.isPlaying)
        {
            _firstEffect.Stop();
        }
    }

    public void HealthRegenerate(float _val)
    {
        if (_isDestroyed) return;

        _isRegenerating = true;
        _currentHealth += _val;
        _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);
        StartCoroutine(DamageRoutine());
    }

    private IEnumerator DamageRoutine()
    {
        yield return new WaitForSeconds(_damageInterval);
        _isRegenerating = false;
    }

    // Especiais;
    public int GetHealthID()
    {
        return _healthID;
    }

    public bool GetCrashing()
    {
        return _isCrashing;
    }

    public void SetCrashing(bool _val)
    {
        _isCrashing = _val;
    }
}
