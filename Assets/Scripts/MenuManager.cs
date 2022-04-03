using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace ArutalaQuiz {
    public class MenuManager : MonoBehaviour {
        [SerializeField] private Button arModeBtn, timeConvertionBtn;

        private void Awake() {
            arModeBtn.onClick.AddListener(() => SceneManager.LoadScene("ARMode"));
            timeConvertionBtn.onClick.AddListener(() => SceneManager.LoadScene("TimeConvertion"));
        }
    }
}
