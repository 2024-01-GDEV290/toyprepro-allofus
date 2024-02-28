using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEditor.ShaderGraph.Drawing;
using UnityEngine;

public class SpriteBillboard : MonoBehaviour
{
    [SerializeField] bool freezeXZAxis = true;
    [SerializeField] Sprite front;
    [SerializeField] Sprite back;
    [SerializeField] Sprite left;
    [SerializeField] Sprite right;
    SpriteRenderer spriteRenderer;
    Transform parent;

    private void Awake()
    {

        parent = transform.parent;
        spriteRenderer = GetComponent<SpriteRenderer>();
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
        Vector3 parentForward = parent.transform.forward;
        Vector3 parentLeft = Quaternion.Euler(0, 90, 0) * parent.transform.forward;
        float northSouth = Vector3.Angle(Camera.main.transform.position,parentForward);
        float eastWest = Vector3.Angle(parentLeft, Camera.main.transform.position);
        Debug.DrawRay(transform.position, parentLeft, Color.red) ;
        Debug.DrawRay(transform.position, parentForward, Color.blue);
        Debug.Log($"Deg from sprite forward: {northSouth}");
        Debug.Log($"Deg from sprite right: {eastWest}");
        Sprite newSprite = spriteRenderer.sprite;
        if (northSouth > 90)
        {
            if (eastWest < 45)
            {
                newSprite = right;
            } else if (eastWest > 135)
            {
                newSprite = left;
            }else
            {
                newSprite = back;
            }
        }
        else if (northSouth < 90)
        {
            if (eastWest < 45)
            {
                newSprite = right;
            }
            else if (eastWest > 135)
            {
                newSprite = left;
            }
            else
            {
                newSprite = front;
            }
        }
        spriteRenderer.sprite = newSprite;
        /*        if (northSouth < 90)
                {
                    spriteRenderer.sprite = front;
                }
                else
                {
                    spriteRenderer.sprite = back;
                }*/
        /* Debug.Log(Vector2.Angle(mainCam2DPos,parentForward));*/

    }
}
