using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image healthBarImage;
    public PlayerController playerController; // ������ �� ������ ������

    void Update()
    {
        // ���������� ������ ��������
        float healthPercentage = (float)playerController.health / 5; // ������������, ��� ������������ �������� 5
        healthBarImage.fillAmount = healthPercentage;
    }
}