using System.Collections;
using UnityEngine;

public class BallScript : MonoBehaviour {
    //zmienne
    //zmienna sprawdzajaca czy pilka jest aktywna 
    private bool ballIsActive;
    //zapamietanna pozycjapileczki
    private Vector3 ballPosition;
    //sila nadana pileczcena poczatku
    public Vector2 ballInitialForce = new Vector2 (100.0f, 300.0f);
    //obiekt playera
    private GameObject playerObject;
    //komponent Rigidbody2D
    private Rigidbody2D rgb2D;
    //zmienna szybkosci pileczki
    public int speed = 100;
    //dzwiek odbicia
    public AudioClip hitSound;
    private AudioSource audioSource;
    //ślad piłeczki
    public TrailRenderer trail;
    // Use this for initialization
    void Start () {
        audioSource = GetComponent<AudioSource> ();
        //zmienna oznaczajaca czy pileczka jest aktywna (w tym przypadku deaktywacja samodzielnosci)
        ballIsActive = false;
        trail.time = 0f;;
       
        //pozycja poczatkowa pileczki
        ballPosition = transform.position;
        //pobranie komponentu Rigidbody2D i zapamietanie go w zmiennej
        rgb2D = gameObject.GetComponent<Rigidbody2D> ();
        //pobranie odniesienia do obiektu playera i zapamietanie go w zmiennej
        playerObject = GameObject.FindGameObjectWithTag ("Player");
    }
   

    float hitFactor (Vector2 ballPos, Vector2 racketPos, float playerWidth) {
        return (ballPos.x - racketPos.x) / playerWidth;
    }

    void OnCollisionEnter2D (Collision2D collision) {
        if (ballIsActive) {

            audioSource.clip = hitSound;
            if (collision.gameObject.tag == "cube") {
                BlockScript bs = collision.gameObject.GetComponent<BlockScript> ();
                audioSource.pitch = (bs.tableOfColors.Length - bs.hitsToKill+ bs.numberOfHits + 1.0f) / bs.tableOfColors.Length;
            } else
                audioSource.pitch = 1;
            audioSource.Play ();

        }

        // udzerzenie w playera
        if (collision.gameObject.tag == "Player") {
            // wyliczanie wspolczynnika odbicia
            float x = hitFactor (transform.position, collision.transform.position, collision.collider.bounds.size.x);

            // wyliczanie znormalizowanego kąta odbicia
            Vector2 dir = new Vector2 (x, 1).normalized;
            // ustawienie szybkości
            rgb2D.velocity = dir * speed;
        }

    }

    // Update is called once per frame
    void Update () {
         //jesli nie jest aktywne samoistne poruszanie pileczki pileczka ma ustawiana pozycje x na identyczna z playerem 
        if (!ballIsActive && playerObject != null) {
            //poruszanie pileczki przez transform 
            ballPosition.x = playerObject.transform.position.x;
            transform.position = ballPosition;
        }
        //jesli nacisniesz klawisz przypisany do wirtualnego klawisza 'jump' zostanie uruchomione poruszanie i odbijanie sie pileczki   
        if ((Input.GetButtonDown ("Jump") == true || Input.GetKeyUp (KeyCode.Mouse0)) && (!ballIsActive)) {
            rgb2D.isKinematic = false;

            //nadanie poczatkowej sily pileczce ; sposob 1
            // rgb2D.AddForce(ballInitialForce);

            //nadanie poczatkowej szybkosci pileczce ; sposob 2
            rgb2D.velocity = new Vector2 (0, 1).normalized * speed;
            //zmianna wartosci przechowywanej w zmiennej na przeciwna
            ballIsActive = !ballIsActive;
            trail.time = .7f;

        }

       

        //wykonywane jesli pileczkawypadnie poza plansz u dolu 
        else if (ballIsActive && transform.position.y < -6f) {
            trail.time = 0f;;
            ballIsActive = !ballIsActive;
            //reset do pozycji poczatkowej
            //pileczki
            ballPosition.x = playerObject.transform.position.x;
            transform.position = ballPosition;
            //playera
            playerObject.transform.position = new Vector2 (0, playerObject.transform.position.y);
            playerObject.GetComponent<PlayerScript> ().ChangeLife (-1);
            rgb2D.isKinematic = true;
        }
    }
}