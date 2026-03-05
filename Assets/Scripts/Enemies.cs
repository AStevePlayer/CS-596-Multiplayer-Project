using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemies : MonoBehaviour {
    public Player targetPlayer;
    public float targetAngle;
    //public float targetSpeed;
    public float screenLimitX;
    public float screenLimitY;
    public Vector3 endPoint;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        Vector2 screenSize = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        screenLimitX = screenSize.x + 4.0f;
        screenLimitY = screenSize.y + 4.0f;
        targetPlayer = GameObject.FindObjectOfType(typeof(Player)) as Player;
        Vector2 offset = new Vector2(transform.position.x - targetPlayer.transform.position.x, transform.position.y - targetPlayer.transform.position.y);
        this.transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg);
        endPoint = this.transform.position - (this.transform.right * 50.0f);
        this.transform.eulerAngles += new Vector3(0, 0, 90);
    }

    // Update is called once per frame
    void FixedUpdate() {
        this.transform.position = Vector3.MoveTowards(this.transform.position, endPoint, 0.05f);
        if (Mathf.Abs(this.transform.position.x) > screenLimitX || Mathf.Abs(this.transform.position.y) > screenLimitY) {
            Destroy(gameObject);
        }
    }
}
