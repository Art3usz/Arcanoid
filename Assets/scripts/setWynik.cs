using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class setWynik : MonoBehaviour
{

    public Text wynik;
    GameObject neverSee;
    void Start()
    {
        neverSee = GameObject.FindGameObjectWithTag("score");
        if (wynik != null)
            wynik.text = "Twoj wynik końcowy to : " + neverSee.GetComponent<NeverDestroy>().getPoint();
    }
}
