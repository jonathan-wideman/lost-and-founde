using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHoldItem : MonoBehaviour
{

    public Transform itemSocket;
    public LayerMask interactLayerMask;
    public float interactionRange = 2f;

    public Item heldItem;


    void Start()
    {

    }

    void Update()
    {
        if (Input.GetButtonDown("Fire"))
        {
            if (heldItem != null)
            {
                // we are holding an item

                // check if we are looking at cart
                RaycastHit hit;
                if (Physics.Raycast(
                    Camera.main.ScreenPointToRay(Input.mousePosition),
                    out hit,
                    interactionRange,
                    interactLayerMask,
                    QueryTriggerInteraction.Ignore
                ))
                {
                    // Debug.Log("raycast hit transform " + hit.transform);
                    Cart cart = hit.transform.GetComponentInParent<Cart>();
                    if (cart != null)
                    {
                        Stow(heldItem, cart);
                    }
                    else
                    {
                        // otherwise drop item
                        Drop(heldItem);
                    }
                }
                else
                {
                    // otherwise drop item
                    Drop(heldItem);
                }

            }
            else
            {
                // we are not holding an item

                RaycastHit hit;
                if (Physics.Raycast(
                    Camera.main.ScreenPointToRay(Input.mousePosition),
                    out hit,
                    interactionRange,
                    interactLayerMask,
                    QueryTriggerInteraction.Ignore
                ))
                {
                    Item item = hit.transform.GetComponentInParent<Item>();
                    if (item != null)
                    {
                        Grab(item);
                    }
                }
            }
        }
    }

    public void Grab(Item item)
    {
        // Debug.Log("grabbed " + item.itemData.displayName);
        item.Grab();
        item.transform.SetParent(itemSocket);
        item.transform.localPosition = Vector3.zero;
        item.transform.localRotation = Quaternion.identity;
        heldItem = item;
    }

    public void Drop(Item item)
    {
        // Debug.Log("dropped " + item.itemData.displayName);
        item.Drop();
        item.transform.SetParent(null);
        heldItem = null;
    }

    public void Stow(Item item, Cart cart)
    {
        // Debug.Log("stowing " + item.itemData.displayName + " in " + cart.name);
        cart.AddItemData(item.itemData);
        item.Stow();
        // item.transform.SetParent(null);
        heldItem = null;
    }
}
