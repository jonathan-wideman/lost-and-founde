using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Item))]
public class ItemRandomizer : MonoBehaviour
{
    private Item item;

    private void Awake() {
        item = GetComponent<Item>();
    }

    private void Start() {
        item.itemData = ItemManager.GenerateItemData();
        Transform choices = transform.Find("Model/choices");
        if (choices != null) {
            int c = Random.Range(0, choices.childCount);
            int i = 0;
            foreach (Transform choice in choices)
            {
                if (c != i) {
                    choice.gameObject.SetActive(false);
                }
                i++;
            } 
        }
        Destroy(this);
    }
}
