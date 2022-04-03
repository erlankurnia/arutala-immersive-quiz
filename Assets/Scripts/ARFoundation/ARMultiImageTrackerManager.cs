using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace ArutalaQuiz {
    [System.Serializable]
    public struct TrackedPrefab {
        public string name;
        public GameObject prefab;
    }
    [RequireComponent(typeof(ARTrackedImageManager))]
    public class ARMultiImageTrackerManager : MonoBehaviour {
        [SerializeField] private bool showDebug = false;
        [SerializeField] private Text debugText;
        [SerializeField] private InputField xIF, yIF, zIF;
        [SerializeField] private Button submitBtn;
        public Vector3 customScale = new Vector3(0, 0, 0);
        public Quaternion customRotation = new Quaternion(0, 0, 0, 0);

        private ARTrackedImageManager arTrackedImageManager;
        private Dictionary<string, Transform> trackableObjectsDict = new Dictionary<string, Transform>();

        private void Awake() {
            arTrackedImageManager = GetComponent<ARTrackedImageManager>();
            submitBtn?.onClick.AddListener(() => {
                customScale = new Vector3(float.Parse(xIF.text), float.Parse(yIF.text), float.Parse(zIF.text));
            });
        }

        private void OnEnable() {
            //  Create trackable object dictionary
            trackableObjectsDict.Clear();
            foreach (GameObject obj in GameObject.FindGameObjectsWithTag("TrackableObject")) {
                obj.SetActive(false);
                trackableObjectsDict.Add(obj.name, obj.transform);
            }

            debugText.transform.parent.gameObject.SetActive(showDebug);
        }

        private void Start() {
            if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer) {
                //  Start AR Mode
                arTrackedImageManager.subsystem.Start();
                //  Add method : tracked image status changed
                arTrackedImageManager.trackedImagesChanged += OnTrackedImageChanged;
            }
        }

        private void Update() {
            if (Input.GetKeyDown(KeyCode.Escape)) SceneManager.LoadScene("Main");
        }

        private void OnDisable() {
            if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer) {
                //  Remove method : tracked image status changed
                arTrackedImageManager.trackedImagesChanged -= OnTrackedImageChanged;
            }
        }

        /// <summary>
        /// Tracked image status has changed
        /// </summary>
        /// <param name="eventArgs"></param>
        private void OnTrackedImageChanged(ARTrackedImagesChangedEventArgs eventArgs) {
            //if (debugText.text.Split('\n').Length > 10) debugText.text = "";

            //  Enable tracked object
            foreach (ARTrackedImage addedImage in eventArgs.added) {
                UpdateTrackedImage(addedImage);
            }

            //  Update tracked object
            foreach (ARTrackedImage updatedImage in eventArgs.updated) {
                if (updatedImage.trackingState == TrackingState.Tracking) {
                    //  If trackable object inside camera viewfield
                    UpdateTrackedImage(updatedImage);
                } else {
                    //  Otherwise
                    Transform go = trackableObjectsDict[updatedImage.referenceImage.name];
                    go.gameObject.SetActive(false);
                }
            }

            //  Disable untracked object
            foreach (ARTrackedImage removedImage in eventArgs.removed) {
                Transform go = trackableObjectsDict[removedImage.referenceImage.name];
                //debugText.text += "\n" + removedImage.referenceImage.name + " removed";
                go.gameObject.SetActive(false);
            }
        }

        /// <summary>
        /// Updating trackable object transform
        /// </summary>
        /// <param name="trackedImage"></param>
        private void UpdateTrackedImage(ARTrackedImage trackedImage) {
            //  Select trackable object to enable
            string name = trackedImage.referenceImage.name;
            Transform obj = trackableObjectsDict[name];

            //  Update object transform
            Vector3 pos = trackedImage.transform.position;
            Quaternion rot = trackedImage.transform.rotation;
            obj.position = pos;
            obj.rotation = rot;
            obj.localScale = new Vector3(customScale.x, customScale.y, customScale.z);
            obj.gameObject.SetActive(true);
            //debugText.text += "\ncustomScale : " + customScale + "\n";
            //debugText.text += "\nlocalScale " + name + " : " + obj.localScale + "\n";
            //debugText.text += "\nposition " + name + " : " + pos + "\n";

            //  Disable others
            foreach (Transform go in trackableObjectsDict.Values) {
                if (go.name != name) {
                    go.gameObject.SetActive(false);
                }
            }
        }
    }
}