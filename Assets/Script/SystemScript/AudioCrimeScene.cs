using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioCrimeScene : MonoBehaviour
{
    public AudioSource SFX;
        public AudioClip clickSound;
    // Start is called before the first frame update
    void Start()
    {
        
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
    // Update is called once per frame
    void Update()
    {
        
    }
}
