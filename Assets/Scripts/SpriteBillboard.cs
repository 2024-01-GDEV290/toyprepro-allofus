using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;

public class SpriteBillboard : MonoBehaviour
{
    [SerializeField] bool freezeXZAxis = true;
    SpriteRenderer spriteRenderer;
    Transform parent;

    private void Awake()
    {
        parent = transform.parent;
        spriteRenderer = GetComponent<SpriteRenderer>();
        Debug.Log(spriteRenderer.sprite);
    }
    void LateUpdate()
    {
        if (freezeXZAxis)
        {
            transform.rotation = Quaternion.Euler(0f, Camera.main.transform.rotation.eulerAngles.y, 0f);
        }
        else
        {
            transform.rotation = Camera.main.transform.rotation;
        }
        GameObject player = GameObject.Find("Player");
        Vector2 mainCam2DPos = new Vector2(Camera.main.transform.position.x,Camera.main.transform.position.z); 
        Vector2 parentForward = new Vector2(parent.transform.forward.x,parent.transform.forward.z);
       /* Debug.Log(Vector2.Angle(mainCam2DPos,parentForward));*/
    }
}
