﻿using System;
using System.Collections.Generic;
using Cinemachine;
using ScriptableObjects;
using ScriptableObjects.Scripts;
 using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    public struct GameEvents
    {
        public static Action winEvent;
        public static Action loseEvent;
        public static Action<GameObject> enemyDeathEvent;

        public static Action<GameObject> collectableEvent;

        public static void DestroyEvents()
        {
            winEvent = null;
            loseEvent = null; 
            collectableEvent = null;
            enemyDeathEvent = null;
        }
        
    }
    
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private bool transitionWithPrefab;
        [SerializeField] private GameData gameData;

        [HideInInspector] public LevelData LevelData;
        
        private const int LevelResetIndex = 1;
        
        private int _currentLevel;

        private int _levelIndex;

        private float _levelTimer = 0f;

        private void Awake()
        {
            LoadLevel();
        }

        public void LoadLevel()
        {
            InitLevelData();

            if (transitionWithPrefab)
            {
                InstantiateLevel();
                _levelTimer = LevelData.levelCompleteTime;
            }
        }

        private void FixedUpdate()
        {
            _levelTimer -= Time.time;
            /*if (_levelTimer <= 0)
            {
                GameEvents.loseEvent?.Invoke();
            }*/
            
        }

        private void InitLevelData()
        {
            _currentLevel = PlayerPrefs.GetInt("PlayerLevel");

            var lastLevelIndex = gameData.LastLevelIndex;
            _levelIndex = lastLevelIndex == gameData.levelsDataArray.Length - 1 ? LevelResetIndex : lastLevelIndex + 1;
            LevelData = gameData.levelsDataArray[_levelIndex];
            
#if UNITY_EDITOR
            Debug.Log($"Current Level: {_currentLevel.ToString()}");
#endif
        }
        
        private static void IncreasePlayerPrefLevel()
        {
            PlayerPrefs.SetInt("PlayerLevel", PlayerPrefs.GetInt("PlayerLevel") + 1);
        }
        
        private void InstantiateLevel()
        {
            Instantiate(gameData.levelsDataArray[_currentLevel % gameData.levelsDataArray.Length].levelObject);

            LevelData = gameData.levelsDataArray[_currentLevel % gameData.levelsDataArray.Length];
        }

        private void OnDestroy()
        {
            GameEvents.DestroyEvents();
        }
    }
}