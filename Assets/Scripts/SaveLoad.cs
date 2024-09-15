using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveLoad : MonoBehaviour
{
    private const string SceneIndexKey = "SceneIndex";
    private const string MusicVolumeKey = "MusicVolume";

    // ��������� ���� ��� AudioSource, ������� ����� ������ � ����������
    public AudioSource musicSource;

    // ����� ��� ���������� ������
    public void Save()
    {
        // ���������� ������� ������� �����
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        PlayerPrefs.SetInt(SceneIndexKey, sceneIndex);

        // ���������� ��������� ������
        if (musicSource != null)
        {
            float musicVolume = musicSource.volume;
            PlayerPrefs.SetFloat(MusicVolumeKey, musicVolume);
        }
        else
        {
            Debug.LogWarning("AudioSource �� �����.");
        }

        // ���������� ������ �� ����
        PlayerPrefs.Save();

        Debug.Log("������ ���������: ����� " + sceneIndex + ", ��������� ������ " + (musicSource != null ? musicSource.volume.ToString() : "N/A"));
    }

    // ����� ��� �������� ������
    public void Load()
    {
        // �������� ������� �����
        if (PlayerPrefs.HasKey(SceneIndexKey))
        {
            int sceneIndex = PlayerPrefs.GetInt(SceneIndexKey);
            SceneManager.LoadScene(sceneIndex);
            Debug.Log("����� ���������: " + sceneIndex);
        }
        else
        {
            Debug.LogWarning("����������� ������ ����� �� ������.");
        }

        // �������� ��������� ������
        if (PlayerPrefs.HasKey(MusicVolumeKey))
        {
            if (musicSource != null)
            {
                musicSource.volume = PlayerPrefs.GetFloat(MusicVolumeKey);
                Debug.Log("��������� ������ ���������: " + PlayerPrefs.GetFloat(MusicVolumeKey));
            }
            else
            {
                Debug.LogWarning("AudioSource �� �����.");
            }
        }
        else
        {
            Debug.LogWarning("����������� ��������� ������ �� �������.");
        }
    }
}