﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Managers;
using TMPro;
using UnityEngine;
 using UnityEngine.SceneManagement;
 using UnityEngine.UI;

namespace UI
{
    public class CanvasController : MonoBehaviour
    {
        [SerializeField] private GameObject winObject;
        [SerializeField] private GameObject loseObject;
        [SerializeField] private GameObject pauseObject;
        private object _gemScoreController;

        private RectTransform _rectTransform;

        private int _finalScore;

        private void Awake()
        {
            InitComponents();
        }

        private void Start()
        {
            SignUpEvents();
            DisableCanvasObjects();
        }
        private void InitComponents()
        {
            _rectTransform = GetComponent<RectTransform>();
        }
        

        private void SignUpEvents()
        {
            SignUpGameOverEvents();
        }
        private void SignUpGameOverEvents()
        {
            GameEvents.loseEvent += OnLoseEvent;
            GameEvents.winEvent += OnWinEvent;
        }

        public void LoadNextLevel()
        {
            PlayerPrefs.SetInt("PlayerLevel", PlayerPrefs.GetInt("PlayerLevel") + 1);
            SceneManager.LoadScene(0);
        }

        public void PauseGamePopUp()
        {
            pauseObject.SetActive(true);
        }

        public void LoadCurrentLevel()
        {
            SceneManager.LoadScene(0);
        }

        private void OnWinEvent()
        {
            winObject.SetActive(true);
        }

        private void OnLoseEvent()
        {
            loseObject.SetActive(true);
        }

        private void DisableCanvasObjects()
        {
            winObject.SetActive(false);
            loseObject.SetActive(false);
        }
        
    }
}