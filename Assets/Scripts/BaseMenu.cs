using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class BaseMenu : MonoBehaviour
{
    protected virtual void Update() => CheckEscapeKey();

    protected virtual void CheckEscapeKey()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            SceneManager.LoadSceneAsync("MainMenu");
    }

}