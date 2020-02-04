using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatUntil : MonoBehaviour
{
    // Referencias;
    [SerializeField] GameObject[] _objs = null;

    // Valores;
    [SerializeField] int _rndIndex = 0;
    [SerializeField] bool _isCrashing = false;

    // Mensagens;
    private void Start()
    {
        for (int _i = 0; _i < 3; _i++)
        {
            RepeatUntilIsDifferent();
        }
    }

    // Personalizados;
    private void RepeatUntilIsDifferent()
    {
        bool _isDifferent = false;

        while (!_isDifferent)
        {
            int _lastIndex = _rndIndex;
            _rndIndex = Random.Range(0, _objs.Length);

            if (_rndIndex != _lastIndex)
            {
                _isDifferent = true;
                print("Nao Repetiu! " + _rndIndex);
            }
            else
            {
                print("Repetiu! " + _rndIndex);
            }
        }

        _isCrashing = true;
        print("Passou! " + _isCrashing);
    }

    // Checar se o dispositivo ja esta com mal funcionamento.
    // if (true) repeat; else true;
}
