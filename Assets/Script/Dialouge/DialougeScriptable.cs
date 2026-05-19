using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New StoryScene", menuName = "New Cutscene")]
public class DialougeScriptable : ScriptableObject
{
      public CharacterDialouge nextscene;
    [System.Serializable]
    public struct Sentence
    {
        public string text;
        public string charName;
        public Sprite characters;
        public Sprite character2;
        public AudioClip sfx;
     
        public bool hidechar1;
        public bool hidechar2;
        public bool notspeakingcut;
        public bool isscenenended;
        public Sprite background;
        public Sprite cutscene;
        public bool char1disappear;
        public bool char2disappear;
    }
}
