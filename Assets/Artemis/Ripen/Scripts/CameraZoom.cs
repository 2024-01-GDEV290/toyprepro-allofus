using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public float baseSize;
    public float zoomSize;

    public void ToStart()
    {
        this.transform.position = new Vector3(0f, 0f, -10f);
        GetComponent<Camera>().orthographicSize = baseSize;
    }

    public void Zoom(GameObject go)
    {
        this.transform.position = new Vector3(go.transform.position.x, go.transform.position.y, -10f);
        GetComponent <Camera>().orthographicSize = zoomSize;
    }
}
