using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Win : MonoBehaviour
{
    [SerializeField] private Button nextLevel;

    private void Start()
    {
        nextLevel.onClick.AddListener(NextLevel);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene("Start Menu");
    }
}
