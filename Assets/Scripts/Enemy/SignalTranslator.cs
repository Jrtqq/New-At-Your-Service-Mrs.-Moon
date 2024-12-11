using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

namespace EnemyScripts
{
    [RequireComponent(typeof(TMP_Text))]
    public class SignalTranslator : MonoBehaviour
    {
        [SerializeField] private PlayerSearcher _playerSearcher;

        private TMP_Text _messageBox;
        private float _messageTime = 3;

        private Coroutine _coroutine;

        private void Awake()
        {
            _messageBox = GetComponent<TMP_Text>();
        }

        private void OnEnable()
        {
            _playerSearcher.Spotted += Warning;
            _playerSearcher.Lost += Confusion;
        }

        private void OnDisable()
        {
            _playerSearcher.Spotted -= Warning;
            _playerSearcher.Lost -= Confusion;
        }

        public void Warning()
        {
            _messageBox.text = "!";
            _coroutine = StartCoroutine(ClearDelayed());
        }

        public void Confusion()
        {
            _messageBox.text = "?";
            _coroutine = StartCoroutine(ClearDelayed());
        }

        private IEnumerator ClearDelayed()
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);

            yield return new WaitForSeconds(_messageTime);
            _messageBox.text = "";
        }
    }
}