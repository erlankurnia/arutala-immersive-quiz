using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace ArutalaQuiz {
    public class ScreenshotManager : MonoBehaviour {
        [SerializeField] private string filePath = "";
        [SerializeField] private Button captureAndShareBtn;

        private void Awake() {
            captureAndShareBtn?.onClick.AddListener(CaptureAndShare);
        }

        //  Hide capture button then take a screenshot and share it
        private void CaptureAndShare() {
            captureAndShareBtn.gameObject.SetActive(false);
            StartCoroutine(TakeAndShare());
        }

        //  Take a screenshot and share it
        private IEnumerator TakeAndShare() {
            //  Take a screenshot save to device storage
            ScreenCapture.CaptureScreenshot(this.filePath);
            yield return new WaitForEndOfFrame();

            //  Share the screenshot
            string filePath = Path.Combine(Application.persistentDataPath, this.filePath);
            new NativeShare()
                .SetSubject("Subject goes here")
                .AddFile(filePath)
                .SetCallback((result, shareTarget) => Debug.Log("Share result: " + result + ", selected app: " + shareTarget))
                .Share();

            //  Show capture button again
            captureAndShareBtn.gameObject.SetActive(true);
        }
    }
}
