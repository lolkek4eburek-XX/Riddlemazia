using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform player; // —сылка на игрока
    public float moveSpeed = 5f; // —корость перемещени€ врага
    public float[] stoppingDistance; // –ассто€ние, на котором враг прекращает преследование

    void Update()
    {
        if (player != null)
        {
            // ѕолучаем вектор направлени€ к игроку
            Vector3 direction = (player.position - transform.position).normalized;

            // –ассчитываем рассто€ние между врагом и игроком
            float distance = Vector3.Distance(player.position, transform.position);

            // ≈сли враг еще не достиг игрока
            if (distance < stoppingDistance[0] && distance > stoppingDistance[1])
            {
                // ѕлавно перемещаем врага в сторону игрока
                transform.position = Vector3.Lerp(transform.position, player.position, moveSpeed * Time.deltaTime);
            }
        }
    }
}