using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollectedItem : MonoBehaviour
{
    public static CollectedItem instance;
    public string inspectedscenename;
    public int total_item;
    public int item_needed;

    // Start is called before the first frame update
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
    public void additem()
    {
        total_item += 1;
    }
    public void checktotalitem()
    {
        if (total_item == item_needed)
        {
            Debug.Log("You have collected all the items!");
            SceneManager.LoadScene(inspectedscenename);
        }

    }

    void Start()
    {
        
    }
    

    // Update is called once per frame
    void Update()
    {
        checktotalitem();
        
    }
}
