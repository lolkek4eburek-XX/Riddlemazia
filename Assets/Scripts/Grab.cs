using UnityEngine;

public class Grab : MonoBehaviour
{
    public float grabDistance = 5f; // Максимальная дистанция захвата
    public float throwForce = 10f; // Сила броска
    public Transform holdPoint; // Точка, где будет удерживаться объект
    private GameObject grabbedObject = null; // Захваченный объект
    private Rigidbody grabbedRigidbody = null; // Rigidbody захваченного объекта

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) // Клавиша E для захвата/броска
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
            if (hit.collider.gameObject.CompareTag("Grabbable")) // Проверка на тег "Grabbable"
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
            grabbedRigidbody.velocity = directionToHoldPoint * 10f; // Скорость перемещения объекта к точке удержания
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