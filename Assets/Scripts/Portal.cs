using UnityEngine;

public class Portal : MonoBehaviour {

    public string SceneToLoad;
    
    void OnTriggerEnter(Collider collider)
    {
        Debug.Log("on trigger enter");
        Player player = collider.GetComponent<Player>();
        if (player != null)
        {
            AreaManager.Get().ChangeArea(SceneToLoad);
        }
    }
}
