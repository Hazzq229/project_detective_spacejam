using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextSequenceManager : MonoBehaviour
{
    [Header("Sequence Data")]
    [TextArea(3, 5)]

    [SerializeField] protected List<string> _dialogueLines;
    [SerializeField] protected GameObject _nextIndicator;

    protected int _currentIndex = 0;
    protected bool _isWaitingForNext = false;

    void OnEnable()
    {
        TextEvents.OnTextRevealCompleted += HandleTextCompleted;
        GameplayEvents.OnFadeOutComplete += StartSequence;
    }

    void OnDisable()
    {
        TextEvents.OnTextRevealCompleted -= HandleTextCompleted;
        GameplayEvents.OnFadeOutComplete -= StartSequence;
    }

    protected void StartSequence()
    {
        if (_nextIndicator != null)
            _nextIndicator.SetActive(false);

        if (_dialogueLines.Count > 0)
        {
            PlayLine(_currentIndex);
        }
    }

    void Update()
    {
        if (!_isWaitingForNext) return;

        if (Input.GetMouseButtonDown(0))
        {
            AdvanceSequence();
        }
    }

    protected void HandleTextCompleted()
    {
        _isWaitingForNext = true;
        if (_nextIndicator != null)
            _nextIndicator.SetActive(true);
    }

    protected virtual void AdvanceSequence()
    {
        _isWaitingForNext = false;
        if (_nextIndicator != null)
            _nextIndicator.SetActive(false);

        _currentIndex++;

        if (_currentIndex < _dialogueLines.Count)
        {
            PlayLine(_currentIndex);
        }
        else
        {
            GameplayEvents.OnChangeSceneReady?.Invoke();
        }
    }

    protected void PlayLine(int index)
    {
        TextEvents.OnTextRequested?.Invoke(_dialogueLines[index]);
    }
}