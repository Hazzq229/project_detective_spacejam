using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioSource BGM;
    public AudioSource SFX;
    public AudioClip Typingsound;
    public AudioClip clickSound;
    public AudioClip Stampsound;
    
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            gameObject.SetActive(true);
            
            // Ensure AudioSources are active
            if (BGM != null)
                BGM.gameObject.SetActive(true);
            if (SFX != null)
                SFX.gameObject.SetActive(true);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    void OnEnable()
    {
        // Ensure AudioSources are enabled
        if (BGM != null)
            BGM.gameObject.SetActive(true);
        if (SFX != null)
            SFX.gameObject.SetActive(true);
    }
    
    void Start()
    {
        // Ensure they stay active
        gameObject.SetActive(true);
        if (BGM != null)
            BGM.gameObject.SetActive(true);
        if (SFX != null)
            SFX.gameObject.SetActive(true);
    }
    
    void Update()
    {
        // Keep them active in case something disables them
        if (!gameObject.activeSelf)
            gameObject.SetActive(true);
        if (BGM != null && !BGM.gameObject.activeSelf)
            BGM.gameObject.SetActive(true);
        if (SFX != null && !SFX.gameObject.activeSelf)
            SFX.gameObject.SetActive(true);
    }
    
    public void PlayTypingSound()
    {
        if (Typingsound != null && SFX != null)
        {
            if (!SFX.gameObject.activeSelf)
                SFX.gameObject.SetActive(true);
            SFX.PlayOneShot(Typingsound);
        }
    }
    
    public void PlayClickSound()
    {
        if (clickSound != null && SFX != null)
        {
            if (!SFX.gameObject.activeSelf)
                SFX.gameObject.SetActive(true);
            SFX.PlayOneShot(clickSound);
        }
    }
    
   
}
