using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioSource BGM;
    public AudioSource SFX;
    public AudioClip Typingsound;
    public AudioClip clickSound;
        void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        public void PlayTypingSound()
        {
            if (Typingsound != null)
            {
                SFX.PlayOneShot(Typingsound);
            }
        }
        public void PlayClickSound()
        {
            if (clickSound != null)
            {
                SFX.PlayOneShot(clickSound);
            }
        }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
