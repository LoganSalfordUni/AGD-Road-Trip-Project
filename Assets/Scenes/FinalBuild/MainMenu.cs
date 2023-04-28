using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Opening");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void SceneTwo()
    {
        SceneManager.LoadScene("CarOne");
    }

    public void SceneThree()
    {
        SceneManager.LoadScene("MontyHallDiner");
    }
    public void SceneFour()
    {
        SceneManager.LoadScene("MontyHallBathroom");
    }
    public void SceneFive()
    {
        SceneManager.LoadScene("MontyHallDinerTwo");
    }
    public void SceneSix()
    {
        SceneManager.LoadScene("CarTwo");
    }
}
