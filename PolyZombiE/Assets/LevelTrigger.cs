using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;

public class LevelTrigger : MonoBehaviour {
    [SerializeField] string nextLevel = "";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Assert.IsFalse(nextLevel == "");
        SceneManager.LoadScene(nextLevel);
    }
}
