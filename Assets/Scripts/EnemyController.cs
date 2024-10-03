using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class EnemyController : MonoBehaviour
{
    public Transform player; // Ссылка на игрока
    public float moveSpeed = 5f; // Скорость перемещения врага
    public float[] stoppingDistance; // Расстояние, на котором враг прекращает преследование
    public int attackDamage = 1; // Урон, который враг наносит игроку
    public float attackRate = 1f; // Частота атаки
    private float lastAttackTime;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();  // Получение компонента Animator
    }
 
    void Update()
    {
        if (player != null)
        {
            // Получаем вектор направления к игроку
            Vector3 direction = (player.position - transform.position).normalized;

            // Рассчитываем расстояние между врагом и игроком
            float distance = Vector3.Distance(player.position, transform.position);

            // Если враг достигает дистанции атаки
            if (distance <= stoppingDistance[1])
            {
                // Проверяем, пришло ли время для атаки
                if (Time.time > lastAttackTime + attackRate)
                {
                    Attack();
                    lastAttackTime = Time.time; // Обновляем время последней атаки
                }
            }
            // Если враг слишком далеко, он перемещается к игроку
            else if (distance < stoppingDistance[0])
            {
                // Плавно перемещаем врага в сторону игрока
                transform.position = Vector3.Lerp(transform.position, player.position, moveSpeed * Time.deltaTime);
            }
        }
    }

    void Attack()
    {
        // Уменьшаем здоровье игрока
        PlayerController playerController = player.GetComponent<PlayerController>();
        if (playerController != null)
        {
            playerController.health -= attackDamage;
            Debug.Log("Player attacked! Current health: " + playerController.health);

            animator.SetTrigger("udarTrigger");
        }
    }
}
