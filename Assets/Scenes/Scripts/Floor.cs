using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;

public class Floor : MonoBehaviour
{
    public State state;
    public Material myMat;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {  
        if(myMat == state.wood && other.CompareTag("Player"))
        {
            state.chargeValue = 200;
        }
        if (myMat == state.carpet && other.CompareTag("Player"))
        {
            state.chargeValue = 500;
        }
        if (myMat == state.leather && other.CompareTag("Player"))
        {
            state.chargeValue = 50;
        }
    }
}
