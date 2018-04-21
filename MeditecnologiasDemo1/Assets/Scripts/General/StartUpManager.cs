using UnityEngine;
using UnityEngine.SceneManagement;

public class StartUpManager : MonoBehaviour
{
    private void Awake()
    {
        SceneManager.LoadScene("Main");
    }
}
