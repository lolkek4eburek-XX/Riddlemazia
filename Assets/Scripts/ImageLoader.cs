using UnityEngine;
using System.IO;

public class ImageLoader : MonoBehaviour
{
    private Texture2D loadedTexture;

    public void LoadImage()
    {
        // Путь к изменённой картинке
        string path = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop), "MyImage.png");

        if (File.Exists(path))
        {
            byte[] fileData = File.ReadAllBytes(path);
            loadedTexture = new Texture2D(2, 2); // Текстура создается с минимальным размером
            loadedTexture.LoadImage(fileData); // Загружаем картинку

            // Применяем текстуру к первому материалу
            GetComponent<Renderer>().materials[0].mainTexture = loadedTexture;

            // Вычисляем усреднённый цвет текстуры
            Color averageColor = GetAverageColor(loadedTexture);
            GetComponent<Renderer>().materials[1].color = averageColor; // Устанавливаем усреднённый цвет ко второму материалу

            Debug.Log("Измененная текстура загружена и средний цвет установлен!");
        }
        else
        {
            Debug.LogError("Файл не найден по пути: " + path);
        }
    }

    private Color GetAverageColor(Texture2D texture)
    {
        Color[] pixels = texture.GetPixels();
        Color sum = new Color(0, 0, 0, 0);
        foreach (Color pixel in pixels)
        {
            sum += pixel;
        }
        Color average = sum / pixels.Length;
        return average;
    }
}