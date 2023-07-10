using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EnemyCounter : MonoBehaviour
{
    [SerializeField] private Text enemyCountText;

    private ObjectSpawner _objectSpawner;
    private void Awake()
    {
        _objectSpawner = FindObjectOfType<ObjectSpawner>();
    }

    private void Update()
    {
        enemyCountText.text = _objectSpawner.enemyList.Count.ToString(); 
        if (_objectSpawner.enemyList.Count ==0)
        {
            GameEvents.winEvent?.Invoke();
        }
    }
}
