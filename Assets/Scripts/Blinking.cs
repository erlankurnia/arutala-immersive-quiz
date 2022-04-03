using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArutalaQuiz {
    public class Blinking : MonoBehaviour {
        [SerializeField] private Color startColor = Color.black, endColor = Color.cyan;
        [SerializeField] private float blinkingSpeed = 1, blinkingDuration = 5, delayBeforeBlink = 0;
        private Renderer rend;
        private float delay = 0, duration = 0;
        [HideInInspector] public bool isBlinkingOver = false;

        private void Awake() {
            rend = GetComponent<Renderer>();
        }

        private void OnEnable() {
            //  Reset timer
            rend.material.color = startColor;
            delay = delayBeforeBlink;
            duration = 0;
            isBlinkingOver = false;
        }

        private void Update() {
            if (delay > 0) {
                //  Wait before blinking
                delay -= Time.deltaTime;
            } else if (duration < blinkingDuration) {
                //  Object blinking 
                duration += Time.deltaTime;
                rend.material.color = Color.Lerp(startColor, endColor, Mathf.PingPong(Time.time * (blinkingDuration - duration) / 20, 1));
            } else {
                //  Blinking over
                rend.material.color = endColor;
                isBlinkingOver = true;
            }
        }
    }
}
