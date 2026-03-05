using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour {
    Rigidbody2D hurtbox;
    float horizontalMovement;
    float verticalMovement;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        hurtbox = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate() {
        if (Keyboard.current.leftArrowKey.isPressed) {
            horizontalMovement = -3.0f;
        }
        else if (Keyboard.current.rightArrowKey.isPressed) {
            horizontalMovement = 3.0f;
        }
        else {
            horizontalMovement = 0;
        }
        if (Keyboard.current.downArrowKey.isPressed)
        {
            verticalMovement = -3.0f;
        }
        else if (Keyboard.current.upArrowKey.isPressed)
        {
            verticalMovement = 3.0f;
        }
        else
        {
            verticalMovement = 0;
        }
        GetComponent<Rigidbody2D>().linearVelocity = new Vector2(horizontalMovement, verticalMovement);
    }

    void OnTriggerStay2D(Collider2D collision) {
        if (collision.CompareTag("Enemy")) {
            Destroy(gameObject);
        }
    }
}
