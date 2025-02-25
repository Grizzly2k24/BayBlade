using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class pohyb : MonoBehaviour
{
    private Rigidbody rb;
    public float rotationSpeed = 100f; // Rychlost otáèení
    PlayersInputMap inputActions;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // Na zaèátku zamkni rotaci, aby byla pyramida stabilní
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        inputActions = new PlayersInputMap();
        inputActions.Enable();
    }

    void Update()
    {
        // Neustálé otáèení pyramidy kolem její osy Y
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);

        // Pohyb pyramidy pomocí šipek
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");


        //float moveHorizontal = inputActions.Player1.Movement.ReadValue<Vector2>().x;
        //float moveVertical = inputActions.Player1.Movement.ReadValue<Vector2>().y;

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.AddForce(movement * 10f); // Mùžeš upravit rychlost pohybu
    }

    void OnCollisionEnter(Collision collision)
    {
        // Pøi kolizi odemkni rotaci, aby pyramida mohla spadnout
        rb.constraints = RigidbodyConstraints.None;
    }
}



