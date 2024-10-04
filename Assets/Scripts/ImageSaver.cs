using UnityEngine;
using System.IO;

public class ImageSaver : MonoBehaviour
{
    public Texture2D myTexture;

    public void SaveImage()
    {
        // ������ �������� (��������, �������� 256x256)
        myTexture = new Texture2D(256, 256);

        // ��������� �������� ������ (��������, �������)
        Color fillColor = Color.white;
        for (int y = 0; y < myTexture.height; y++)
        {
            for (int x = 0; x < myTexture.width; x++)
            {
                myTexture.SetPixel(x, y, fillColor);
            }
        }
        myTexture.Apply();

        // ��������� �������� �� ������� ����
        byte[] bytes = myTexture.EncodeToPNG();
        string path = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop), "MyImage.png");
        File.WriteAllBytes(path, bytes);

        Debug.Log("�������� ��������� �� ������� �����: " + path);
    }
}
