using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryAnchor: MonoBehaviour
{
    [SerializeField] Image iconPrefab;
    [SerializeField] List<Image> icons = new List<Image>();
    private PlayerMotor player;
    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<PlayerMotor>();
    }

    public void DisplayPlayerInventory()
    {
        foreach(Item item in player.inventory)
        {
            icons.Add(Instantiate(iconPrefab));

        }
    }
}
