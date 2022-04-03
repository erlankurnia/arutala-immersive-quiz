using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace ArutalaQuiz {
    public class TimeConvertionManager : MonoBehaviour {
        [SerializeField] private InputField givenTimeIF;
        [SerializeField] private Text resultTxt;
        [SerializeField] private Button convertBtn, clearBtn;

        private void Start() {
            givenTimeIF.onValueChanged.AddListener(OnValueChanged);
            clearBtn.onClick.AddListener(() => resultTxt.text = "");
            convertBtn.onClick.AddListener(Convert);
            convertBtn.interactable = false;
        }

        private void Update() {
            if (Input.GetKeyDown(KeyCode.Escape)) SceneManager.LoadScene("Main");
        }

        private void OnValueChanged(string value) {
            convertBtn.interactable = Regex.IsMatch(value, @"^((0[0-9]|1[0-1]):[0-5][0-9]:[0-5][0-9]|12:00:00)(A|P)M$");
        }

        private void Convert() {
            string givenTime = givenTimeIF.text;
            givenTime = givenTime.Replace("A", " A");
            givenTime = givenTime.Replace("P", " P");
            System.DateTime time = System.DateTime.ParseExact(givenTime, "hh:mm:ss tt", CultureInfo.InvariantCulture);
            string convertedTime = time.ToString("HH:mm:ss");
            resultTxt.text += $"\nGiven Time\t\t\t: {givenTime}\nConverted Time\t: {convertedTime}\n";
        }
    }
}
