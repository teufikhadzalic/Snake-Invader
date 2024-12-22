using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    AudioSource background;
    AudioSource death;
    AudioSource heal;
    AudioSource damage;
    [SerializeField] AudioClip backgroundSFX;
    [SerializeField] AudioClip deathSFX;
    [SerializeField] AudioClip healSFX;
    [SerializeField] AudioClip damageSFX;
    // Start is called before the first frame update
    void Start()
    {
        background = GameObject.Find("Background").GetComponent<AudioSource>();
        background.clip = backgroundSFX;
        background.Play();
    }

    public void PlayDamage()
    {
        damage = GameObject.Find("Damage").GetComponent<AudioSource>();
        damage.clip = damageSFX;
        damage.Play();
    }

    public void PlayDeath()
    {
        death = GameObject.Find("Death").GetComponent<AudioSource>();
        death.clip = deathSFX;
        death.Play();
    }

    public void PlayHeal()
    {
        heal = GameObject.Find("Heal").GetComponent<AudioSource>();
        heal.clip = healSFX;
        heal.Play();
    }

    public void StopBackground()
    {
        background.Stop();
    }
}
