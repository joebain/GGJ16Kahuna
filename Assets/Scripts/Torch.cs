using UnityEngine;

public class Torch : MonoBehaviour
{
    private bool PlayerNear = false;
    protected bool Active = false;
	public static int phase = 0;

    private Player player;

    private ParticleSystem particles;
    private new Light light;
    private Material glow;
    private MeshRenderer meshRenderer;

    private Material[] originalMaterials;
    private Material[] glowMaterials;
	bool shownFireMessage = false;
	private Torch[] torches = new Torch[3];

    void Start()
    {
        particles = transform.Find("Particles").GetComponent<ParticleSystem>();
        light = transform.Find("Light").GetComponent<Light>();
        glow = transform.Find("Glow").GetComponent<MeshRenderer>().material;
        meshRenderer = transform.Find("Model").GetComponent<MeshRenderer>();
        originalMaterials = (Material[])meshRenderer.materials;
		glowMaterials = new Material[originalMaterials.Length];
		player = GameObject.Find("Player").GetComponent<Player>();
        for (int m = 0; m < originalMaterials.Length; m++)
        {
            glowMaterials[m] = glow;
        }
        TurnOn();
		torches[0] = GameObject.Find("Torch0").GetComponent<Torch>();
		torches[1] = GameObject.Find("Torch1").GetComponent<Torch>();
		torches[2] = GameObject.Find("Torch2").GetComponent<Torch>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (player != null)
        {
            PlayerNear = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
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

				if (phase == 1) 
					player.ShowTextBox("If I light them all first, then I can put them out in the right order.");
                    
                if (phase == 0)
				{
					switch (name)
					{
					default: break;

					case "Torch0":
						{
							if (!torches[1].Active && torches[2].Active)
								TurnOff();
							else
								player.ShowTextBox("Oh no, I need to douse the middle torch first.", true);
                                
						} break;
					case "Torch1":
						{
							if (torches[0].Active && torches[2].Active)
								TurnOff();
							//else
								//player.ShowTextBox("Oh no, I need to douse the middle torch first.");
						} break;
					case "Torch2":
						{
							if (!torches[0].Active && !torches[1].Active)
							{
								TurnOff();
								player.ShowTextBox("There. Everyone will be safe now.");
                                    player.log.actions.Add("torches_doused");
                                    phase = 1;
							}
							else
								player.ShowTextBox("This is not the order in which the torches should be doused.", true);
                            } break;
					}
				}

				if (phase == 2)
				{
					switch (name)
					{
					default: break;

					case "Torch0":
						{
							if (!torches[1].Active && torches[2].Active)
								TurnOff();
							else
								player.ShowTextBox("I need to douse the middle torch first.", true);
                            } break;
					case "Torch1":
						{
							if (torches[0].Active && torches[2].Active)
								TurnOff();
							//else
							//player.ShowTextBox("Oh no, I need to douse the middle torch first.");
						} break;
					case "Torch2":
						{
							if (!torches[0].Active && !torches[1].Active)
							{
								TurnOff();
								player.ShowTextBox("OK. I think I put them out properly that time.");
                                    player.log.actions.Add("torches_doused2");
                                    phase = 3;
							}
							else
								player.ShowTextBox("It goes middle, right, left. Then I can leave.", true);
                            } break;
					}
				}
            } 
			else
            {
				TurnOn();
				if (Random.Range(0.0f,1.0f) < 0.2f && !shownFireMessage)
				{
					player.ShowTextBox("It's dangerous and dirty, but sometimes I need the light.");
					shownFireMessage = true;
				}
				if (phase == 1 && torches[0].Active && torches[1].Active && torches[2].Active)
				{
					player.log.actions.Add("torches_relit");
					phase = 2;
				}
					
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
        player.sfx.PlayPositive();
        Active = false;
    }

    private void TurnOn()
    {
        ParticleSystem.EmissionModule emission = particles.emission;
        emission.enabled = true;
        Active = true;
		light.gameObject.SetActive(true);
        player.sfx.PlayPositive();
    }
}
