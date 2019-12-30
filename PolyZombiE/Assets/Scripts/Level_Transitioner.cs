using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level_Transitioner : MonoBehaviour
{
	public void TransitionLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }
    public void ReplayLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
