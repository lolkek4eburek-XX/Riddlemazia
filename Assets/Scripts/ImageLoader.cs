using UnityEngine;
using System.IO;

public class ImageLoader : MonoBehaviour
{
    private Texture2D loadedTexture;

    public void LoadImage()
    {
        // ���� � ��������� ��������
        string path = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop), "MyImage.png");

        if (File.Exists(path))
        {
            byte[] fileData = File.ReadAllBytes(path);
            loadedTexture = new Texture2D(2, 2); // �������� ��������� � ����������� ��������
            loadedTexture.LoadImage(fileData); // ��������� ��������

            // ��������� �������� � ������� ���������
            GetComponent<Renderer>().materials[0].mainTexture = loadedTexture;

            // ��������� ���������� ���� ��������
            Color averageColor = GetAverageColor(loadedTexture);
            GetComponent<Renderer>().materials[1].color = averageColor; // ������������� ���������� ���� �� ������� ���������

            Debug.Log("���������� �������� ��������� � ������� ���� ����������!");
        }
        else
        {
            Debug.LogError("���� �� ������ �� ����: " + path);
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