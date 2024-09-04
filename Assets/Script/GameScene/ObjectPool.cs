using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] GameObject _enemyPrefab;

    GameObject[] _enemy;

    GameObject[] targetPool;

    private void Awake()
    {
        _enemy = new GameObject[20];

        Seting();
    }

    void Seting()
    {
        for (int i = 0; i < _enemy.Length; i++)
        {
            _enemy[i] = Instantiate(_enemyPrefab);
            _enemy[i].SetActive(false);
        }
    }

    internal GameObject GetObject(string ObjectName)
    {
        switch (ObjectName)
        {
            case "enemy":
                targetPool = _enemy;
                break;
        }

        for (int i = 0; i < targetPool.Length; i++)
        {
            if (false == targetPool[i].activeSelf)
            {
                targetPool[i].SetActive(true);
                return targetPool[i];
            }
        }

        return null;
    }
}
