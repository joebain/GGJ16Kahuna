using UnityEngine;

using DG.Tweening;

public class Item : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public bool CanPickup()
    {
        return true;
    }

    public void Pickup(GameObject picker)
    {
        transform.DOMove(picker.transform.position, 0.5f).OnComplete(() => gameObject.SetActive(false));
    }
}
