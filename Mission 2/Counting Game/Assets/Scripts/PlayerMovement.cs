using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public float speed = 20.0f;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start() {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update() {
        if (!gameManager.isGameActive) return;

        float movement = Input.GetAxis("Horizontal");
        transform.Translate(Vector2.right * Time.deltaTime * speed * movement);
    }
}
