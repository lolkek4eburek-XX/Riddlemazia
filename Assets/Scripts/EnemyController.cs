using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform player; // ������ �� ������
    public float moveSpeed = 5f; // �������� ����������� �����
    public float[] stoppingDistance; // ����������, �� ������� ���� ���������� �������������

    void Update()
    {
        if (player != null)
        {
            // �������� ������ ����������� � ������
            Vector3 direction = (player.position - transform.position).normalized;

            // ������������ ���������� ����� ������ � �������
            float distance = Vector3.Distance(player.position, transform.position);

            // ���� ���� ��� �� ������ ������
            if (distance < stoppingDistance[0] && distance > stoppingDistance[1])
            {
                // ������ ���������� ����� � ������� ������
                transform.position = Vector3.Lerp(transform.position, player.position, moveSpeed * Time.deltaTime);
            }
        }
    }
}