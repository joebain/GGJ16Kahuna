using UnityEngine;
using System.Collections;

public class ClickControl : MonoBehaviour {

    public Player Player;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hitInfo = new RaycastHit();
            LayerMask clickableLayer = LayerMask.NameToLayer("Clickable");
            bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);//, clickableLayer);
            if (hit)
            {
                Item item = hitInfo.transform.GetComponent<Item>();
                if (item != null)
                {
                    if (item.CanPickup())
                    {
                        item.Pickup(Player.gameObject);
                        Player.Inventory.AddItem(item);
                    }
                } else
                {
                    Debug.Log("clicked on: " + hitInfo.collider.name);
                    Player.GoTo(hitInfo.point);
                }

            }
        }
    }
}
