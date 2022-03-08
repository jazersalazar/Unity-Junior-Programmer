using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter : MonoBehaviour {
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start() {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        other.gameObject.transform.parent = transform;
        if(other.gameObject.tag == "Special Ball") {
            gameManager.UpdateLife(1);
            gameManager.UpdateScore(5);
        } else {
            gameManager.UpdateScore(1);
        }
    }
}
