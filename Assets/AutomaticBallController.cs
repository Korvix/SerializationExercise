using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticBallController : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Rigidbody>().AddForce(ComputeRandomDirection() * 20, ForceMode.Impulse);
    }

    private Vector3 ComputeRandomDirection()
    {
        return new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f)).normalized;
    }
}
