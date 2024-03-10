using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAvatar : MonoBehaviour
{
    public Item item;

    public Item Collect()
    {
        Debug.Log($"Collected {item.itemName}");
        Destroy(gameObject);
        Destroy(this);
        return item;
    }
}
