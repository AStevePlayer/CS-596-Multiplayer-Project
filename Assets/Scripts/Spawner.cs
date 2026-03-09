using UnityEngine;
using Unity.Netcode;

public class Spawner : NetworkBehaviour {
    public float spawnTimer; // Time between spawns
    public float timePassed; // Time passed
    public GameObject enemies; // Enemies that will be spawned
    public float screenLimitX; // Horizontal limit
    public float screenLimitY; // Vertical limit
    public float negativeX; // Whether x coordinate will be positive or negative
    public float negativeY; // Whether y coordinate will be positive or negative
    public int rng; // Randomly generated number
    public enum gameState : int { // Game states
        waiting,
        slowSpawn,
        mediumSpawn,
        fastSpawn,
        finished,
    }
    public gameState currentState; // Current game state
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        spawnTimer = 3.0f; // Set spawn timer to slow spawn
        currentState = gameState.waiting; // Set game state to waiting
        Vector2 screenSize = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        screenLimitX = screenSize.x + 1.0f; // Set horizontal limit a little bit beyond screen
        screenLimitY = screenSize.y + 1.0f; // Set vertical limit a little bit beyond screen
    }

    // Update is called once per frame
    void FixedUpdate() {
        if (!IsServer) { // Don't allow clients to have their own enemy spawners
            return;
        }
        switch (currentState) {
            case gameState.waiting: // Wait until a player is present
                if ((Player)FindFirstObjectByType(typeof(Player)) != null) {
                    currentState = gameState.slowSpawn;
                }
                break;
            case gameState.slowSpawn: // Spawn enemies every 3 seconds
                if ((Player)FindFirstObjectByType(typeof(Player)) == null) { // If there are no players left, stop spawning
                    currentState = gameState.finished;
                    return;
                }
                spawnTimer -= Time.deltaTime;
                timePassed += Time.deltaTime;
                if (spawnTimer <= 0) {
                    spawnTimer = 3.0f;
                    rng = Random.Range(0, 2); // Decide whether x or y coordinate will be offscreen
                    if (rng == 0) {
                        rng = Random.Range(0, 2); // Decide whether offscreen coordinate will be positive or negative
                        negativeY = rng;
                        if (rng == 0) {
                            negativeY = -1.0f;
                        }
                        Vector3 location1 = new Vector3(Random.Range(-screenLimitX - 3.0f, screenLimitX + 3.0f), Random.Range(screenLimitY, screenLimitY + 3.0f) * negativeY, 0); // Generate coordinates
                        GameObject newEnemy = Instantiate(enemies, location1, Quaternion.identity);
                        newEnemy.GetComponent<NetworkObject>().Spawn(); // Show enemy on every device's screens
                    }
                    else {
                        rng = Random.Range(0, 2); // Decide whether offscreen coordinate will be positive or negative
                        negativeX = rng;
                        if (rng == 0) {
                            negativeX = -1.0f;
                        }
                        Vector3 location1 = new Vector3(Random.Range(screenLimitX, screenLimitX + 3.0f) * negativeX, Random.Range(-screenLimitY - 3.0f, screenLimitY + 3.0f), 0); // Generate coordinates
                        GameObject newEnemy = Instantiate(enemies, location1, Quaternion.identity);
                        newEnemy.GetComponent<NetworkObject>().Spawn(); // Show enemy on every device's screens
                    }
                }
                if (timePassed >= 20) { // If enough time passed, change state to medium spawn
                    spawnTimer = 2.0f;
                    currentState = gameState.mediumSpawn;
                }
                break;
            case gameState.mediumSpawn: // Spawn enemies every 2 seconds
                if ((Player)FindFirstObjectByType(typeof(Player)) == null) { // If there are no players left, stop spawning
                    currentState = gameState.finished;
                    return;
                }
                spawnTimer -= Time.deltaTime;
                timePassed += Time.deltaTime;
                if (spawnTimer <= 0) {
                    spawnTimer = 2.0f;
                    rng = Random.Range(0, 2); // Decide whether x or y coordinate will be offscreen
                    if (rng == 0) {
                        rng = Random.Range(0, 2); // Decide whether offscreen coordinate will be positive or negative
                        negativeY = rng;
                        if (rng == 0) {
                            negativeY = -1.0f;
                        }
                        Vector3 location1 = new Vector3(Random.Range(-screenLimitX - 3.0f, screenLimitX + 3.0f), Random.Range(screenLimitY, screenLimitY + 3.0f) * negativeY, 0); // Generate coordinates
                        GameObject newEnemy = Instantiate(enemies, location1, Quaternion.identity);
                        float randomSize = Random.Range(1.0f, 1.75f); // Generate random size
                        newEnemy.transform.localScale = new Vector3(randomSize, randomSize, randomSize);
                        newEnemy.GetComponent<NetworkObject>().Spawn(); // Show enemy on every device's screens
                    }
                    else {
                        rng = Random.Range(0, 2); // Decide whether offscreen coordinate will be positive or negative
                        negativeX = rng;
                        if (rng == 0) {
                            negativeX = -1.0f;
                        }
                        Vector3 location1 = new Vector3(Random.Range(screenLimitX, screenLimitX + 3.0f) * negativeX, Random.Range(-screenLimitY - 3.0f, screenLimitY + 3.0f), 0); // Generate coordinates
                        GameObject newEnemy = Instantiate(enemies, location1, Quaternion.identity);
                        float randomSize = Random.Range(1.0f, 1.5f); // Generate random size
                        newEnemy.transform.localScale = new Vector3(randomSize, randomSize, randomSize);
                        newEnemy.GetComponent<NetworkObject>().Spawn(); // Show enemy on every device's screens
                    }
                }
                if (timePassed >= 40) { // If enough time passed, change state to fast spawn
                    spawnTimer = 1.0f;
                    currentState = gameState.fastSpawn;
                }
                break;
            case gameState.fastSpawn: // Spawn enemies every second
                if ((Player)FindFirstObjectByType(typeof(Player)) == null) { // If there are no players left, stop spawning
                    currentState = gameState.finished;
                    return;
                }
                spawnTimer -= Time.deltaTime;
                if (spawnTimer <= 0) {
                    spawnTimer = 1.0f;
                    rng = Random.Range(0, 2); // Decide whether x or y coordinate will be offscreen
                    if (rng == 0) {
                        rng = Random.Range(0, 2); // Decide whether offscreen coordinate will be positive or negative
                        negativeY = rng;
                        if (rng == 0) {
                            negativeY = -1.0f;
                        }
                        Vector3 location1 = new Vector3(Random.Range(-screenLimitX - 3.0f, screenLimitX + 3.0f), Random.Range(screenLimitY, screenLimitY + 3.0f) * negativeY, 0); // Generate coordinates
                        GameObject newEnemy = Instantiate(enemies, location1, Quaternion.identity);
                        float randomSize = Random.Range(1.0f, 1.75f); // Generate random size
                        newEnemy.transform.localScale = new Vector3(randomSize, randomSize, randomSize);
                        newEnemy.GetComponent<NetworkObject>().Spawn(); // Show enemy on every device's screens
                    }
                    else {
                        rng = Random.Range(0, 2); // Decide whether offscreen coordinate will be positive or negative
                        negativeX = rng;
                        if (rng == 0) {
                            negativeX = -1.0f;
                        }
                        Vector3 location1 = new Vector3(Random.Range(screenLimitX, screenLimitX + 3.0f) * negativeX, Random.Range(-screenLimitY - 3.0f, screenLimitY + 3.0f), 0); // Generate coordinates
                        GameObject newEnemy = Instantiate(enemies, location1, Quaternion.identity);
                        float randomSize = Random.Range(1.0f, 1.75f); // Generate random size
                        newEnemy.transform.localScale = new Vector3(randomSize, randomSize, randomSize);
                        newEnemy.GetComponent<NetworkObject>().Spawn(); // Show enemy on every device's screens
                    }
                }
                break;
            case gameState.finished: // Do nothing
                break;
        }
    }
}
