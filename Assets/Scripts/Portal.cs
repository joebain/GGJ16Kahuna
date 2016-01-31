using UnityEngine;

public class Portal : MonoBehaviour {

    public string SceneToLoad;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("portal trigger " + other.name);
        Player player = other.GetComponent<Player>();
        if (player != null)
        {
            AreaManager.Get().ChangeArea(SceneToLoad);
        }
    }
}
