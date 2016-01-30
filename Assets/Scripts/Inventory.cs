
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    public void AddItem(Item item)
    {
        item.transform.parent = transform;
    }
}
