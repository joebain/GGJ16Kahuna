using UnityEngine;

public class Portal : MonoBehaviour {

    public string SceneToLoad;
    
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("on trigger enter");
        Player player = other.GetComponent<Player>();
        if (player != null)
        {
            AreaManager.Get().ChangeArea(SceneToLoad);
        }
    }
}
