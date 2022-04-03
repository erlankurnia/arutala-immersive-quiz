using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArutalaQuiz {
    public class RotateObject : MonoBehaviour {
        [SerializeField] private float rotationSpeed = 10f;
        private bool isDragging = false;

        private void Update() {
            if (isDragging) {
                print("IsDragging: " + isDragging);
                float x = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
                float y = Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;

                //  Change objectr rotation
                transform.Rotate(Vector3.down * x);
                transform.Rotate(Vector3.right * y);
            }
        }

        //  While dragging
        private void OnMouseDrag() {
            isDragging = true;
        }

        //  When dragging is done
        private void OnMouseUp() {
            isDragging = false;
        }
    }
}
