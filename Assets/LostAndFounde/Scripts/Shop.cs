using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(Rigidbody))]
public class Shop : MonoBehaviour
{

    public TextMeshProUGUI hudTextbox = null;
    public TextMeshProUGUI textbox = null;
    public float itemSellingDelay = 1f;
    public List<ItemData> items;
    public int money = 0;

    private Coroutine sellCoroutine = null;


    private void Awake()
    {
        items = new List<ItemData>();
    }

    private void Start()
    {
        UpdateHud();
    }

    IEnumerator SellItem(float delay)
    {
        while (items.Count > 0)
        {
            // sell an item
            Debug.Log("Selling " + items[0].displayName + " for " + items[0].value);
            // string message = "\nSold " + items[0].displayName + " for " + items[0].value;
            string message = $"\nSold {items[0].displayName} for {items[0].value}";
            // string message = $"\nSold Willow battleaxe of misery for {items[0].value}";
            textbox.text = textbox.text + message;
            // textbox.text = textbox.text + "\nBlah";
            money += items[0].value;
            UpdateHud();
            items.RemoveAt(0);

            // wait
            yield return new WaitForSeconds(delay);
        }
        yield return new WaitForSeconds(5f);
        textbox.text = "";
    }

    private void OnTriggerEnter(Collider other)
    {
        Cart cart = other.transform.GetComponentInParent<Cart>();
        if (cart != null)
        {
            // Debug.Log("Cart has entered shop");
            items = cart.items;
            cart.EmptyItems();
            // DebugLogItems();
            if (sellCoroutine != null)
            {
                StopCoroutine(sellCoroutine);
            }
            sellCoroutine = StartCoroutine(SellItem(itemSellingDelay));
        }

    }

    void DebugLogItems()
    {
        Debug.Log("Items in shop:");
        foreach (ItemData datum in items)
        {
            Debug.Log(datum.displayName);
        }
    }

    public void UpdateHud()
    {
        hudTextbox.text = "" + money;
    }
}
