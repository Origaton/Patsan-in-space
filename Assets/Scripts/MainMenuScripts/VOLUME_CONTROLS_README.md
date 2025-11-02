# Инструкция по настройке слайдеров громкости

## Обзор
Система управления громкостью состоит из двух основных компонентов:
- **Options.cs** - управляет слайдерами в меню настроек
- **AudioManager.cs** - централизованное управление всеми звуками в игре

## Настройка в Unity Editor

### Шаг 1: Настройка слайдеров в меню Options

1. Откройте сцену с меню настроек (Menu.unity)
2. Найдите объект с компонентом Options (или создайте новый GameObject)
3. Добавьте компонент Options, если его нет
4. В Canvas создайте три UI слайдера:
   - **Слайдер общей громкости** (Master Volume)
   - **Слайдер музыки** (Music Volume)
   - **Слайдер звуковых эффектов** (Sound Effects Volume)

5. Перетащите созданные слайдеры в соответствующие поля компонента Options:
   - `Master Volume Slider` → слайдер общей громкости
   - `Music Volume Slider` → слайдер музыки
   - `Sound Volume Slider` → слайдер звуков

### Шаг 2: Настройка слайдеров

Для каждого слайдера установите следующие параметры:
- **Min Value**: 0
- **Max Value**: 1
- **Whole Numbers**: выключено (unchecked)
- **Value**: 1 (по умолчанию максимальная громкость)

### Шаг 3: Настройка AudioManager

1. Создайте пустой GameObject в сцене и назовите его "AudioManager"
2. Добавьте компонент AudioManager
3. (Опционально) Перетащите существующие AudioSource компоненты в поля:
   - `Music Source` - для музыки
   - `Sound Source` - для звуковых эффектов
   
   Если эти поля оставить пустыми, AudioManager создаст их автоматически.

## Использование в коде

### Воспроизведение музыки:
```csharp
// Воспроизвести фоновую музыку
AudioManager.Instance.PlayMusic(musicClip);

// Воспроизвести музыку без зацикливания
AudioManager.Instance.PlayMusic(musicClip, false);

// Остановить музыку
AudioManager.Instance.StopMusic();

// Пауза/возобновление музыки
AudioManager.Instance.PauseMusic();
AudioManager.Instance.UnpauseMusic();
```

### Воспроизведение звуковых эффектов:
```csharp
// Простое воспроизведение звука
AudioManager.Instance.PlaySound(soundClip);

// Воспроизведение с настройкой громкости (0.0 - 1.0)
AudioManager.Instance.PlaySound(soundClip, 0.5f);

// Воспроизведение звука в определенной точке мира
AudioManager.Instance.PlaySoundAtPoint(soundClip, transform.position);
```

### Получение текущих значений громкости:
```csharp
float masterVol = Options.GetMasterVolume();
float musicVol = Options.GetMusicVolume();
float soundVol = Options.GetSoundVolume();
```

## Как это работает

### Общая громкость (Master Volume)
- Управляет глобальной громкостью через `AudioListener.volume`
- Влияет на ВСЕ звуки в игре

### Громкость музыки (Music Volume)
- Управляет громкостью AudioSource объектов с тегом "Music" или содержащих "Music" в имени
- Используйте AudioManager.Instance.PlayMusic() для автоматического применения настроек

### Громкость звуков (Sound Volume)
- Управляет громкостью AudioSource объектов с тегом "Sound" или содержащих "Sound" в имени
- Используйте AudioManager.Instance.PlaySound() для автоматического применения настроек

## Теги Unity

Для правильной работы системы добавьте теги в Unity:
1. Откройте Tags & Layers (Edit → Project Settings → Tags & Layers)
2. Добавьте два новых тега:
   - **Music**
   - **Sound**

3. Назначьте соответствующие теги объектам с AudioSource:
   - Музыкальным источникам → тег "Music"
   - Звуковым эффектам → тег "Sound"

## Сохранение настроек

Все настройки громкости автоматически сохраняются в PlayerPrefs:
- `MasterVolume` - общая громкость
- `MusicVolume` - громкость музыки
- `SoundVolume` - громкость звуков

Настройки загружаются автоматически при запуске игры.

## Пример структуры UI

```
Canvas
├── Options Panel
│   ├── Master Volume
│   │   ├── Label (Text: "Общая громкость")
│   │   └── Slider (Master Volume Slider)
│   ├── Music Volume
│   │   ├── Label (Text: "Громкость музыки")
│   │   └── Slider (Music Volume Slider)
│   └── Sound Volume
│       ├── Label (Text: "Громкость звуков")
│       └── Slider (Sound Volume Slider)
```

## Дополнительные возможности

### Применение настроек громкости к существующим AudioSource
```csharp
AudioManager.Instance.ApplyVolumeSettings();
```

### Прямое управление громкостью из кода
```csharp
AudioManager.Instance.SetMusicVolume(0.8f);
AudioManager.Instance.SetSoundVolume(0.6f);
```

## Устранение проблем

**Слайдеры не работают:**
- Убедитесь, что слайдеры назначены в компоненте Options
- Проверьте, что Min/Max значения установлены правильно (0 и 1)

**Музыка не реагирует на слайдер:**
- Убедитесь, что AudioSource имеет тег "Music"
- Используйте AudioManager.Instance.PlayMusic() вместо прямого вызова source.Play()

**Звуки не реагируют на слайдер:**
- Убедитесь, что AudioSource имеет тег "Sound"
- Используйте AudioManager.Instance.PlaySound() для воспроизведения звуков

**AudioManager не найден:**
- Убедитесь, что GameObject с AudioManager присутствует в первой загружаемой сцене
- AudioManager автоматически сохраняется между сценами (DontDestroyOnLoad)
