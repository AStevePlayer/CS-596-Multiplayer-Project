using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class Player2 : MonoBehaviour {
    Rigidbody2D hurtbox;
    float horizontalMovement;
    float verticalMovement;
    public Vector3 knockback;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        hurtbox = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate() {
        if (Keyboard.current.aKey.isPressed) {
            horizontalMovement = -3.0f;
        }
        else if (Keyboard.current.dKey.isPressed) {
            horizontalMovement = 3.0f;
        }
        else {
            horizontalMovement = 0;
        }
        if (Keyboard.current.sKey.isPressed)
        {
            verticalMovement = -3.0f;
        }
        else if (Keyboard.current.wKey.isPressed)
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
        if (collision.CompareTag("Player")) {
            Vector2 offset = new Vector2(transform.position.x - collision.transform.position.x, transform.position.y - collision.transform.position.y);
            this.transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg);
            knockback = this.transform.position + (this.transform.right * 2.5f);
            this.transform.eulerAngles = new Vector3(0, 0, 0);
            this.transform.position = Vector3.MoveTowards(this.transform.position, knockback, 0.75f);
        }
    }
}
