using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Range(0f, 1f)]
    public float musicVolume = 1f;
    [Range(0f, 1f)]
    public float sfxVolume = 1f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            SceneManager.sceneLoaded += OnSceneLoaded; // Subscribe to scene change
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // Always clean up event handlers!
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Scene Loaded: " + scene.name);

        // Example: Find all music sources by tag
        GameObject[] musicSources = GameObject.FindGameObjectsWithTag("Music");

        foreach (var obj in musicSources)
        {
            var source = obj.GetComponent<AudioSource>();
            if (source != null)
            {
                source.volume = musicVolume;
                source.loop = true;
                source.Play(); // Optional
            }
        }

        // Example: Find all SFX sources by tag
        GameObject[] sfxSources = GameObject.FindGameObjectsWithTag("SFX");

        foreach (var obj in sfxSources)
        {
            var source = obj.GetComponent<AudioSource>();
            if (source != null)
            {
                source.volume = sfxVolume;
            }
        }
    }

    public void SetMusicVolume(float volume)
    {
        musicVolume = volume;
        UpdateVolumeByTag("Music", musicVolume);
    }

    public void SetSFXVolume(float volume)
    {
        sfxVolume = volume;
        UpdateVolumeByTag("SFX", sfxVolume);
    }

    private void UpdateVolumeByTag(string tag, float volume)
    {
        GameObject[] sources = GameObject.FindGameObjectsWithTag(tag);
        foreach (var obj in sources)
        {
            var source = obj.GetComponent<AudioSource>();
            if (source != null)
            {
                source.volume = volume;
            }
        }
    }
}
