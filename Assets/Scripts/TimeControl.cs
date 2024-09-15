using UnityEngine;

public class TimeControl : MonoBehaviour
{
    public Transform raycastOrigin;
    public float slowSpeed = 0.5f;
    public float normalSpeed = 1.0f;
    public float reverseSpeed = -1.0f;
    public float stopSpeed = 0.0f;

    void Update()
    {
        bool hasRaycastTarget = Physics.Raycast(raycastOrigin.position, raycastOrigin.forward, out RaycastHit hit, 100f);

        if (hasRaycastTarget)
        {
            EnemyController enemyController = hit.collider.GetComponent<EnemyController>();
            Animator animator = hit.collider.GetComponent<Animator>();
            if (animator != null || enemyController != null)
            {
                if (hit.collider.TryGetComponent(out EnemyController enemy_controller))
                {
                    if (Input.GetMouseButtonDown(0) && Input.GetMouseButtonDown(1)) // ��� + ���
                    {
                        ChangeEnemySpeed(enemyController, normalSpeed);
                    }
                    else if (Input.GetMouseButtonDown(0)) // ���
                    {
                        ChangeEnemySpeed(enemyController, slowSpeed);
                    }
                    else if (Input.GetMouseButtonDown(1)) // ���
                    {
                        ChangeEnemySpeed(enemyController, reverseSpeed);
                    }
                    else if (Input.GetMouseButtonDown(2)) // ���
                    {
                        ChangeEnemySpeed(enemyController, stopSpeed);
                    }
                }
                if (hit.collider.TryGetComponent(out Animator aniMator))
                {
                    if (Input.GetMouseButtonDown(0) && Input.GetMouseButtonDown(1)) // ��� + ���
                    {
                        ChangeAnimationSpeed(animator, normalSpeed); // ������������� �������� �������� � 1
                    }
                    else if (Input.GetMouseButtonDown(0)) // ���
                    {
                        ChangeAnimationSpeed(animator, slowSpeed); // ������������� �������� �������� � 0.5
                    }
                    else if (Input.GetMouseButtonDown(1)) // ���
                    {
                        ChangeAnimationSpeed(animator, reverseSpeed); // ������������� �������� �������� � -1
                    }
                    else if (Input.GetMouseButtonDown(2)) // ���
                    {
                        ChangeAnimationSpeed(animator, stopSpeed); // ������������� �������� �������� � 0
                    }
                }
            }
        }
    }

    public void ChangeAnimationSpeed(Animator anim, float speed)
    {
        anim.SetFloat("AnimSpeed", speed);//m_Speed
    }
    public void ChangeEnemySpeed(EnemyController enemyController, float speed)
    {
        enemyController.moveSpeed = speed * 3; // ����������� �������� ����������� �����
    }
}