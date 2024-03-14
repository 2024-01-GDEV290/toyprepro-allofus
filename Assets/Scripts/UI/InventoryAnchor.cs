using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryAnchor: MonoBehaviour
{
    [SerializeField] GameObject iconPrefab;
    [SerializeField] List<GameObject> icons;
    private PlayerMotor player;
    private void Awake()
    {
        icons = new List<GameObject>();
        player = GameObject.Find("Player").GetComponent<PlayerMotor>();
    }

    public void DisplayPlayerInventory()
    {
        foreach(GameObject icon in icons)
        {
            Destroy(icon);
        }
        foreach(Item item in player.inventory)
        {
            GameObject icon = Instantiate(iconPrefab);
            /*icon.GetComponent<Image>()*/
            icons.Add(icon);

        }
    }
}
