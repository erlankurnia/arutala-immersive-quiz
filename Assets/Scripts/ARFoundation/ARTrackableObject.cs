using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArutalaQuiz {
    public class ARTrackableObject : MonoBehaviour {
        [SerializeField] private AudioSource audioSource;
        private ParticleSystem fireParticle;

        private void Awake() {
            audioSource = GetComponentInChildren<AudioSource>();
            fireParticle = transform.Find("TrackableObject/Fire").GetComponent<ParticleSystem>();
        }

        private void OnEnable() {
            audioSource.Play();
            //fireParticle.gameObject.SetActive(true);
            fireParticle.Play(true);
        }

        private void Update() {
            //  Stop strect animation
            if (GetComponentInChildren<Blinking>().isBlinkingOver) {
                GetComponentInChildren<Stretch>().isStrectOver = true;
                //fireParticle.gameObject.SetActive(false);
                fireParticle.Stop(true, ParticleSystemStopBehavior.StopEmitting);
            }
        }

        private void OnDisable() {
            audioSource.Pause();
        }
    }
}
