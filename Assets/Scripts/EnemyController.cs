using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class EnemyController : MonoBehaviour
{
    public Transform player; // ������ �� ������
    public float moveSpeed = 5f; // �������� ����������� �����
    public float[] stoppingDistance; // ����������, �� ������� ���� ���������� �������������
    public int attackDamage = 1; // ����, ������� ���� ������� ������
    public float attackRate = 1f; // ������� �����
    private float lastAttackTime;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();  // ��������� ���������� Animator
    }
 
    void Update()
    {
        if (player != null)
        {
            // �������� ������ ����������� � ������
            Vector3 direction = (player.position - transform.position).normalized;

            // ������������ ���������� ����� ������ � �������
            float distance = Vector3.Distance(player.position, transform.position);

            // ���� ���� ��������� ��������� �����
            if (distance <= stoppingDistance[1])
            {
                // ���������, ������ �� ����� ��� �����
                if (Time.time > lastAttackTime + attackRate)
                {
                    Attack();
                    lastAttackTime = Time.time; // ��������� ����� ��������� �����
                }
            }
            // ���� ���� ������� ������, �� ������������ � ������
            else if (distance < stoppingDistance[0])
            {
                // ������ ���������� ����� � ������� ������
                transform.position = Vector3.Lerp(transform.position, player.position, moveSpeed * Time.deltaTime);
            }
        }
    }

    void Attack()
    {
        // ��������� �������� ������
        PlayerController playerController = player.GetComponent<PlayerController>();
        if (playerController != null)
        {
            playerController.health -= attackDamage;
            Debug.Log("Player attacked! Current health: " + playerController.health);

            animator.SetTrigger("udarTrigger");
        }
    }
}
