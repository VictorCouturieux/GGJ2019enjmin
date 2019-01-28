using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    public Transform target;
    public float m_speed = 1f;
    Camera cam;


	// Use this for initialization
	void Start () {

        cam = GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {

        //cam.orthographicSize = (Screen.height / 100f) / 1f;

        if (target){
            transform.position = Vector3.Lerp(transform.position, target.position, m_speed) + new Vector3(0, 0, -10);

        }

    }
}
