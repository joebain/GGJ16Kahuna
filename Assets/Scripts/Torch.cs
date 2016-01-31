using UnityEngine;

public class Torch : MonoBehaviour
{
    private bool PlayerNear = false;
    private bool Active = false;

    private Player player;

    private ParticleSystem particles;
    private new Light light;
    private Material glow;
    private MeshRenderer meshRenderer;

    private Material[] originalMaterials;
    private Material[] glowMaterials;

    void Start()
    {
        particles = transform.Find("Particles").GetComponent<ParticleSystem>();
        light = transform.Find("Light").GetComponent<Light>();
        glow = transform.Find("Glow").GetComponent<MeshRenderer>().material;
        meshRenderer = transform.Find("Model").GetComponent<MeshRenderer>();
        originalMaterials = (Material[])meshRenderer.materials;
        glowMaterials = new Material[originalMaterials.Length];
        for (int m = 0; m < originalMaterials.Length; m++)
        {
            glowMaterials[m] = glow;
        }
        TurnOn();
    }

    void OnTriggerEnter(Collider other)
    {
        player = other.GetComponent<Player>();
        if (player != null)
        {
            PlayerNear = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        player = other.GetComponent<Player>();
        if (player != null)
        {
            PlayerNear = false;
        }
    }

    void OnMouseDown()
    {
        if (PlayerNear)
        {
            if (Active)
            {
                TurnOff();
            } else
            {
                TurnOn();
            }
        }
    }

    void OnMouseEnter()
    {
        meshRenderer.materials = glowMaterials;
    }

    void OnMouseExit()
    {
        meshRenderer.materials = originalMaterials;
    }

    private void TurnOff()
    {
        ParticleSystem.EmissionModule emission = particles.emission;
        emission.enabled = false;
        light.gameObject.SetActive(false);
        Active = false;
    }

    private void TurnOn()
    {
        ParticleSystem.EmissionModule emission = particles.emission;
        emission.enabled = true;
        Active = true;
        light.gameObject.SetActive(true);
    }
}
