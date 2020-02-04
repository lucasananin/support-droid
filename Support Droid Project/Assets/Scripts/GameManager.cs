using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Referencias;
    [SerializeField] HealthBehaviour[] _devices = null;

    // Valores;
    [SerializeField] bool _gameOver = false;
    [SerializeField] float _nextCrash = 60f;
    [SerializeField] int _startedDevices = 0;
    public static int _destroyedDevices = 0;

    // Mensagens;
    private void Start()
    {
        _destroyedDevices = 0;

        if (_devices != null)
        {
            for (int _i = 0; _i < 3; _i++)
            {
                NewCrash();
            }

            StartCoroutine(CrashRoutine());
        }
    }

    private void Update()
    {
        if (_destroyedDevices >= 3 && !_gameOver)
        {
            _gameOver = true;
            PauseMenu _pauseScript = GameObject.Find("Canvas").GetComponent(typeof(PauseMenu)) as PauseMenu;

            if (_pauseScript != null)
            {
                _pauseScript.GameOver();
            }
        }
    }

    // Personalizados;
    private void NewCrash()
    {
        bool _canCrash = false;

        if (_startedDevices == _devices.Length)
        {
            return;
        }

        while (!_canCrash)
        {
            int _randomDevice = Random.Range(0, _devices.Length);

            if (_devices[_randomDevice].GetCrashing() == false)
            {
                _canCrash = true;
            }

            _devices[_randomDevice].SetCrashing(true);
        }

        _startedDevices++;
    }

    private void CheckCrash()
    {
        foreach (var _item in _devices)
        {
            if (_item.GetCrashing().Equals(false))
            {
                break;
            }
            //else
            //{
            //    StopCoroutine(CrashRoutine());
            //}
        }
    }

    private IEnumerator CrashRoutine()
    {
        while (!_gameOver)
        {
            yield return new WaitForSeconds(_nextCrash);
            //CheckCrash();
            NewCrash();
        }
    }
}
