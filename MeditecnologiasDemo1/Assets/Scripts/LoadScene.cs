using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour {

    [Tooltip("Default label string")]
    public string Default;

    public void Load()
    {
        SceneManager.LoadScene(Default);
    }
}
