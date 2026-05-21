using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct VisualData
{
    public string keyword;
    public Sprite image;
}

public class VisualSequenceManager : MonoBehaviour
{
    [Header("UI Target")]
    [SerializeField] Image _backgroundImage;
    [SerializeField] CanvasGroup _fadeOverlay;

    [Header("Sequence Animation")]
    [SerializeField] Animator _sequenceAnimator;

    [Header("Visual Database")]
    [SerializeField] List<VisualData> _backgroundDatabase;

    [Header("Fade Transition Settings")]
    [SerializeField] float _fadeDuration = 0.5f;

    // Flag untuk melacak apakah efek fade background sedang berjalan
    bool _isTransitioningBackground = false;

    void OnEnable()
    {
        TextEvents.OnLinkTriggered += HandleLinkTriggered;
    }

    void OnDisable()
    {
        TextEvents.OnLinkTriggered -= HandleLinkTriggered;
    }

    void HandleLinkTriggered(string keyword)
    {
        if (string.IsNullOrEmpty(keyword)) return;

        var parts = keyword.Split(new[] { '_' }, 2);
        switch (parts[0])
        {
            case "BG":
                StartCoroutine(TransitionBackground(keyword));
                break;
            case "Anim":
                string animationName = parts[1];

                if (_sequenceAnimator == null)
                {
                    Debug.LogWarning("Sequence Animator is not assigned.");
                    return;
                }

                // Ubah eksekusi animasi menggunakan Coroutine
                StartCoroutine(PlayAnimationDelayed(animationName));
                break;
        }
    }

    IEnumerator PlayAnimationDelayed(string animationName)
    {
        // Tahan eksekusi jika background sedang transisi
        yield return new WaitWhile(() => _isTransitioningBackground);

        _sequenceAnimator.Play(animationName);
    }

    IEnumerator TransitionBackground(string keyword)
    {
        // Tandai bahwa transisi sedang berjalan
        _isTransitioningBackground = true;
        TextEvents.OnPauseRequested?.Invoke();

        yield return StartCoroutine(FadeOverlayAlpha(0f, 1f));

        ChangeVisual(keyword, _backgroundDatabase, _backgroundImage);

        yield return StartCoroutine(FadeOverlayAlpha(1f, 0f));

        // Transisi selesai, lepas flag sebelum meresume teks
        _isTransitioningBackground = false;
        TextEvents.OnResumeRequested?.Invoke();
    }

    private IEnumerator FadeOverlayAlpha(float from, float to)
    {
        if (_fadeOverlay == null)
            yield break;

        if (_fadeDuration <= 0f)
        {
            _fadeOverlay.alpha = to;
            yield break;
        }

        float time = 0f;
        _fadeOverlay.alpha = from;
        while (time < _fadeDuration)
        {
            time += Time.deltaTime;
            _fadeOverlay.alpha = Mathf.Lerp(from, to, time / _fadeDuration);
            yield return null;
        }
        _fadeOverlay.alpha = to;
    }

    void ChangeVisual(string targetKeyword, List<VisualData> database, Image targetUI)
    {
        foreach (var data in database)
        {
            if (data.keyword == targetKeyword)
            {
                targetUI.sprite = data.image;
                return;
            }
        }

        Debug.LogWarning($"Visual with keyword '{targetKeyword}' not found on database.");
    }
}