using System.Collections;
using System.Collections.Generic;
using JW;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public static ItemManager Instance { get; private set; }

    public struct ItemType
    {
        public string name;
        public int value;
    }

    public struct ItemMaterial
    {
        public string name;
        public string type;
        public int value;
    }

    public struct ItemAffix
    {
        public string name;
        // public int value;
    }


    public List<ItemType> itemTypes;
    public List<ItemMaterial> itemMaterials;
    public List<ItemAffix> itemAffixes;

    private void Awake()
    {
        #region Initialize Singleton Instance
        if (Instance != null)
        {
            Debug.LogError("Duplicate Singleton: " + Instance + " and " + this);
        }
        Instance = this;
        #endregion

        itemTypes = new List<ItemType>();
        itemMaterials = new List<ItemMaterial>();
        itemAffixes = new List<ItemAffix>();

        readItemTypes();
        readItemMaterials();
        readItemAffixes();
        // DebugLogItemTypes();
        // for (int i = 0; i < 10; i++)
        // {
        //     Debug.Log(GenerateItemName());
        // }
    }

    public void DebugLogItemTypes()
    {
        string output = "";
        foreach (var item in itemTypes)
        {
            output += item.name + ", " + item.value + "\n";
        }
        Debug.Log(output);
    }

    // read materials
    public void readItemMaterials()
    {
        var dataset = Resources.Load<TextAsset>("Data/Items/item-materials");
        var dataLines = dataset.text.Split('\n');
        // Debug.Log("file: " + dataset.text);
        // Debug.Log("lines: " + dataLines);

        for (int i = 1; i < dataLines.Length; i++)
        {
            // Debug.Log(dataLines[i]);
            var data = dataLines[i].Split(',');
            ItemMaterial itemMaterial = new ItemMaterial
            {
                name = data[0].Trim(),
                type = data[1].Trim(),
                value = int.Parse(data[2])
            };
            itemMaterials.Add(itemMaterial);
        }
    }

    // read item types
    public void readItemTypes()
    {
        var dataset = Resources.Load<TextAsset>("Data/Items/item-types");
        var dataLines = dataset.text.Split('\n');
        // Debug.Log("file: " + dataset.text);
        // Debug.Log("lines: " + dataLines);

        for (int i = 1; i < dataLines.Length; i++)
        {
            // Debug.Log(dataLines[i]);
            var data = dataLines[i].Split(',');
            ItemType itemType = new ItemType
            {
                name = data[0].Trim(),
                value = int.Parse(data[1])
            };
            itemTypes.Add(itemType);
        }
    }

    // read affixes
    public void readItemAffixes()
    {
        var dataset = Resources.Load<TextAsset>("Data/Items/item-affixes");
        var dataLines = dataset.text.Split('\n');
        // Debug.Log("file: " + dataset.text);
        // Debug.Log("lines: " + dataLines);

        for (int i = 1; i < dataLines.Length; i++)
        {
            // Debug.Log(dataLines[i]);
            var data = dataLines[i].Split(',');
            ItemAffix itemAffix = new ItemAffix
            {
                name = data[0].Trim(),
                // value = int.Parse(data[1])
            };
            itemAffixes.Add(itemAffix);
        }
    }

    public static ItemData GenerateItemData()
    {
        ItemMaterial mat = GameUtil.ChooseRandom<ItemMaterial>(Instance.itemMaterials);
        ItemType type = GameUtil.ChooseRandom<ItemType>(Instance.itemTypes);
        ItemAffix affix = GameUtil.ChooseRandom<ItemAffix>(Instance.itemAffixes);
        return new ItemData
        {
            displayName = (mat.name + " " +
                        type.name + " of " +
                        affix.name).Trim(),
            value = mat.value + type.value * Random.Range(100, 500)
        };
    }
    // create new itemData
    // spawn new item






}
