using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;

public class Enemies : NetworkBehaviour {
    public Player targetPlayer; // Targeted player
    public float targetSpeed; // Enemy speed
    public float screenLimitX; // Horizontal limit
    public float screenLimitY; // Vertical limit
    public Vector3 endPoint; // Ending position
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        Vector2 screenSize = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        screenLimitX = screenSize.x + 4.0f; // Set horizontal limit a little bit beyond screen
        screenLimitY = screenSize.y + 4.0f; // Set vertical limit a little bit beyond screen
        targetPlayer = GameObject.FindObjectOfType(typeof(Player)) as Player; // Target a player
        Vector2 offset = new Vector2(transform.position.x - targetPlayer.transform.position.x, transform.position.y - targetPlayer.transform.position.y);
        this.transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg); // Get angle to player
        targetSpeed = Random.Range(1.0f, 1.75f); // Generate random speed
        endPoint = this.transform.position - (this.transform.right * 50.0f); // Plan  movement in that angle
        this.transform.eulerAngles += new Vector3(0, 0, 90); // Face towards movement
    }

    void FixedUpdate() {
        this.transform.position = Vector3.MoveTowards(this.transform.position, endPoint, 0.05f * targetSpeed); // Go in straight line
        if (Mathf.Abs(this.transform.position.x) > screenLimitX || Mathf.Abs(this.transform.position.y) > screenLimitY) { // If out of bounds, self destruct
            Destroy(gameObject);
        }
    }
}
