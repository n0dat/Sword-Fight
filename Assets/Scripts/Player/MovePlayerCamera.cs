using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour {
    
    public Transform m_cameraPosition;

    private void Update() {
        transform.position = m_cameraPosition.position;
    }
}
