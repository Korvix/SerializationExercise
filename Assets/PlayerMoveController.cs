using UnityEngine;

public class PlayerMoveController : MonoBehaviour
{
    private Rigidbody ballRigidbody;
    [SerializeField]
    private float speed = 5;
    [SerializeField]
    private float jumpFactor = 5;
    [SerializeField]
    private float brakeFactor = 2f;

    void Start()
    {
        ballRigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        ballRigidbody.AddForce(movement * speed);

        if (Input.GetButton("Brake"))
        {
            ballRigidbody.AddForce(-brakeFactor * ballRigidbody.velocity);
        }

        if (Input.GetButtonDown("Jump"))
        {
            ballRigidbody.AddForce(Vector3.up * jumpFactor, ForceMode.Impulse);
        }
    }
}