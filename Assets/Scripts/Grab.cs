using UnityEngine;

public class Grab : MonoBehaviour
{
    public float grabDistance = 5f; // ������������ ��������� �������
    public float throwForce = 10f; // ���� ������
    public Transform holdPoint; // �����, ��� ����� ������������ ������
    private GameObject grabbedObject = null; // ����������� ������
    private Rigidbody grabbedRigidbody = null; // Rigidbody ������������ �������

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) // ������� E ��� �������/������
        {
            if (grabbedObject == null)
            {
                TryGrabObject();
            }
            else
            {
                ThrowObject();
            }
        }

        if (grabbedObject != null)
        {
            MoveObject();
        }
    }

    void TryGrabObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, grabDistance))
        {
            if (hit.collider.gameObject.CompareTag("Grabbable")) // �������� �� ��� "Grabbable"
            {
                grabbedObject = hit.collider.gameObject;
                grabbedRigidbody = grabbedObject.GetComponent<Rigidbody>();

                if (grabbedRigidbody != null)
                {
                    grabbedRigidbody.useGravity = false;
                    grabbedRigidbody.velocity = Vector3.zero;
                    grabbedRigidbody.angularVelocity = Vector3.zero;
                }
            }
        }
    }

    void MoveObject()
    {
        if (grabbedRigidbody != null)
        {
            Vector3 directionToHoldPoint = holdPoint.position - grabbedObject.transform.position;
            grabbedRigidbody.velocity = directionToHoldPoint * 10f; // �������� ����������� ������� � ����� ���������
        }
    }

    void ThrowObject()
    {
        if (grabbedRigidbody != null)
        {
            grabbedRigidbody.useGravity = true;
            grabbedRigidbody.AddForce(Camera.main.transform.forward * throwForce, ForceMode.VelocityChange);
        }

        grabbedObject = null;
        grabbedRigidbody = null;
    }
}