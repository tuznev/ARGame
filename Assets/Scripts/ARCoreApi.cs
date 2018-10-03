using UnityEngine;
using GoogleARCore;
using System.Collections.Generic;

#if UNITY_EDITOR
using Input = GoogleARCore.InstantPreviewInput;
#endif

public class ARCoreApi : MonoBehaviour {

	public Camera FirstPersonCamera;
	public GameObject DetectedPlanePrefab;
	public GameObject SearchingForPlaneUI; // A gameobject parenting UI for displaying the "searching for planes" snackbar
	private const float k_ModelRotation = 180.0f; // The rotation in degrees need to apply to model when the Andy model is placed.

	private List<DetectedPlane> m_AllPlanes = new List<DetectedPlane> (); // A list to hold all planes ARCore is tracking in the current frame.This object is used across
																		  // the application to avoid per-frame allocations.
	private bool m_IsQuitting = false; // True if the app is in the process of quitting due to an ARCore connection error, otherwise false

	private List<GameObject> distancedObj = new List<GameObject> ();

	public void Update () {
		_UpdateApplicationLifecycle ();

		HideSnackBar (); // Hide snackbar when currently tracking at least one plane.

		Touch touch; // If the player has not touched the screen, we are done with this update.
		if (Input.touchCount < 1 || (touch = Input.GetTouch (0)).phase != TouchPhase.Began) {
			return;
		}

		// Raycast against the location the player touched to search for planes.
		TrackableHit hit;
		TrackableHitFlags raycastFilter = TrackableHitFlags.PlaneWithinPolygon | TrackableHitFlags.FeaturePointWithSurfaceNormal;

		if (Frame.Raycast (touch.position.x, touch.position.y, raycastFilter, out hit)) {
			// Use hit pose and camera pose to check if hittest is from the back of the plane, if it is, no need to create the anchor
			if ((hit.Trackable is DetectedPlane) &&
				Vector3.Dot (FirstPersonCamera.transform.position - hit.Pose.position,
					hit.Pose.rotation * Vector3.up) < 0) {
				Debug.Log ("Hit at back of the current DetectedPlane");
			} else {
				GameObject prefab = GameObject.CreatePrimitive (PrimitiveType.Sphere);
				prefab.transform.localScale *= 0.1f;
				prefab.transform.position = hit.Pose.position;
				prefab.transform.rotation = hit.Pose.rotation;
				prefab.transform.Rotate (0, k_ModelRotation, 0, Space.Self); // Compensate for the hitPose rotation facing away from the raycast (i.e. camera)
				var anchor = hit.Trackable.CreateAnchor (hit.Pose); // Create an anchor to allow ARCore to track the hitpoint as understanding of the physical world evolves
				prefab.transform.SetParent (anchor.transform); // Make model a child of the anchor
			}
		}
	}

	private void HideSnackBar () {
		Session.GetTrackables<DetectedPlane> (m_AllPlanes);
		bool showSearchingUI = true;
		for (int i = 0; i < m_AllPlanes.Count; i++) {
			if (m_AllPlanes[i].TrackingState == TrackingState.Tracking) {
				showSearchingUI = false;
				break;
			}
		}

		SearchingForPlaneUI.SetActive (showSearchingUI);
	}

	private void _UpdateApplicationLifecycle () { // Check and update the application lifecycle.
		if (Input.GetKey (KeyCode.Escape))
			Application.Quit ();

		if (Session.Status != SessionStatus.Tracking) { // Only allow the screen to sleep when not tracking.
			Screen.sleepTimeout = 15;
		} else {
			Screen.sleepTimeout = SleepTimeout.NeverSleep;
		}

		if (m_IsQuitting) {
			return;
		}

		// Quit if ARCore was unable to connect and give Unity some time for the toast to appear.
		if (Session.Status == SessionStatus.ErrorPermissionNotGranted) {
			_ShowAndroidToastMessage ("Camera permission is needed to run this application.");
			m_IsQuitting = true;
			Invoke ("_DoQuit", 0.5f);
		} else if (Session.Status.IsError ()) {
			_ShowAndroidToastMessage ("ARCore encountered a problem connecting.  Please start the app again.");
			m_IsQuitting = true;
			Invoke ("_DoQuit", 0.5f);
		}
	}

	private void _DoQuit () {
		Application.Quit ();
	}

	private void _ShowAndroidToastMessage (string message) { // Show an Android toast message.
		AndroidJavaClass unityPlayer = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");
		AndroidJavaObject unityActivity = unityPlayer.GetStatic<AndroidJavaObject> ("currentActivity");

		if (unityActivity != null) {
			AndroidJavaClass toastClass = new AndroidJavaClass ("android.widget.Toast");
			unityActivity.Call ("runOnUiThread", new AndroidJavaRunnable (() => {
				AndroidJavaObject toastObject = toastClass.CallStatic<AndroidJavaObject> ("makeText", unityActivity,
					message, 0);
				toastObject.Call ("show");
			}));
		}
	}
}
