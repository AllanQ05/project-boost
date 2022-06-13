using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 starttingPosition;
    [SerializeField]Vector3 movenVector;
    // [SerializeField][Range(0,1)]
    float movementFactor;
    [SerializeField]float period = 2f;
    // Start is called before the first frame update
    void Start()
    {
        starttingPosition = transform.position;
        // Debug.Log(starttingPosition);
    }

    // Update is called once per frame
    void Update()
    {
        if(period<=Mathf.Epsilon){
            // if(period==0){
            return;
        }
        float cycle = Time.time/period; //ontinually growing over time
        const float tau = Mathf.PI*2; // 6.283
        float rowSinWave = Mathf.Sin(cycle*tau);//going from -1 to 1

        movementFactor = (rowSinWave + 1f)/2f; // recalculate from 0 to 1

        // Debug.Log(rowSinWave);
        Vector3 offet = movenVector*movementFactor;
        transform.position = starttingPosition+offet;
    }
}
