﻿using System.Collections;
using UnityEngine;

public class BlockScript : MonoBehaviour {
    //zmienne
    //ile razy mozna uderzyc kloec pilka nim zniknie
    public int hitsToKill = 1;
    public GameObject cubeParticles;

    //punkty za zbicie klocka
    private int points = 1;
    //ile razy do tej pory zostal uderzony klocek
    public int numberOfHits = 0;

    public bool setRandomNumber = true;
    //obiekt playera
    private GameObject playerObject;
    //tablica kolorow 
    public Color[] tableOfColors;
    //komponet SpriteRenderer
    SpriteRenderer sr;
    // Use this for initialization

    void Start () {
        sr = gameObject.GetComponent<SpriteRenderer> ();
        if (setRandomNumber)
            hitsToKill = Random.Range (1, tableOfColors.Length);
        if (0 == hitsToKill) {
            Destroy (gameObject);
            return;
        }
        changeColor ();
        playerObject = GameObject.FindGameObjectWithTag ("Player");
        points = hitsToKill * 5;

    }
    private void changeColor () {
        int i = hitsToKill - 1 - numberOfHits;
        if (i >= tableOfColors.Length) i = tableOfColors.Length - 1;
        sr.color = new Color (tableOfColors[i].r, tableOfColors[i].g, tableOfColors[i].b);

    }
    //funkcja urochomiana po wykryciu kolizji / wejsciu w kolizje
    void OnCollisionEnter2D (Collision2D collision) {

        if (collision.gameObject.tag == "ball") {
            numberOfHits++;
            if (numberOfHits == hitsToKill) {
                playerObject.GetComponent<PlayerScript> ().ChangePoints (points);
                Instantiate (cubeParticles, transform.position, Quaternion.Euler (90, 0, 0));
                Destroy (gameObject);
            } else
                changeColor ();
        }
    }

}