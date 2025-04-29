using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene"); 
    }

    public void ToMenu()
    {
        SceneManager.LoadScene("menu");
    }

    public void QuitGame()
    {
        Application.Quit(); 
    }
}