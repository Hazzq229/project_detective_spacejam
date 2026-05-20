using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrimeFlashback : TextSequenceManager
{

    [SerializeField] GameObject crimesceneImage;
    [SerializeField] GameObject evidence;
    void OnEnable()
    {
        TextEvents.OnTextRevealCompleted += HandleTextCompleted;
        
        _currentIndex = 0;
        StartSequence();
    }

    void OnDisable()
    {
        TextEvents.OnTextRevealCompleted -= HandleTextCompleted;
    }

    protected override void AdvanceSequence()
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
           crimesceneImage.SetActive(false);
           evidence.SetActive(false);
           CollectedItem.instance.additem();
        }
    }
}
