using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    [Header("Volume Sliders")]
    [SerializeField] private Slider masterVolumeSlider;
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider soundVolumeSlider;

    private float masterVolume = 1f;
    private float musicVolume = 1f;
    private float soundVolume = 1f;

    void Start()
    {
        // Загружаем сохраненные значения громкости (по умолчанию 1)
        masterVolume = PlayerPrefs.GetFloat("MasterVolume", 1f);
        musicVolume = PlayerPrefs.GetFloat("MusicVolume", 1f);
        soundVolume = PlayerPrefs.GetFloat("SoundVolume", 1f);
        
        // Устанавливаем общую громкость через AudioListener
        AudioListener.volume = masterVolume;
        
        // Инициализируем слайдеры
        InitializeSlider(masterVolumeSlider, masterVolume, SetMasterVolume);
        InitializeSlider(musicVolumeSlider, musicVolume, SetMusicVolume);
        InitializeSlider(soundVolumeSlider, soundVolume, SetSoundVolume);
    }

    private void InitializeSlider(Slider slider, float initialValue, UnityEngine.Events.UnityAction<float> callback)
    {
        if (slider != null)
        {
            slider.value = initialValue;
            slider.onValueChanged.AddListener(callback);
        }
    }

    // Метод для изменения общей громкости
    public void SetMasterVolume(float level)
    {
        masterVolume = level;
        AudioListener.volume = masterVolume;
        
        // Сохраняем значение
        PlayerPrefs.SetFloat("MasterVolume", masterVolume);
        PlayerPrefs.Save();
    }

    // Метод для изменения громкости музыки
    public void SetMusicVolume(float level)
    {
        musicVolume = level;
        
        // Обновляем громкость всех источников музыки
        UpdateMusicVolume();
        
        // Сохраняем значение
        PlayerPrefs.SetFloat("MusicVolume", musicVolume);
        PlayerPrefs.Save();
    }

    // Метод для изменения громкости звуковых эффектов
    public void SetSoundVolume(float level)
    {
        soundVolume = level;
        
        // Обновляем громкость всех источников звуковых эффектов
        UpdateSoundVolume();
        
        // Сохраняем значение
        PlayerPrefs.SetFloat("SoundVolume", soundVolume);
        PlayerPrefs.Save();
    }

    // Обновляем громкость всех источников музыки
    private void UpdateMusicVolume()
    {
        AudioSource[] audioSources = FindObjectsByType<AudioSource>(FindObjectsSortMode.None);
        foreach (AudioSource source in audioSources)
        {
            // Проверяем, является ли источник музыкой (например, по тегу или имени)
            if (source.gameObject.CompareTag("Music") || source.gameObject.name.Contains("Music"))
            {
                source.volume = musicVolume;
            }
        }
    }

    // Обновляем громкость всех источников звуковых эффектов
    private void UpdateSoundVolume()
    {
        AudioSource[] audioSources = FindObjectsByType<AudioSource>(FindObjectsSortMode.None);
        foreach (AudioSource source in audioSources)
        {
            // Проверяем, является ли источник звуковым эффектом
            if (source.gameObject.CompareTag("Sound") || source.gameObject.name.Contains("Sound"))
            {
                source.volume = soundVolume;
            }
        }
    }

    // Получить текущие значения громкости (для использования в других скриптах)
    public static float GetMasterVolume()
    {
        return PlayerPrefs.GetFloat("MasterVolume", 1f);
    }

    public static float GetMusicVolume()
    {
        return PlayerPrefs.GetFloat("MusicVolume", 1f);
    }

    public static float GetSoundVolume()
    {
        return PlayerPrefs.GetFloat("SoundVolume", 1f);
    }

    void OnDestroy()
    {
        // Отписываемся от событий при уничтожении объекта
        if (masterVolumeSlider != null)
            masterVolumeSlider.onValueChanged.RemoveListener(SetMasterVolume);
        
        if (musicVolumeSlider != null)
            musicVolumeSlider.onValueChanged.RemoveListener(SetMusicVolume);
        
        if (soundVolumeSlider != null)
            soundVolumeSlider.onValueChanged.RemoveListener(SetSoundVolume);
    }
}
