using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SetScore : MonoBehaviour
{

    public Text score;
    GameObject neverSee;
    void Start()
    {
        neverSee = GameObject.FindGameObjectWithTag("score");
        if (score != null)
            score.text = "Your score is : " + neverSee.GetComponent<NeverDestroy>().getPoint();
    }
}
