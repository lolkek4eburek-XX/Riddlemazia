using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f;
    public float rotateSpeed = 3.0f;
    public float jumpForce = 5.0f;

    public int health = 5;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    private bool isGrounded;

    public bool joystickControl = false; // Переменная для контроля управления
    public GameObject mobileUI;
    public Joystick joystick;

    private float verticalLookRotation; // Новая переменная для отслеживания поворота камеры по вертикали
    private bool isSneaking = false; // Вводим переменную для отслеживания состояния ползания

    void Start()
    {
        if (joystickControl)
        {
            Cursor.lockState = CursorLockMode.Confined;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    void Update()
    {
        if (!GetComponent<MenuControl>().isOpened)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical") + joystick.Vertical;

            if (health <= 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }

            Vector3 moveDirection = new Vector3(horizontalInput, 0, verticalInput) * speed;
            moveDirection = transform.TransformDirection(moveDirection);
            transform.position += moveDirection * Time.deltaTime;

            if (joystickControl)
            {
                float mouseX = joystick.Horizontal;
                transform.Rotate(0, mouseX * rotateSpeed, 0);
                mobileUI.SetActive(true);
            }
            else
            {
                float mouseX = Input.GetAxis("Mouse X");
                transform.Rotate(0, mouseX * rotateSpeed, 0);
                mobileUI.SetActive(false);
            }

            // Поворот камеры по вертикали
            float mouseY = Input.GetAxis("Mouse Y");
            verticalLookRotation += mouseY * rotateSpeed;
            verticalLookRotation = Mathf.Clamp(verticalLookRotation, -90f, 90f); // Ограничение поворота камеры по вертикали
            Camera.main.transform.localEulerAngles = new Vector3(-verticalLookRotation, 0, 0);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }

            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                Sneak(true);
            }
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                Sneak(false);
            }

            // Проверка нажатия клавиши "Control" для бега
            if (!isSneaking)
            {
                if (Input.GetKeyDown(KeyCode.LeftControl))
                {
                    Run(true);
                }
                if (Input.GetKeyUp(KeyCode.LeftControl))
                {
                    Run(false);
                }
            }
        }
    }

    public void Jump()
    {
        if (Physics.CheckSphere(groundCheck.position, groundDistance, groundMask))
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    public void Sneak(bool isSneaking)
    {
        this.isSneaking = isSneaking; // Обновляем переменную состояния ползания
        if (isSneaking)
        {
            speed = 1.0f;
            Transform transform = GetComponent<Transform>();
            transform.localScale = new Vector3(transform.localScale.x, 1, transform.localScale.z);
        }
        else
        {
            speed = 5.0f;
            Transform transform = GetComponent<Transform>();
            transform.localScale = new Vector3(transform.localScale.x, 1.5f, transform.localScale.z);
        }
    }

    public void Run(bool isRunning)
    {
        if (isRunning)
        {
            speed = 10.0f;
        }
        else
        {
            speed = 5.0f;
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health < 0)
        {
            health = 0;
        }
        if (health > 5)
        {
            health = 5;
        }
    }
}