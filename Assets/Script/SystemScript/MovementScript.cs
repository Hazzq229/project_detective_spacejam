using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MovementScript : MonoBehaviour
{
    public static MovementScript instance;
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
    [SerializeField] GameObject button_room1;
    [SerializeField] GameObject button_room2;
    [SerializeField] CanvasGroup blackScreen;
    [SerializeField] float transition_duration = 1f;
    [SerializeField] GameObject room1;
    [SerializeField] GameObject room2;
    public enum OnDialogue
    {
        yes,
        no,
    }
    [HideInInspector]public OnDialogue dialoguecondition = OnDialogue.no;
    bool isinroom1 = true;
    bool isinroom2 = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void checkdialougecondition()
    {
        if (dialoguecondition == OnDialogue.yes)
        {
           
         button_room1.SetActive(false);
            button_room2.SetActive(false);
        }
        else
        {
          if(isinroom1)
            {
                button_room1.SetActive(true);
                button_room2.SetActive(false);
            }
            else if(isinroom2)
            {
                button_room1.SetActive(false);
                button_room2.SetActive(true);
            }
        }
    }
    public void movetoroom1()
    {
        isinroom1 = true;
        isinroom2 = false;
        StopAllCoroutines();
        StartCoroutine(movementtransition());
        room1.SetActive(true);
        room2.SetActive(false);
    }
    public void movetoroom2()
    {
        isinroom2 = true;
        isinroom1 = false;
        StopAllCoroutines();
        StartCoroutine(movementtransition());
        room1.SetActive(false);
        room2.SetActive(true);
    }
    IEnumerator movementtransition()
    {
        blackScreen.gameObject.SetActive(true);
        float elapsed = 0f;
        
       
        while (elapsed < transition_duration)
        {
            elapsed += Time.deltaTime;
            blackScreen.alpha = Mathf.Lerp(0, 1, elapsed / transition_duration);
            yield return null;
        }
        blackScreen.alpha = 1;
      
        elapsed = 0f;
        while (elapsed < transition_duration)
        {
            elapsed += Time.deltaTime;
            blackScreen.alpha = Mathf.Lerp(1, 0, elapsed / transition_duration);
            yield return null;
        }
        blackScreen.alpha = 0;
        blackScreen.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        checkdialougecondition();
    }
}
