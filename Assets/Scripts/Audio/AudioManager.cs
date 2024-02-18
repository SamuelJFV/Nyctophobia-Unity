using UnityEngine.Audio;
using System.Collections.Generic;
using UnityEngine;

public struct NamedImage 
{
    public string name;
    public AudioClip clip;
}
public class AudioManager : MonoBehaviour
{
    #region Singleton
    public static AudioManager Instance { get; private set; }
    private void Awake() 
    {        
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        } 
    }
    #endregion
    [SerializeField] GameObject[] AudioSources;
    [SerializeField] AudioClip[] clips;
    [SerializeField] AudioSource game;

    public void PlaySound(int id)
    {
        game.clip = clips[id];
        game.PlayOneShot(clips[id]);
    }
}
