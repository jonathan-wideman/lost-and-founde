using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cart : MonoBehaviour
{

    public Transform centerOfMassAnchor = null;
    public List<ItemData> items;

    public Rigidbody rb;

    private void Awake()
    {
        items = new List<ItemData>();
        rb = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (centerOfMassAnchor != null) {
            rb.centerOfMass = centerOfMassAnchor.localPosition;
        }
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

    public void EmptyItems () {
        items = new List<ItemData>();
    }
}
