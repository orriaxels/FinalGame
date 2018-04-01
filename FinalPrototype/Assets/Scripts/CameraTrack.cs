using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrack : MonoBehaviour {
	public Transform player1, player2;
	public float minSizeY = 5f;

	void SetCameraPos() {
		Vector3 middle = (player1.position + player2.position) * 0.5f;

		Camera.main.transform.position = new Vector3(
			middle.x,
			middle.y,
			Camera.main.transform.position.z
		);
	}

	void SetCameraSize() {
		//horizontal size is based on actual screen ratio
		float minSizeX = minSizeY * Screen.width / Screen.height;

		//multiplying by 0.5, because the ortographicSize is actually half the height
		float width = Mathf.Abs(player1.position.x - player2.position.x) * 0.5f;
		float height = Mathf.Abs(player1.position.y - player2.position.y) * 0.5f;

		//computing the size
		float camSizeX = Mathf.Max(width, minSizeX);
		Camera.main.orthographicSize = Mathf.Max(height,
			camSizeX * Screen.height / Screen.width, minSizeY);
	}

	void Update() {
		SetCameraPos();
		SetCameraSize();
	}
//	public Transform player1;
//	public Transform player2;

//	private const float DISTANCE_MARGIN = 1.0f;
//
//	private Vector3 middlePoint;
//	private float distanceFromMiddlePoint;
//	private float distanceBetweenPlayers;
//	private float cameraDistance;
//	private float aspectRatio;
//	private float fov;
//	private float tanFov;
//
//	void Start() {
//		aspectRatio = Screen.width / Screen.height;
//		tanFov = Mathf.Tan(Mathf.Deg2Rad * Camera.main.fieldOfView / 2.0f);
//	}	
//
//	void Update () {

		// Position the camera in the center.
//		Vector3 newCameraPos = Camera.main.transform.position;
//		newCameraPos.x = middlePoint.x;
//		newCameraPos.z = middlePoint.z;
//
//		Camera.main.transform.position = newCameraPos;
//
//		// Find the middle point between players.
//		Vector3 vectorBetweenPlayers = player2.position - player1.position;
//		middlePoint = player1.position + 0.5f * vectorBetweenPlayers;
//
//		// Calculate the new distance.
//		distanceBetweenPlayers = vectorBetweenPlayers.magnitude;
//		cameraDistance = (distanceBetweenPlayers / 2.0f / aspectRatio) / tanFov;
//
//		// Set camera to new position.
//		Vector3 dir = (Camera.main.transform.position - middlePoint).normalized;
//		Camera.main.transform.position = middlePoint + dir * (cameraDistance + DISTANCE_MARGIN);
//	}
}