using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] GameObject[] gameObjects; // Array of game objects to be destroyed
    [SerializeField] string winSceneName; // Name of the scene to load when all objects are destroyed

    private int remainingObjects;

    void Start()
    {
        // Initialize the count of remaining game objects to be destroyed
        remainingObjects = gameObjects.Length;

        // Attach the "DestroyableObject" script to each game object in the array
        for (int i = 0; i < gameObjects.Length; i++)
        {
            gameObjects[i].AddComponent<DestroyableObject>();
            gameObjects[i].GetComponent<DestroyableObject>().gameController = this;
        }
    }

    // This function is called when a game object is destroyed
    public void GameObjectDestroyed()
    {
        remainingObjects--;

        if (remainingObjects == 0)
        {
            LoadWinScene();
        }
    }

    void LoadWinScene()
    {
        // Load the specified scene
        SceneManager.LoadScene(winSceneName);
    }
}

public class DestroyableObject : MonoBehaviour
{
    public GameController gameController;

    void OnDestroy()
    {
        if (gameController != null)
        {
            gameController.GameObjectDestroyed();
        }
    }
}
