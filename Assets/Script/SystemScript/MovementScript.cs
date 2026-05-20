using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MovementScript : MonoBehaviour
{
    [SerializeField] Button button_room1;
    [SerializeField] Button button_room2;
    [SerializeField] CanvasGroup blackScreen;
    [SerializeField] float transition_duration = 1f;
    [SerializeField] GameObject room1;
    [SerializeField] GameObject room2;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void movetoroom1()
    {
        StopAllCoroutines();
        StartCoroutine(movementtransition());
        room1.SetActive(true);
        room2.SetActive(false);
    }
    public void movetoroom2()
    {
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
        
    }
}
