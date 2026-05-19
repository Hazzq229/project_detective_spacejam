using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDialouge : MonoBehaviour
{
    
    public List<Sentence> sentences;

    public CharacterDialouge nextscene;
    [System.Serializable]
    public struct Sentence
    {
        public string text;
        public string charName;
        public Sprite characters;
      
    }
    
    }

