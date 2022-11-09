using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Infrastructure.Data;
using Configs;

namespace UI.Menu
{
    public sealed class LevelSelection : MonoBehaviour
    {
        [SerializeField] private List<LevelSelectionData> _levels;
        [SerializeField] private LoadLevelOption _loadLevelOption;

        [SerializeField] private Text _text;
        [SerializeField] private Button _leftArrow;
        [SerializeField] private Button _rightArrow;

        [SerializeField] private AudioSource _buttonClickSound;

        private int _currentIndex = 0;

        private void Start()
        {
            ChangeLevel();
        }

        private void OnEnable()
        {
            _leftArrow.onClick.AddListener(PreviousLevel);
            _rightArrow.onClick.AddListener(NextLevel);
        }

        private void PreviousLevel()
        {
            _currentIndex = (_currentIndex - 1) < 0 ? _levels.Count - 1 : _currentIndex - 1;
            ChangeLevel();
        }

        private void NextLevel()
        {
            _currentIndex = (_currentIndex + 1) >= _levels.Count ? 0 : _currentIndex + 1;
            ChangeLevel();
        }

        private void ChangeLevel()
        {
            _text.text = _levels[_currentIndex].Name;
            _loadLevelOption.LevelNumber = _levels[_currentIndex].Number;

            _buttonClickSound.Play();
        }

        private void OnDisable()
        {
            _leftArrow.onClick.RemoveAllListeners();
            _rightArrow.onClick.RemoveAllListeners();
        }
    }
}