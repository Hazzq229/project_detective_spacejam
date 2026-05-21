using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrimeFlashback : TextSequenceManager
{

    [SerializeField] GameObject crimesceneImage;
    [SerializeField] GameObject evidence;

    void OnEnable()
    {
        MovementScript.instance.show_button = MovementScript.Showmovementbutton.yes;
        TextEvents.OnTextRevealCompleted += HandleTextCompleted;
        
        _currentIndex = 0;
        _isWaitingForNext = false;
        Invoke(nameof(StartSequence), 0.01f);
    }

    void OnDisable()
    {
        TextEvents.OnTextRevealCompleted -= HandleTextCompleted;
        CancelInvoke(nameof(StartSequence));
    }

    public void PlayCrimeFlashback()
    {
        gameObject.SetActive(true);
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
           MovementScript.instance.show_button = MovementScript.Showmovementbutton.no;
        }
    }
}
