using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeScene : MonoBehaviour
{
    [Header("Scene Changer Settings")]
    [SerializeField] string _nextSceneName;
    [SerializeField] string _changeScenePrompt = "Klik tombol apapun untuk lanjut";
    [SerializeField] TMP_Text _changeSceneText;
    [SerializeField] float _transitionDelay;
    [SerializeField] Animator _fadeAnimator;
    [SerializeField] GameObject _transitionImage;
    bool _isReadyChangeScene = false;
    bool _isChangingScene;

    void OnEnable()
    {
        GameplayEvents.OnChangeSceneReady += PrepareChangeScene;
    }
    void OnDisable()
    {
        GameplayEvents.OnChangeSceneReady -= PrepareChangeScene;
    }
    void Start()
    {
        StartCoroutine(FadeOutCoroutine());        
    }
    void Update()
    {
        if (!_isReadyChangeScene)
            return;
        
        if (_isChangingScene)
            return;

        if (Input.anyKeyDown)
        {
            _isChangingScene = true;
            StartCoroutine(OnChangeScene());
        }
    }
    void PrepareChangeScene()
    {
        StartCoroutine(EnableSceneChange());
    }
    IEnumerator EnableSceneChange()
    {
        yield return null;

        _isReadyChangeScene = true;
        _changeSceneText.text = _changeScenePrompt;
    }
    // From alpha 0 to 1
    IEnumerator FadeInCoroutine()
    {
        _transitionImage.SetActive(true);
        _fadeAnimator.Play("FadeIn");
        yield return new WaitForSeconds(_transitionDelay);
    }
    // Opposite of fade in
    IEnumerator FadeOutCoroutine()
    {
        _fadeAnimator.Play("FadeOut");
        yield return new WaitForSeconds(_transitionDelay);

        _transitionImage.SetActive(false);

        GameplayEvents.OnFadeOutComplete?.Invoke();
    }
    IEnumerator OnChangeScene()
    {
        yield return StartCoroutine(FadeInCoroutine());

        if (_nextSceneName != null)
            SceneManager.LoadScene(_nextSceneName);
    }
}