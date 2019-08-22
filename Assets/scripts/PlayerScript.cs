using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    //zmienne
    //szybkosc playera
    public int speed = 10;
    //komponent Rigidbody2D
    private Rigidbody2D rgb2D;
    //punkty
    private int score;
    //zycia
    public int life = 3;
    //zmienne tekstu 
    public Text lifesText, pointsText;
    //obiekt niezniszczalny
    private GameObject neverSee;

    public AudioClip pointSound;
    public AudioClip lifeSound;
    // Use this for initialization
    void Start()
    {
        score = 0;
        //pobranie komponentu Rigidbody2D i zapamietanie go w zmiennej
        rgb2D = gameObject.GetComponent<Rigidbody2D>();
    }
    //funkcja do zmiany wyniku
    public void ChangePoints(int changePoints)
    {
        score += changePoints;
        GetComponent<AudioSource>().clip = pointSound;
        GetComponent<AudioSource>().Play();
    }

    //metoda zmieniajaca dane wyswiatlane na ekranie na bierzaco
    void OnGUI()
    {
        if (pointsText != null)
            pointsText.text = "Current score : " + score;
        if (lifesText != null)
            lifesText.text = "Current lifes : " + life;
    }
    //funkcja do zmiany liczby zyc
    public void ChangeLife(int changeLife)
    {
        if (changeLife < 0)
        {
            GetComponent<AudioSource>().clip = lifeSound;
            GetComponent<AudioSource>().Play();
        };
        life += changeLife;
    }
    // Update is called once per frame
    void Update()
    {
        //po nacisnieciu klawisza escape zostaje wylaczona gra
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        if (neverSee == null)
            neverSee = GameObject.FindGameObjectWithTag("score");
        //jesli nie masz ani jednego zycia toplansz sie przeladuje
        if (life <= 0)
        {
            neverSee.GetComponent<NeverDestroy>().setPoint(score);
            SceneManager.LoadSceneAsync("Last");
        }

        //sprawdzenie czy na planszy sa jakies klocki
        GameObject[] cubes = GameObject.FindGameObjectsWithTag("cube");
        if (cubes.Length <= 0)
        {
            neverSee.GetComponent<NeverDestroy>().setPoint(score);
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        }


        //pobranie wartosci wirtualnej osi identyfikowanej przez nazwe osi
        float inputX = Input.GetAxis("Horizontal");
        //nadanie predkosci obiektowi
        rgb2D.velocity = new Vector2(speed * inputX, 0); ;
    }
}
