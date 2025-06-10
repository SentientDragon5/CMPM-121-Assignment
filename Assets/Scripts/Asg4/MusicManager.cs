using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance { get; private set; }

    private AudioSource audioSource;
    private Coroutine fadeCoroutine;

    [Header("Music Tracks")]
    public AudioClip easyMusic;
    public AudioClip mediumMusic;
    public AudioClip endlessMusic;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(this.gameObject);

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            Debug.LogError("MusicManager: No AudioSource found!");
    }

    // Plays music based on the selected difficulty name.

    //The difficulty level ("Easy", "Medium", "Endless").
    public void PlayMusicForDifficulty(string difficulty)
    {
        if (audioSource == null)
            return;

        AudioClip selectedClip = null;

        switch (difficulty.ToLower())
        {
            case "easy":
                selectedClip = easyMusic;
                break;
            case "medium":
                selectedClip = mediumMusic;
                break;
            case "endless":
                selectedClip = endlessMusic;
                break;
            default:
                selectedClip = easyMusic;
                Debug.LogWarning($"MusicManager: Dynamically added difficulty '{difficulty}' will use easy mode music.");
                break;
        }

        if (selectedClip != null)
        {
            if (fadeCoroutine != null)
                StopCoroutine(fadeCoroutine);

            fadeCoroutine = StartCoroutine(FadeInMusic(selectedClip, 7f)); // 7 seconds fade-in
        }
    }


    private IEnumerator FadeInMusic(AudioClip clip, float duration)
    {
        audioSource.Stop();
        audioSource.clip = clip;
        audioSource.volume = 0f;
        audioSource.loop = true;
        audioSource.Play();

        float elapsed = 0f;
        float targetVolume = 0.7f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            audioSource.volume = Mathf.Clamp01(elapsed / duration) * targetVolume;
            yield return null;
        }

        audioSource.volume = targetVolume;
    }

    public void StopMusic()
    {
        if (audioSource != null)
            audioSource.Stop();
    }
}
