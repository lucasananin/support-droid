using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpSpawner : MonoBehaviour
{
    // Referencias;
    [SerializeField] GameObject _pickUpPrefab = null;

    // Valores;
    [SerializeField] float _nextPickUp = 60f;

    // Mensagens;
    private void Start()
    {
        SpawnPickUp();
    }

    // Personalizados;
    private void SpawnPickUp()
    {
        Instantiate(_pickUpPrefab, transform.position + Vector3.up, transform.rotation);
    }

    public void PickUpCollected()
    {
        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        yield return new WaitForSeconds(_nextPickUp);
        SpawnPickUp();
    }
}
