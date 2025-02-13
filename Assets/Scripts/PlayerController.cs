using UnityEngine;

public class PlayerController : MonoBehaviour
{
   [SerializeField] private float walkSpeed = 3f;
           [SerializeField] private float runSpeed = 10f;
           [HideInInspector] public Vector3 movementDirection;
           float _inputX, _inputZ;
   
   
           // Rigidbody _rb;
   
           [SerializeField] float groundYOffset;
           [SerializeField] private LayerMask groundLayer;
   
           //Earth gravity ~9,81 m/s2
           [SerializeField] float gravity = -9.81f;
           Vector3 _velocity;
   
           private Vector3 _spherePosition;
   
           [SerializeField] private float turnSpeed = 100f;
           [SerializeField] private float fastTurnSpeed = 200f;
   
           // [SerializeField] private bool isRootMotionned = false;
           [SerializeField] private Transform rootCharacter;
   
           private PlayerInputs _inputs;
           private CharacterController _characterController;
           
   
           private float _angleVelocity;
           // private float _inputMouseY;
           // private float _inputMouseX;
   
          
           // public IdleState IdleState = new IdleState();
           // public RunningState RunState = new RunningState();
   
          
           private float _movementVelocity;
   
           // Start is called once before the first execution of Update after the MonoBehaviour is created
           void Start()
           {
               _inputs = GetComponent<PlayerInputs>();
               _characterController = GetComponentInChildren<CharacterController>();
               
           }
   
           // Update is called once per frame
           void Update()
           {
               float horizontalSpeed = _inputs.IsRunning ? runSpeed : walkSpeed;
               if (_inputs.Move.magnitude >= Mathf.Epsilon)
               {
                   //animator.SetBool("Walking", true);
                   if (!_inputs.IsAiming)
                   {
                       if (_inputs.IsRunning == true)
                       {
                           _characterController.SimpleMove(transform.forward * (_inputs.Move.y * horizontalSpeed*2));
                       }
                       //float horizontalSpeed = _inputs.IsRunning ? runSpeed : walkSpeed;
                      // animator.SetFloat("vtInput", _inputs.Move.magnitude * horizontalSpeed);
                   }
                   else
                   {
                       //animator.SetFloat("hzInput", Mathf.SmoothDamp(animator.GetFloat("hzInput"),_inputs.Move.x,ref _movementVelocity, 0.25f));
                   
                       //animator.SetFloat("vtInput",Mathf.SmoothDamp(animator.GetFloat("vtInput"),_inputs.Move.y,ref _movementVelocity, 0.25f));
                   }
               }
               else
               {
                   //animator.SetBool("Walking", false);
                   //animator.SetFloat("vtInput", 0f);
                   //animator.SetFloat("hzInput", 0f);
               }
   
   
               float _turnSpeed = _inputs.IsRunning ? fastTurnSpeed : this.turnSpeed;
                transform.Rotate(Vector3.up, _inputs.Move.x * _turnSpeed * Time.deltaTime);
               
               
               _characterController.SimpleMove(transform.forward * (_inputs.Move.y * horizontalSpeed));
               
              
   
   
               GetDirectionAndMove();
               RotateCameraPlayer();
               Gravity();
           }

           private void RotateCameraPlayer()
           {
               //Player facing away from camera
               float targetAngle = Camera.main.transform.rotation.eulerAngles.y;
               targetAngle += Mathf.Atan2(_inputs.Move.x, _inputs.Move.y) * Mathf.Rad2Deg;

               float actualAngle = Mathf.SmoothDampAngle(rootCharacter.eulerAngles.y, targetAngle, ref _angleVelocity, 0.25f);

               rootCharacter.rotation = Quaternion.Euler(0, actualAngle, 0);
           }

           void GetDirectionAndMove()
           {
               _inputX = Input.GetAxis("Horizontal");
               _inputZ = Input.GetAxis("Vertical");
               // _inputMouseX = Input.GetAxis("Mouse X");
               // _inputMouseY = Input.GetAxis("Mouse Y");
   
               float targetAngle = Camera.main.transform.rotation.eulerAngles.y;
               targetAngle += Mathf.Atan2(_inputs.Move.x, _inputs.Move.y) * Mathf.Rad2Deg;
               
               float actualAngle =
                   Mathf.SmoothDampAngle(rootCharacter.eulerAngles.y, targetAngle, ref _angleVelocity, 0.25f);
               
               rootCharacter.rotation = Quaternion.Euler(0, actualAngle, 0);
               
               
               movementDirection = Camera.main.transform.forward * _inputZ + Camera.main.transform.right * _inputX;
               _characterController.Move(movementDirection * (walkSpeed * Time.deltaTime));
              
              
           }
   
           bool IsGrounded()
           {
               _spherePosition = new Vector3(transform.position.x, transform.position.y - groundYOffset, transform.position.z);
               //Player touching ground
               if (Physics.CheckSphere(_spherePosition, _characterController.radius, groundLayer)) return true;
               return false;
           }
   
           void Gravity()
           {
               if (!IsGrounded()) _velocity.y += gravity * Time.deltaTime;
   
               else if (_velocity.y < 0) _velocity.y = -2;
   
               _characterController.Move(_velocity * Time.deltaTime);
           }

}
