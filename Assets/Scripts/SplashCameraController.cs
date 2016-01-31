using UnityEngine;
using DG.Tweening;

public class SplashCameraController : MonoBehaviour
{
    public float movement = 5;
    public float duration = 0.5f;

    public GameObject UI;

    void Start()
    {
        UI.SetActive(false);
        transform.DOMoveY(transform.position.y + movement, duration).OnComplete(() => UI.SetActive(true)).SetEase(Ease.OutBounce);
    }
}
