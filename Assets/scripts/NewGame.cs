using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewGame : MonoBehaviour {

    public void newGame () {
        //nulliing points
        GameObject.FindGameObjectWithTag ("score").GetComponent<NeverDestroy> ().setPoint (0);
        GameObject.FindGameObjectWithTag ("score").GetComponent<NeverDestroy> ().setLifes (3);
        SceneManager.LoadSceneAsync ("lv 1");
    }

    public void Menu () {
        SceneManager.LoadSceneAsync ("Menu");

    }
    public void Info () {
        SceneManager.LoadSceneAsync ("Info");
    }

    public void Music () {
        GameObject neverDes = GameObject.FindGameObjectWithTag ("score");
        if (neverDes) {
            neverDes.GetComponent<NeverDestroy> ().Mute ();
        }
    }

    private void Update () {
        if (Input.GetKeyDown (KeyCode.Joystick1Button1))
            newGame ();
    }
}