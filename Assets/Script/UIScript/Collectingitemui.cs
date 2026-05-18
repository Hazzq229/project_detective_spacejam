using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Collectingitemui : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI totalitemtext;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        totalitemtext.text = "Item Collected: " + CollectedItem.instance.total_item.ToString() + "/" + CollectedItem.instance.item_needed.ToString();
    }
}
