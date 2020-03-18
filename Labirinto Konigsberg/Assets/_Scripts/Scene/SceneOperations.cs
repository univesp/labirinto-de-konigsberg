using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneOperations : MonoBehaviour
{
    public static SceneOperations Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
        PlayerPrefs.DeleteKey("tries_total");
    }

    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);        
    }
}
