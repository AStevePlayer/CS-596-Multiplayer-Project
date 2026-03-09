using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using Unity.Netcode;

public class Player : NetworkBehaviour {
    Rigidbody2D hurtbox;
    float horizontalMovement;
    float verticalMovement;
    public Vector3 knockback;

    public override void OnNetworkSpawn() {
        hurtbox = GetComponent<Rigidbody2D>(); // Get rigidbody when spawned
    }

    void FixedUpdate() {
        if (!IsOwner) { // Only the owner of this object can move it
            return;
        }
        if (Keyboard.current.leftArrowKey.isPressed) { // Movement
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
        if (collision.CompareTag("Enemy")) { // If colliding with enemy, die
            Destroy(gameObject);
        }
        if (collision.CompareTag("Player")) { // If colliding with player, get pushed
            Vector2 offset = new Vector2(transform.position.x - collision.transform.position.x, transform.position.y - collision.transform.position.y);
            this.transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg);
            knockback = this.transform.position + (this.transform.right * 2.5f);
            this.transform.eulerAngles = new Vector3(0, 0, 0);
            this.transform.position = Vector3.MoveTowards(this.transform.position, knockback, 0.75f);
        }
    }
}
