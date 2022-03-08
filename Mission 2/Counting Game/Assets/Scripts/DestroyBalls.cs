using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBalls : MonoBehaviour {
    private GameManager gameManager;

    private void Start() {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag != "Box") {
            Destroy(other.gameObject);
            gameManager.UpdateLife(-1);

            var particle = gameManager.isGameActive ? gameManager.smoke : gameManager.explosion;
            Instantiate(particle, other.transform.position, other.transform.rotation);
        }
    }
}
