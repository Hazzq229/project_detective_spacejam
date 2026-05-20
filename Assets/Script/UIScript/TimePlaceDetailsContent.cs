using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(TMP_Text))]
public class TimePlaceDetailsContent : MonoBehaviour
{
    [Header("Text Settings")]
    [TextArea]
    [SerializeField] string _detailsText = "Kensington Waffle House,\n30 Oktober 1956";
    
    void OnEnable()
    {
        TextEvents.OnTextRevealCompleted += ChangeSceneReady;
        GameplayEvents.OnFadeOutComplete += ChangeText;
    }
    void OnDisable()
    {
        TextEvents.OnTextRevealCompleted -= ChangeSceneReady;
        GameplayEvents.OnFadeOutComplete -= ChangeText;
    }

    void ChangeText()
    {
        TextEvents.OnTextRequested(_detailsText);
    }
    void ChangeSceneReady()
    {
        GameplayEvents.OnChangeSceneReady?.Invoke();
    }
}