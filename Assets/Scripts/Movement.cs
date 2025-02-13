using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float walkSpeed = 3f;
    [HideInInspector] public Vector3 movementDirection;
    float _inputX, _inputZ;


    Rigidbody _rb;

    [SerializeField] float groundYOffset;
    [SerializeField] private LayerMask groundLayer;


    Vector3 _velocity;

    private Vector3 _spherePosition;

    // [SerializeField] private float turnSpeed = 100f;
    // [SerializeField] private float fastTurnSpeed = 200f;


    [SerializeField] private Transform rootCharacter;

    private PlayerInputs _inputs;
    private CharacterController _characterController;


    private float _angleVelocity;
    private float _movementVelocity;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // _inputs = GetComponent<PlayerInputs>();
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // float horizontalSpeed = _inputs.IsRunning ? runSpeed : walkSpeed;
        //float _turnSpeed = _inputs.IsRunning ? fastTurnSpeed : this.turnSpeed;
        //transform.Rotate(Vector3.up, _inputs.Move.x * 100 * Time.deltaTime);
        //_rb.SimpleMove(transform.forward * (_inputs.Move.y * horizontalSpeed));
        // Vector3 move = new Vector3(0, 0, 1) * _inputs.Move * (walkSpeed * Time.deltaTime);
        // _rb.MovePosition(move);
        GetDirectionAndMove();
    }

    private void GetDirectionAndMove()
    {
        _inputX = Input.GetAxis("Horizontal");
        _inputZ = Input.GetAxis("Vertical");

        movementDirection = transform.forward * _inputZ + transform.right * _inputX;
        //_rb.AddForce(Vector2.up *walkSpeed , ForceMode.Impulse);
        _rb.linearVelocity = new Vector3(Input.GetAxis("Horizontal") * walkSpeed, _rb.linearVelocity.y,Input.GetAxis("Vertical") * walkSpeed);
    }
}