using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioAccustion : MonoBehaviour
{
    public static AudioAccustion instance;
     void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public AudioSource SFX;
        public AudioClip Stampsound;
    // Start is called before the first frame update
    void Start()
    {
        
    }
 public void PlayStampSound()
    {
        if (Stampsound != null && SFX != null)
        {
            if (!SFX.gameObject.activeSelf)
                SFX.gameObject.SetActive(true);
            SFX.PlayOneShot(Stampsound);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
