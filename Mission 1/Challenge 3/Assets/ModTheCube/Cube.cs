using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    public MeshRenderer Renderer;
    private Color lerpedColor = Color.white;

    
    void Start()
    {
        float cubeScale = Random.Range(0.5f, 2f);
        transform.position = new Vector3(3, 4, 1);
        transform.localScale = Vector3.one * cubeScale;
    }
    
    void Update()
    {
        transform.Rotate(10.0f * Time.deltaTime, 0.0f, 0.0f);
        lerpedColor = Color.Lerp(Color.black, Color.yellow, Mathf.PingPong(Time.time, 1));
        Material material = Renderer.material;
        material.color = lerpedColor;
    }
}
