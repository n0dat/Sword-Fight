using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;

public class PlayerController : MonoBehaviour {
    
    public Camera m_playerCamera;
    public float m_walkSpeed = 5f;
    public float m_sprintSpeed = 10f;
    public float m_jumpPower = 7f;
    public float m_gravity = 10f;
    
    public float m_lookSensitivity = 2f;
    public float m_lookXLimit = 90f;
    
    Vector3 m_moveDirection = Vector3.zero;
    float m_rotationX = 0;
    
    public bool m_canMove = true;
    
    CharacterController m_characterController;

    void Start() {
        m_characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update() {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = m_canMove ? (isRunning ? m_sprintSpeed : m_walkSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = m_canMove ? (isRunning ? m_sprintSpeed : m_walkSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = m_moveDirection.y;
        m_moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        if (Input.GetButton("Jump") && m_canMove && m_characterController.isGrounded)
            m_moveDirection.y = m_jumpPower;
        else
            m_moveDirection.y = movementDirectionY;

        if (!m_characterController.isGrounded)
            m_moveDirection.y -= m_gravity * Time.deltaTime;
        
        m_characterController.Move(m_moveDirection * Time.deltaTime);

        if (m_canMove) {
            m_rotationX += -Input.GetAxis("Mouse Y") * m_lookSensitivity;
            m_rotationX = Mathf.Clamp(m_rotationX, -m_lookXLimit, m_lookXLimit);
            m_playerCamera.transform.localRotation = Quaternion.Euler(m_rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * m_lookSensitivity, 0);
        }
    }
    //
    // public Transform m_player;
    // public Transform m_camera;
    //
    // public float m_sensitivityX;
    // public float m_sensitivityY;
    //
    // float m_rotationX;
    // float m_rotationY;
    //
    // private void Start() {
    //     // lock and hide the cursor
    //     Cursor.lockState = CursorLockMode.Locked;
    //     Cursor.visible = false;
    // }
    //
    // private void Update() {
    //     // get current mouse input
    //     Vector2 mouseInput = Mouse.current.delta.ReadValue() * Time.deltaTime;
    //     
    //     // set rotation
    //     m_rotationY += mouseInput.x * m_sensitivityX;
    //     m_rotationX -= mouseInput.y * m_sensitivityY;
    //     
    //     // limit movement on X axis to be vertically up and down
    //     // (this will avoid doing a "loop")
    //     m_rotationX = Mathf.Clamp(m_rotationX, -90f, 90f);
    //     
    //     // set this transform rotation (camera) and the orientation transform rotation (player)
    //     transform.rotation = Quaternion.Euler(m_rotationX, m_rotationY, 0);
    //     m_player.rotation = Quaternion.Euler(0, m_rotationY, 0);
    //     
    //     m_camera.position = transform.position;
    // }
}
