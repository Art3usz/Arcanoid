using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class NewGame : MonoBehaviour
{

    public void newGame()
    {
        SceneManager.LoadSceneAsync("lv1");
    }

    public void Menu()
    {
        SceneManager.LoadSceneAsync("Menu");

    }
    public void Info()
    {
        SceneManager.LoadSceneAsync("Info");
    }
    public void Music()
    {
        GameObject neverDes = GameObject.FindGameObjectWithTag("score");
        if (neverDes)
        {
            neverDes.GetComponent<NeverDestroy>().Mute();
        }
    }
}