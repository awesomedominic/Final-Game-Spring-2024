using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int score = 0;
    public float JumpForce = 10f;
    public float GravityModifier = 1f;
    public float turnSpeed = 20f;
    public float moveSpeed = 1f;
    public float OutOfBounds = -10f;
    public bool isOnGround = true;
    Vector3 m_Movement;
    Rigidbody m_Rigidbody;
    Quaternion m_Rotation = Quaternion.identity;
    private Vector3 _startingPosition;
    private Vector3 _checkpointPosition;
    private Rigidbody _playerRb;
    private bool _isAtCheckpoint = false;

    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        m_Movement.Set(horizontal, 0f, vertical);
        m_Movement.Normalize();

        bool hasHorizontalInput = !Mathf.Approximately (horizontal, 0f);
        bool hasVerticalInput = !Mathf.Approximately (vertical, 0f);
        bool isWalking = hasHorizontalInput || hasVerticalInput;

        Vector3 desiredForward = Vector3.RotateTowards (transform.forward, m_Movement, turnSpeed * Time.deltaTime, 0f);
        m_Rotation = Quaternion.LookRotation (desiredForward);

        m_Rigidbody.MovePosition (m_Rigidbody.position + m_Movement * moveSpeed * Time.deltaTime);
        m_Rigidbody.MoveRotation (m_Rotation);
    }

    // Start is called before the first frame update
    void Start()
    {
        _playerRb = GetComponent<Rigidbody>();
        Physics.gravity *= GravityModifier;
        m_Rigidbody = GetComponent<Rigidbody>();
        _startingPosition = transform.position; 
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            _playerRb.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
            isOnGround = false;
        }

        if(transform.position.y < OutOfBounds)
        {
            if(_isAtCheckpoint)
            {
                transform.position = _checkpointPosition;
            }
            else
            {
                transform.position = _startingPosition;
            }
        }
            
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
        }

        if (collision.gameObject.CompareTag("Obstacle"))
        {
            transform.position = _startingPosition;

             if(_isAtCheckpoint)
            {
                transform.position = _checkpointPosition;
            }
            else
            {
                transform.position = _startingPosition;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Checkpoint"))
        {
            _isAtCheckpoint = true;
            _checkpointPosition = other.gameObject.transform.position;
        }

        if(other.gameObject.CompareTag("Endpoint"))
        {
            transform.position = _startingPosition;
            _isAtCheckpoint = false;
        }

        if(other.gameObject.CompareTag("Collectable"))
        {
            score++;
            //scoreText.text = "Score: " + score.ToString();
            Destroy(other.gameObject);
        }
    }
}
