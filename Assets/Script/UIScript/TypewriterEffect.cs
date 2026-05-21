// Attach this script to TMP gameobject that need typewriter fx
// Credits to ChristinaCreatesGames for the tutorial & script example  
using System;
using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class TypewriterEffect : MonoBehaviour
{
    TMP_Text _textBox;

    // Typewriter Functionality
    int _currentVisibleCharacterIndex;
    Coroutine _typewriterCoroutine;
    bool _readyForNewText = true;

    WaitForSeconds _simpleDelay;
    WaitForSeconds _interpunctuationDelay;

    [Header("Typewriter Settings")]
    [SerializeField] float charactersPerSecond = 20;
    [SerializeField] float interpunctuationDelay = 0.5f;

    // Skip Functionality
    [Header("Skip Options")]
    [SerializeField] bool quickSkip;
    [SerializeField][Min(1)] int skipSpeedup = 5;
    public bool CurrentlySkipping { get; private set; }
    WaitForSeconds _skipDelay;

    // Event Functionality 
    WaitForSeconds _textboxFullEventDelay;
    [SerializeField][Range(0.1f, 0.5f)] float sendDoneDelay = 0.25f;
    bool _isPaused = false;

    void Awake()
    {
        _textBox = GetComponent<TMP_Text>();

        _simpleDelay = new WaitForSeconds(1 / charactersPerSecond);
        _interpunctuationDelay = new WaitForSeconds(interpunctuationDelay);

        _skipDelay = new WaitForSeconds(1 / (charactersPerSecond * skipSpeedup));
        _textboxFullEventDelay = new WaitForSeconds(sendDoneDelay);
    }

    void OnEnable()
    {
        TextEvents.OnTextRequested += PrepareForNewText;
        TextEvents.OnPauseRequested += PauseTypewriter;
        TextEvents.OnResumeRequested += ResumeTypewriter;
    }

    // Tambahkan di OnDisable
    void OnDisable()
    {
        TextEvents.OnTextRequested -= PrepareForNewText;
        TextEvents.OnPauseRequested -= PauseTypewriter;
        TextEvents.OnResumeRequested -= ResumeTypewriter;
    }

    void PauseTypewriter() => _isPaused = true;
    void ResumeTypewriter() => _isPaused = false;

    #region Skip Functionality
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (_textBox.maxVisibleCharacters != _textBox.textInfo.characterCount - 1)
                Skip();
        }
    }
    void Skip()
    {
        if (CurrentlySkipping)
            return;

        CurrentlySkipping = true;

        if (!quickSkip)
        {
            StartCoroutine(SkipSpeedupReset());
            return;
        }

        CheckLinkEvent(_textBox.textInfo);

        StopCoroutine(_typewriterCoroutine);
        _textBox.maxVisibleCharacters = _textBox.textInfo.characterCount;
        _readyForNewText = true;
        TextEvents.OnTextRevealCompleted?.Invoke();
    }

    private IEnumerator SkipSpeedupReset()
    {
        yield return new WaitUntil(() => _textBox.maxVisibleCharacters == _textBox.textInfo.characterCount - 1);
        CurrentlySkipping = false;
    }
    #endregion

    #region Typewriter Functionality
    private void PrepareForNewText(string newText)
    {
        if (!_readyForNewText)
            return;

        CurrentlySkipping = false;
        _readyForNewText = false;

        if (_typewriterCoroutine != null)
            StopCoroutine(_typewriterCoroutine);

        _textBox.text = newText;
        _textBox.maxVisibleCharacters = 0;
        _currentVisibleCharacterIndex = 0;
        
        _typewriterCoroutine = StartCoroutine(Typewriter());
    }
    private IEnumerator Typewriter()
    {
        yield return null;
        _textBox.ForceMeshUpdate();
        TMP_TextInfo textInfo = _textBox.textInfo;

        while (_currentVisibleCharacterIndex < textInfo.characterCount + 1)
        {
            while (_isPaused) yield return null;

            var lastCharacterIndex = textInfo.characterCount - 1;

            if (_currentVisibleCharacterIndex >= lastCharacterIndex)
            {  AudioManager.instance.PlayTypingSound();
                _textBox.maxVisibleCharacters++;
                yield return _textboxFullEventDelay;
                TextEvents.OnTextRevealCompleted?.Invoke();
                _readyForNewText = true;
                yield break;
            }
            
            char character = textInfo.characterInfo[_currentVisibleCharacterIndex].character;

            _textBox.maxVisibleCharacters++;

            CheckLinkEvent(textInfo);

            if (!CurrentlySkipping &&
                (character == '?' || character == '.' || character == ',' || character == ':' ||
                    character == ';' || character == '!' || character == '-'))
            {
                yield return _interpunctuationDelay;
            }
            else
            {
                yield return CurrentlySkipping ? _skipDelay : _simpleDelay;
            }
          
            TextEvents.OnCharacterRevealed?.Invoke(character);
            _currentVisibleCharacterIndex++;
        }
    }
    #endregion

    #region Link Event
    void CheckLinkEvent(TMP_TextInfo textInfo)
    {
        if (textInfo.linkCount < 0)
            return;

        foreach (var linkInfo in textInfo.linkInfo.Take(textInfo.linkCount))
        {
            if (linkInfo.linkTextfirstCharacterIndex >= _currentVisibleCharacterIndex)
                TextEvents.OnLinkTriggered?.Invoke(linkInfo.GetLinkID());
        }
    }
    #endregion
}
