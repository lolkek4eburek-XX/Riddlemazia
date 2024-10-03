using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image healthBarImage;
    public PlayerController playerController; // Ссылка на скрипт игрока

    void Update()
    {
        // Обновление полосы здоровья
        float healthPercentage = (float)playerController.health / 5; // Предполагаем, что максимальное здоровье 5
        healthBarImage.fillAmount = healthPercentage;
    }
}