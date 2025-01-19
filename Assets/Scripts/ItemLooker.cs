using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemLooker : MonoBehaviour {

    [SerializeField]
    Camera mainCamera = null;

    void Update() {
        if (Input.GetKeyDown(KeyCode.E)) {
            Vector3 origin = mainCamera.transform.position;
            Vector3 direction = mainCamera.transform.forward;
            
            RaycastHit hit;

            if (Physics.Raycast(origin, direction, out hit)) {
                Debug.Log($"Hit object: {hit.collider.name}, Distance: {hit.distance}");
                if (hit.distance < 5.0f) {
                    Destroy(hit.collider.gameObject);
                }
            }
            else {
                Debug.Log("No object hit");
            }
        }
    }
}
