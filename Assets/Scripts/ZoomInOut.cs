using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArutalaQuiz {
    public class ZoomInOut : MonoBehaviour {
        [SerializeField] private float zoomInLimit = 1;
        [SerializeField] private float zoomOutLimit = 0.1f;

        private void Update() {
            if (Input.touchCount == 2) {    //  Touch gesture
                Touch one = Input.GetTouch(0);
                Touch two = Input.GetTouch(1);
                Zoom(GetDiff(one, two) * 0.0025f);
            } else if (Input.GetAxis("Mouse ScrollWheel") != 0) {   //  Mouse scroll wheel
                Zoom(Input.GetAxis("Mouse ScrollWheel"));
            }
        }

        /// <summary>
        /// Get difference value between 2 detected touchs
        /// </summary>
        /// <param name="one"></param>
        /// <param name="two"></param>
        /// <returns></returns>
        private float GetDiff(Touch one, Touch two) {
            Vector2 onePrevPos = one.position - one.deltaPosition;
            Vector2 twoPrevPos = two.position - two.deltaPosition;
            float prevMagnitude = (onePrevPos - twoPrevPos).magnitude;
            float currMagnitude = (one.position - two.position).magnitude;
            return currMagnitude - prevMagnitude;
        }

        /// <summary>
        /// Zoom in (positive) or out(negative)
        /// </summary>
        /// <param name="scale"></param>
        private void Zoom(float scale) {
            //print("Scale: " + scale);
            foreach (GameObject trackable in GameObject.FindGameObjectsWithTag("TrackableObject")) {
                float scaledX = Mathf.Clamp(FindObjectOfType<ARMultiImageTrackerManager>().customScale.x + scale, zoomOutLimit, zoomInLimit);
                float scaledY = Mathf.Clamp(FindObjectOfType<ARMultiImageTrackerManager>().customScale.y + scale, zoomOutLimit, zoomInLimit);
                float scaledZ = Mathf.Clamp(FindObjectOfType<ARMultiImageTrackerManager>().customScale.z + scale, zoomOutLimit, zoomInLimit);
                FindObjectOfType<ARMultiImageTrackerManager>().customScale = new Vector3(scaledX, scaledY, scaledZ);
            }
        }
    }
}
