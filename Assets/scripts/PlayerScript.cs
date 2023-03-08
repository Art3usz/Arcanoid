using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour {
    //zmienne
    //szybkosc playera
    public float speed = 10;
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
    public float dragDistance = .3f;
    public AudioClip pointSound;
    public AudioClip lifeSound;
    private Vector2 mousePos;
    private bool isActiveCotroll;

    // Use this for initialization
    void Start () {
        if (neverSee == null)
            neverSee = GameObject.FindGameObjectWithTag ("score");
        score = neverSee.GetComponent<NeverDestroy> ().getPoint ();
        life=neverSee.GetComponent<NeverDestroy> ().getLifes();
        //pobranie komponentu Rigidbody2D i zapamietanie go w zmiennej
        rgb2D = gameObject.GetComponent<Rigidbody2D> ();
        isActiveCotroll = false;
        StartCoroutine (FirstActivation ());
    }

    IEnumerator FirstActivation () {
        yield return new WaitForSeconds (2f);
        isActiveCotroll = true;
    }

    //funkcja do zmiany wyniku
    public void ChangePoints (int changePoints) {
        score += changePoints;
        GetComponent<AudioSource> ().clip = pointSound;
        GetComponent<AudioSource> ().Play ();
    }

    //metoda zmieniajaca dane wyswiatlane na ekranie na bierzaco
    void OnGUI () {
        if (pointsText != null)
            pointsText.text = "Current score : " + score;
        if (lifesText != null)
            lifesText.text = "Current lifes : " + life;
    }
    //funkcja do zmiany liczby zyc
    public void ChangeLife (int changeLife) {
        if (changeLife < 0) {
            GetComponent<AudioSource> ().clip = lifeSound;
            GetComponent<AudioSource> ().Play ();
        };
        life += changeLife;
    }
    // Update is called once per frame
    void Update () {
        //po nacisnieciu klawisza escape zostaje wylaczona gra
        if (Input.GetKeyDown (KeyCode.R)||Input.GetKeyDown (KeyCode.Joystick1Button1)) {
            //Application.Quit();
            SceneManager.LoadScene ("Menu");
        }
        if (neverSee == null)
            neverSee = GameObject.FindGameObjectWithTag ("score");
        //jesli nie masz ani jednego zycia toplansz sie przeladuje
        if (life <= 0) {
            neverSee.GetComponent<NeverDestroy> ().setPoint (score);
            SceneManager.LoadSceneAsync ("Last");
        }

        //sprawdzenie czy na planszy sa jakies klocki
        GameObject[] cubes = GameObject.FindGameObjectsWithTag ("cube");
        if (cubes.Length <= 0) {
            neverSee.GetComponent<NeverDestroy> ().setPoint (score);
             neverSee.GetComponent<NeverDestroy> ().setLifes(life);
            SceneManager.LoadSceneAsync (SceneManager.GetActiveScene ().buildIndex + 1);
            //SceneManager.LoadSceneAsync ("lv random 1");
        }
        if (isActiveCotroll) {

            //mouse control
            if (Input.GetKey (KeyCode.Mouse0)) {
                mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
                if (dragDistance < Mathf.Abs (transform.position.x - mousePos.x)) {
                    if (transform.position.x < (mousePos.x)) {
                        rgb2D.velocity = new Vector2 (speed, 0);
                    } else if (transform.position.x > mousePos.x) {
                        rgb2D.velocity = new Vector2 (-speed, 0);
                    }
                } else rgb2D.velocity = new Vector2 (0, 0);
            } else {
                //pobranie wartosci wirtualnej osi identyfikowanej przez nazwe osi
                float inputX = Input.GetAxis ("Horizontal");
                //nadanie predkosci obiektowi
                rgb2D.velocity = new Vector2 (speed * inputX, 0);
            }
        }
    }

    private void FixedUpdate () {
        speed += 0.0001f;
    }
}