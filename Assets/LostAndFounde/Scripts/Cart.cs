using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cart : MonoBehaviour
{

    public List<ItemData> items;

    private void Awake()
    {
        items = new List<ItemData>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddItemData(ItemData itemData)
    {
        items.Add(itemData);
        DebugLogItems();
    }

    void DebugLogItems()
    {
        Debug.Log("Items in cart:");
        foreach (ItemData datum in items)
        {
            Debug.Log(datum.displayName);
        }
    }
}
