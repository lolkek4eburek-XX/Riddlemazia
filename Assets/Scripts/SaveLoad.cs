using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveLoad : MonoBehaviour
{
    private const string SceneIndexKey = "SceneIndex";
    private const string MusicVolumeKey = "MusicVolume";

    // Публичное поле для AudioSource, которое можно задать в инспекторе
    public AudioSource musicSource;

    // Метод для сохранения данных
    public void Save()
    {
        // Сохранение индекса текущей сцены
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        PlayerPrefs.SetInt(SceneIndexKey, sceneIndex);

        // Сохранение громкости музыки
        if (musicSource != null)
        {
            float musicVolume = musicSource.volume;
            PlayerPrefs.SetFloat(MusicVolumeKey, musicVolume);
        }
        else
        {
            Debug.LogWarning("AudioSource не задан.");
        }

        // Сохранение данных на диск
        PlayerPrefs.Save();

        Debug.Log("Данные сохранены: Сцена " + sceneIndex + ", Громкость музыки " + (musicSource != null ? musicSource.volume.ToString() : "N/A"));
    }

    // Метод для загрузки данных
    public void Load()
    {
        // Загрузка индекса сцены
        if (PlayerPrefs.HasKey(SceneIndexKey))
        {
            int sceneIndex = PlayerPrefs.GetInt(SceneIndexKey);
            SceneManager.LoadScene(sceneIndex);
            Debug.Log("Сцена загружена: " + sceneIndex);
        }
        else
        {
            Debug.LogWarning("Сохраненный индекс сцены не найден.");
        }

        // Загрузка громкости музыки
        if (PlayerPrefs.HasKey(MusicVolumeKey))
        {
            if (musicSource != null)
            {
                musicSource.volume = PlayerPrefs.GetFloat(MusicVolumeKey);
                Debug.Log("Громкость музыки загружена: " + PlayerPrefs.GetFloat(MusicVolumeKey));
            }
            else
            {
                Debug.LogWarning("AudioSource не задан.");
            }
        }
        else
        {
            Debug.LogWarning("Сохраненная громкость музыки не найдена.");
        }
    }
}