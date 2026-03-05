using UnityEngine;

public class Spawner : MonoBehaviour {
    public float spawnTimer = 1.0f;
    public GameObject enemies;
    public float screenLimitX;
    public float screenLimitY;
    public float negativeX;
    public float negativeY;
    public int rng;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        Vector2 screenSize = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        screenLimitX = screenSize.x + 1.0f;
        screenLimitY = screenSize.y + 1.0f;
    }

    // Update is called once per frame
    void FixedUpdate() {
        spawnTimer -= Time.deltaTime;
        if (spawnTimer < 0) {
            rng = Random.Range(0, 2);
            if (rng == 0) {
                rng = Random.Range(0, 2);
                negativeY = rng;
                if (rng == 0)
                {
                    negativeY = -1.0f;
                }
                Vector3 location1 = new Vector3(Random.Range(-screenLimitX - 3.0f, screenLimitX + 3.0f), Random.Range(screenLimitY, screenLimitY + 3.0f) * negativeY, 0);
                GameObject newEnemy = Instantiate(enemies, location1, Quaternion.identity);
                spawnTimer = 1.0f;
            }
            else {
                rng = Random.Range(0, 2);
                negativeX = rng;
                if (rng == 0)
                {
                    negativeX = -1.0f;
                }
                Vector3 location1 = new Vector3(Random.Range(screenLimitX, screenLimitX + 3.0f) * negativeX, Random.Range(-screenLimitY - 3.0f, screenLimitY + 3.0f), 0);
                GameObject newEnemy = Instantiate(enemies, location1, Quaternion.identity);
                spawnTimer = 1.0f;
            }
            //rng = Random.Range(0, 2);
            //negativeX = rng;
            //if (rng == 0) {
            //    negativeX = -1.0f;
            //}
            //rng = Random.Range(0, 2);
            //negativeY = rng;
            //if (rng == 0)
            //{
            //    negativeY = -1.0f;
            //}
            //Vector3 location1 = new Vector3(Random.Range(screenLimitX, screenLimitX + 3.0f) * negativeX, Random.Range(screenLimitY, screenLimitY + 3.0f) * negativeY, 0);
            //GameObject newEnemy = Instantiate(enemies, location1, Quaternion.identity);
            //spawnTimer = 1.0f;
        }
    }
}
