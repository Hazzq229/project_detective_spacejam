using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SuspectUI : MonoBehaviour,
    IPointerEnterHandler,
    IPointerExitHandler
{
    [Header("References")]
    [SerializeField] SuspectData _myData;
    [SerializeField] AccusationManager _manager;
    [SerializeField] Animator _suspectAnimator;
    [SerializeField] SuspectUI _otherSuspect;
    [SerializeField] Image _portraitImage;
    [SerializeField] Button _button;

    [Header("Visual States")]
    [SerializeField] Color _normalColor = Color.gray;
    [SerializeField] Color _hoverColor = Color.white;
    [SerializeField] Color _selectedColor = Color.white;

    bool _isHovered;
    bool _isSelected;

    void Start()
    {
        RefreshVisual();
    }

    public void OnSuspectClicked()
    {
        if (!_manager.isActiveAndEnabled)
            return;

        _manager.SelectSuspect(_myData);

        SetSelected(true);

        if (_otherSuspect != null)
            _otherSuspect.SetSelected(false);

        _suspectAnimator.SetTrigger("MoveToCenter");

        if (_otherSuspect != null)
            _otherSuspect.TriggerFadeOut();
    }

    public void TriggerFadeOut()
    {
        _suspectAnimator.SetTrigger("FadeOut");
    }

    public void RevertAnimation()
    {
        _suspectAnimator.SetTrigger("Cancel");

        SetSelected(false);
    }

    public void SetSelected(bool selected)
    {
        _isSelected = selected;

        RefreshVisual();
    }

    void RefreshVisual()
    {
        if (_portraitImage == null)
            return;

        if (_isSelected)
        {
            _portraitImage.color = _selectedColor;
        }
        else if (_isHovered)
        {
            _portraitImage.color = _hoverColor;
        }
        else
        {
            _portraitImage.color = _normalColor;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Hover Enter");
        _isHovered = true;
        RefreshVisual();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Hover Exit");
        _isHovered = false;
        RefreshVisual();
    }
}