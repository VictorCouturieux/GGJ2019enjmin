using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class FogOfWarScript : MonoBehaviour
{
    public GameObject fogOfWarPlane;
    public Transform player;
    public LayerMask fogLayer;
    public float radius = 5f;
    public Color[] otherWorld;
    
    private float radiusSqr { get { return radius * radius; } }

    private Mesh mesh;
    private Vector3[] vertices;
    private Color[] colors;
    
    // Start is called before the first frame update
    void Start() {
        Initialize();
    }

    // Update is called once per frame
    void Update() {
        Ray r = new Ray(transform.position, player.position - transform.position);
        RaycastHit hit;
        if (Physics.Raycast(r, out hit, 1000, fogLayer, QueryTriggerInteraction.Collide)) {
            for (int i = 0; i < vertices.Length; i++) {
                Vector3 v = fogOfWarPlane.transform.TransformPoint(vertices[i]);
                float dist = Vector3.SqrMagnitude(v - hit.point);
                if (dist < radiusSqr) {
                    float alpha = Mathf.Min(colors[i].a, dist / radiusSqr);
//                    colors[i].a = 1-alpha;
                    colors[i] = Color.yellow;
                }
            }
            UpdateColor();
        }
    }

    void Initialize() {
        
        mesh = fogOfWarPlane.GetComponent<MeshFilter>().mesh;
        vertices = mesh.vertices;
        colors = new Color[vertices.Length];
        otherWorld = new Color[vertices.Length];
        for (int i = 0; i < colors.Length; i++) {   
//            colors[i] = Color.black;    
            colors[i] = Color.clear;    
        }
        UpdateColor();
    }

    void UpdateColor() {
        mesh.colors = colors;
    }
}
