using UnityEngine;

/// <summary>
/// Менеджер аудио для управления музыкой и звуковыми эффектами
/// Используйте теги "Music" и "Sound" для AudioSource объектов
/// или вызывайте методы этого класса для воспроизведения звуков
/// </summary>
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Audio Sources")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource soundSource;

    private void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // Создаем AudioSource компоненты, если они не назначены
        if (musicSource == null)
        {
            musicSource = gameObject.AddComponent<AudioSource>();
            musicSource.loop = true;
            musicSource.playOnAwake = false;
        }

        if (soundSource == null)
        {
            soundSource = gameObject.AddComponent<AudioSource>();
            soundSource.loop = false;
            soundSource.playOnAwake = false;
        }

        // Устанавливаем теги для правильной работы с Options.cs
        musicSource.gameObject.tag = "Music";
        soundSource.gameObject.tag = "Sound";

        // Применяем сохраненные настройки громкости
        ApplyVolumeSettings();
    }

    private void Start()
    {
        // Применяем настройки громкости при старте
        ApplyVolumeSettings();
    }

    /// <summary>
    /// Применить сохраненные настройки громкости
    /// </summary>
    public void ApplyVolumeSettings()
    {
        if (musicSource != null)
        {
            musicSource.volume = Options.GetMusicVolume();
        }

        if (soundSource != null)
        {
            soundSource.volume = Options.GetSoundVolume();
        }
    }

    /// <summary>
    /// Воспроизвести музыку
    /// </summary>
    public void PlayMusic(AudioClip clip, bool loop = true)
    {
        if (musicSource != null && clip != null)
        {
            musicSource.clip = clip;
            musicSource.loop = loop;
            musicSource.volume = Options.GetMusicVolume();
            musicSource.Play();
        }
    }

    /// <summary>
    /// Остановить музыку
    /// </summary>
    public void StopMusic()
    {
        if (musicSource != null)
        {
            musicSource.Stop();
        }
    }

    /// <summary>
    /// Поставить музыку на паузу
    /// </summary>
    public void PauseMusic()
    {
        if (musicSource != null)
        {
            musicSource.Pause();
        }
    }

    /// <summary>
    /// Возобновить музыку
    /// </summary>
    public void UnpauseMusic()
    {
        if (musicSource != null)
        {
            musicSource.UnPause();
        }
    }

    /// <summary>
    /// Воспроизвести звуковой эффект
    /// </summary>
    public void PlaySound(AudioClip clip)
    {
        if (soundSource != null && clip != null)
        {
            soundSource.volume = Options.GetSoundVolume();
            soundSource.PlayOneShot(clip);
        }
    }

    /// <summary>
    /// Воспроизвести звуковой эффект с указанной громкостью
    /// </summary>
    public void PlaySound(AudioClip clip, float volumeScale)
    {
        if (soundSource != null && clip != null)
        {
            soundSource.PlayOneShot(clip, Options.GetSoundVolume() * volumeScale);
        }
    }

    /// <summary>
    /// Воспроизвести звук в определенной точке мира
    /// </summary>
    public void PlaySoundAtPoint(AudioClip clip, Vector3 position)
    {
        if (clip != null)
        {
            AudioSource.PlayClipAtPoint(clip, position, Options.GetSoundVolume());
        }
    }

    /// <summary>
    /// Установить громкость музыки
    /// </summary>
    public void SetMusicVolume(float volume)
    {
        if (musicSource != null)
        {
            musicSource.volume = volume;
        }
    }

    /// <summary>
    /// Установить громкость звуковых эффектов
    /// </summary>
    public void SetSoundVolume(float volume)
    {
        if (soundSource != null)
        {
            soundSource.volume = volume;
        }
    }
}
