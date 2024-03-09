using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAvatar : MonoBehaviour
{
    [SerializeField] Item item;

    void Collect()
    {
        /*Emit event to add item SO to player inventory*/
        Destroy(this);
    }
}
