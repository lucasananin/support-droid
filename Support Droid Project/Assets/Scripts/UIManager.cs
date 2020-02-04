using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // Referencias;
    [SerializeField] Text _ammoText = null;
    [SerializeField] Text _timeText = null;
    private static UIManager _uiManager = null;

    // Mensagens;
    private void Start()
    {
        if (_uiManager == null)
        {
            _uiManager = this;
        }

        SetAmmo(100);
    }

    private void Update()
    {
        SetTime();
    }

    // Especiais;
    public static void SetAmmo(int _val)
    {
        _uiManager._ammoText.text = _val.ToString();
    }

    public static void SetTime()
    {
        _uiManager._timeText.text = ((int)Time.timeSinceLevelLoad).ToString();
    }
}
