using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuManager : MonoBehaviour
{
    public Text bestScoreText;
    public InputField nameInputField;
    // Start is called before the first frame update
    void Start()
    {
        if (DataManager.Instance != null && DataManager.Instance.bestPlayer != null)
        {
            bestScoreText.text = "Best Score: " + DataManager.Instance.bestPlayer + ": " + DataManager.Instance.bestScore;
            nameInputField.text = DataManager.Instance.bestPlayer;
        } else
        {
            bestScoreText.text = "Best Score: None";
        }
        nameInputField.onEndEdit.AddListener(delegate{DataManager.Instance.setCurrPlayer(nameInputField.text);});
    }

    public void StartNewGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

}
