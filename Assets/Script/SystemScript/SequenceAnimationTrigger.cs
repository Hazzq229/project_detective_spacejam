using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceAnimationTrigger : MonoBehaviour
{
    void TriggerTypewriterPause()
    {
        TextEvents.OnPauseRequested?.Invoke();
    }
    void TriggerTypewriterResume()
    {
        TextEvents.OnResumeRequested?.Invoke();
    }
}
