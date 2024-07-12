using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterReceiver : MonoBehaviour
{
    public Animator animator;
    public int receiverID;
    public float brickPresicion = 0.15f;
    public string animationName;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector2 calculateWind(GameObject fallingLetter)
    {
        // return new Vector2(0, 0);
        Vector2 wind = new Vector2();
        float distance = Math.Abs(GetComponent<Transform>().position.y - fallingLetter.GetComponent<Transform>().position.y);
        // print(receiver.GetComponent<Transform>().position.y - fallingLetters[0].First<GameObject>().GetComponent<Transform>().position.y);
        if ((distance) < brickPresicion){
            wind.x = 0;
            wind.y = 0;
        }
        else {

            wind.x = UnityEngine.Random.Range(-distance/2, distance/2);
            wind.y = UnityEngine.Random.Range(-distance/2, distance/2);
        }

        animator.Play(animationName);
        return wind;
    }
}
