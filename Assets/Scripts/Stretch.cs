using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArutalaQuiz {
    public class Stretch : MonoBehaviour {
        [SerializeField] private float max = 2f, min = .5f, duration = 10;
        private float baseScaleY = 0;
        [HideInInspector] public bool isStrectOver = false;

        private void Awake() {
            baseScaleY = transform.localScale.y;
        }

        private void OnEnable() {
            isStrectOver = false;
        }

        private void Update() {
            if (!isStrectOver) {
                //  Stretch object
                float scaleY = Mathf.Lerp(min, max, Mathf.PingPong(Time.time * duration, 1));
                transform.localScale = new Vector3(transform.localScale.x, scaleY, transform.localScale.z);
            } else {
                //  Stretch object over
                //transform.localScale = new Vector3(transform.localScale.x, baseScaleY, transform.localScale.z);
            }
        }
    }
}
