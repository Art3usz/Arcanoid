using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NeverDestroy : MonoBehaviour
{
    int point;
    int life;
    //komponet 
    AudioSource aS;
    // Use this for initialization
    void Start()
    {
        //zadbanie o to by obiekt nie byl niszczony podczas ladowania nowej sceny
        DontDestroyOnLoad(gameObject);
        aS = gameObject.GetComponent<AudioSource>();
        SceneManager.LoadScene("Menu");
    }
    public int getLifes()
    {
        return life;
    }

      public void setLifes(int life)
    {
       this.life=life;
    }
    public int getPoint()
    {
        return point;
    }
    public void setPoint(int points)
    {
        this.point = points;
    }
    public void Mute()
    {
        aS.mute = !aS.mute;
    }

}
