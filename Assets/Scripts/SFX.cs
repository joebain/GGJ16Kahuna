using UnityEngine;

public class SFX : MonoBehaviour
{

    AudioSource[] hits;

    AudioSource discord, positive;

    int lastHit = -1;

    public void Start()
    {
        hits = new AudioSource[3];
        hits[0] = transform.Find("hit-1").GetComponent<AudioSource>();
        hits[1] = transform.Find("hit-2").GetComponent<AudioSource>();
        hits[2] = transform.Find("hit-3").GetComponent<AudioSource>();

        discord = transform.Find("discord").GetComponent<AudioSource>();
        positive = transform.Find("positive").GetComponent<AudioSource>();
    }

    public void PlayHit()
    {
        int hit = lastHit;
        while (hit == lastHit)
        {
            hit = Mathf.FloorToInt(Random.Range(0, hits.Length));
        }
        hits[hit].Play();
        lastHit = hit;
    }

    public void PlayDiscord()
    {
        discord.Play();
    }

    public void PlayPositive()
    {
        positive.Play();
    }
}
