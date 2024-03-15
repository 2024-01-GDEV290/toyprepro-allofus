using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAvatar : MonoBehaviour
{
    public Item item;

    public Item Collect(Transform newParentTransform = null)
    {
        /*        Debug.Log($"Collected {item.itemName}");
                Destroy(gameObject);
                Destroy(this);
        */
        if (newParentTransform != null)
        {
            GetComponent<Rigidbody>().isKinematic = true;
            transform.SetParent(newParentTransform, false);
            transform.localPosition = Vector3.zero;
        }

        return item;
    }
}
