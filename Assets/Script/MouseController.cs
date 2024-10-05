using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    [SerializeField] private float jetpackForce = 75.0f;
    [SerializeField] private float forwardMovementSpeed = 3.0f;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void FixedUpdate()
    {
        //dieu khien nhan vat bay len bang nhan giu nut cach
        bool jetpackActive = Input.GetKey(KeyCode.Space);
        if (jetpackActive)
        {
            rb.AddForce(new Vector2(0, jetpackForce));
        }

        //dieu khien nhan vat bay ve phia truoc lien tuc
        Vector2 newVelocity = rb.velocity;
        newVelocity.x = forwardMovementSpeed;
        rb.velocity = newVelocity;

        //
    }
}
