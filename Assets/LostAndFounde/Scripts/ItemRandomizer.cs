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
    }
}
