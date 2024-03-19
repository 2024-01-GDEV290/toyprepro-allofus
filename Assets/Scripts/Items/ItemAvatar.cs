using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAvatar : MonoBehaviour
{
    public Item item;

    public Item Collect(Transform newParentTransform = null)
    {
        if (newParentTransform != null)
        {
            GetComponent<Rigidbody>().isKinematic = true;
            transform.SetParent(newParentTransform, false);
            transform.localPosition = Vector3.zero;
        }
        else
        {
            Destroy(gameObject);
            Destroy(this);
        }
        return item;
    }
}
