using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
#if UNITY_EDITOR
    using UnityEditor;
#endif

public class MenuUIHandler : MonoBehaviour
{
    public GameObject nameInput;
    public GameObject scoreText;

    private void Start()
    {
        GameManager.Instance.GetHighScore();
        scoreText.GetComponent<TMP_Text>().text = $"Best Score : {GameManager.Instance.HighScoreName} : {GameManager.Instance.HighScore}";
    }

    public void StartNew()
    {
        GameManager.Instance.PlayerName = nameInput.GetComponent<TMP_InputField>().text;
        SceneManager.LoadScene(1);
    }

    public void Leaderboard()
    {
        SceneManager.LoadScene(2);
    }

    public void Exit()
    {
        #if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
        #else
            Application.Quit();
        #endif
    }
}
