using TMPro;
using UnityEngine;

public class AccusationManager : MonoBehaviour
{
    enum AccusationState
    {
        Idle,
        ShowingDetails,
        WaitingExplanationInput,
        ShowingExplanation,
        WaitingFinishInput
    }

    [Header("UI References")]
    [SerializeField] GameObject _clipboardPanel;
    [SerializeField] TMP_Text _subjectText;
    [SerializeField] GameObject _confirmAccuseButton;
    [SerializeField] GameObject _cancelButton;
    [SerializeField] GameObject _continuePrompt;
    [SerializeField] TMP_Text _stampText;

    [Header("Animators")]
    [SerializeField] Animator _stampAnimator;
    [SerializeField] Animator _sceneTitleAnimator;

    [Header("Suspects")]
    [SerializeField] SuspectUI[] _allSuspects;

    SuspectData _selectedSuspect;
    AccusationState _currentState = AccusationState.Idle;

    void OnEnable()
    {
        TextEvents.OnTextRevealCompleted += HandleTextRevealCompleted;
        GameplayEvents.OnFadeOutComplete += PlaySceneTitleAnimation;
    }

    void OnDisable()
    {
        TextEvents.OnTextRevealCompleted -= HandleTextRevealCompleted;
        GameplayEvents.OnFadeOutComplete -= PlaySceneTitleAnimation;
    }

    void Start()
    {
        ResetUI();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (_currentState == AccusationState.WaitingExplanationInput)
            {
                ShowExplanation();
            }
            else if (_currentState == AccusationState.WaitingFinishInput)
            {
                GameplayEvents.OnChangeSceneReady?.Invoke();
            }
        }
    }

    void ResetUI()
    {
        _clipboardPanel.SetActive(false);
        _confirmAccuseButton.SetActive(false);
        _cancelButton.SetActive(false);
        _continuePrompt.SetActive(false);
        _currentState = AccusationState.Idle;
    }

    // Dipanggil dari SuspectUI saat tersangka diklik
    public void SelectSuspect(SuspectData suspectData)
    {
        _selectedSuspect = suspectData;
        _clipboardPanel.SetActive(true);
        _confirmAccuseButton.SetActive(true);
        _cancelButton.SetActive(true);
        _stampAnimator.Play("Stamp_Default");
        _currentState = AccusationState.ShowingDetails;

        string clipboardText = $"Suspect: {_selectedSuspect.suspectName}\nAlibi: {_selectedSuspect.alibi}";
        TextEvents.OnTextRequested?.Invoke(clipboardText);
    }

    public void CancelSelection()
    {
        ResetUI();
        _selectedSuspect = null;
        TextEvents.OnTextRequested?.Invoke("");

        foreach (var suspect in _allSuspects)
        {
            suspect.RevertAnimation();
        }
    }

    public void ConfirmAccusation()
    {
        _confirmAccuseButton.SetActive(false);
        _cancelButton.SetActive(false);

        if (_selectedSuspect.isGuilty)
        {
            _stampAnimator.Play("Stamp_Red");
            _stampText.text = "Guilty";
        }
        else
        {
            _stampAnimator.Play("Stamp_Green");
            _stampText.text = "Innocent";
        }

        _continuePrompt.SetActive(true);
        _currentState = AccusationState.WaitingExplanationInput;
    }

    void ShowExplanation()
    {
        _continuePrompt.SetActive(false);
        _currentState = AccusationState.ShowingExplanation;

        _subjectText.text = "Explanation";
        TextEvents.OnTextRequested?.Invoke(_selectedSuspect.explanationText);
    }

    void HandleTextRevealCompleted()
    {
        if (_currentState == AccusationState.ShowingExplanation)
        {
            if (_selectedSuspect.isGuilty)
            {
                _stampAnimator.Play("Stamp_Green");
                _stampText.text = "Success";
            }
            else
            {
                _stampAnimator.Play("Stamp_Red");
                _stampText.text = "Failed";
            }

            _continuePrompt.SetActive(true);
            _currentState = AccusationState.WaitingFinishInput;
        }
    }
    void PlaySceneTitleAnimation()
    {
        _sceneTitleAnimator.Play("TitleImage_FadeOut");
    }
}