using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelection : MonoBehaviour
{
    // Create an array of Buttons to represent each level button
    public Button[] levelButtons;

    private void Start()
    {
        // Loop through the levelButtons array and add listeners to each button
        for (int i = 0; i < levelButtons.Length; i++)
        {
            int levelIndex = i + 1; // Level indices start from 1
            levelButtons[i].onClick.AddListener(() => LoadLevel(levelIndex));
        }
    }

    private void LoadLevel(int levelIndex)
    {
        string sceneName = "Level " + levelIndex;
        SceneManager.LoadScene(sceneName);
    }
}
