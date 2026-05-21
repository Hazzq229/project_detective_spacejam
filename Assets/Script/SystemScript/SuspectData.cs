using UnityEngine;

[CreateAssetMenu(fileName = "New Suspect", menuName = "Detective/Suspect Data")]
public class SuspectData : ScriptableObject
{
    public string suspectName;
    [TextArea(3, 5)]
    public string alibi;
    public string correctWeaponKeyword;
    public bool isGuilty;

    [TextArea(3, 5)]
    public string explanationText;
}