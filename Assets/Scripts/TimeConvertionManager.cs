using System.Collections;
using System.Collections.Generic;
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
            string convertedTime = "";

            string hour = givenTime.Split(':')[0];
            string minute = givenTime.Split(':')[1];
            Regex re = new Regex(@"(\d+)([A-P]+)");
            Match result = re.Match(givenTime.Split(':')[2]);
            string second = result.Groups[1].ToString();
            string initial = result.Groups[2].ToString().ToUpper();

            if (initial == "PM") {
                hour = "" + (byte.Parse(hour) + 12);
            }

            convertedTime = $"{hour:00}:{minute:00}:{second:00}";

            resultTxt.text += $"\nGiven Time\t\t\t: {givenTime}\nConverted Time\t: {convertedTime}\n";
        }
    }
}
