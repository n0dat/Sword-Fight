using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCamera : MonoBehaviour {
    
    public Transform m_orientation;
    
    public float m_sensitivityX;
    public float m_sensitivityY;
    
    float m_rotationX;
    float m_rotationY;

    private void Start() {
        // lock and hide the cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update() {
        // get current mouse input
        Vector2 mouseInput = Mouse.current.delta.ReadValue() * Time.deltaTime;
        
        // set rotation
        m_rotationY += mouseInput.x * m_sensitivityX;
        m_rotationX -= mouseInput.y * m_sensitivityY;
        
        // limit movement on X axis to be vertically up and down
        // (this will avoid doing a "loop")
        m_rotationX = Mathf.Clamp(m_rotationX, -90f, 90f);
        
        // set this object transform (camera) and the orientation transform rotation (player)
        transform.rotation = Quaternion.Euler(m_rotationX, m_rotationY, 0);
        m_orientation.rotation = Quaternion.Euler(0, m_rotationY, 0);
    }
}
