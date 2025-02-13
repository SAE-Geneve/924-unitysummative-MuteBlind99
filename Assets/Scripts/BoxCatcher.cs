using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

public class BoxCatcher : MonoBehaviour
{

    [SerializeField] private Transform catchPoint;

    private Box _catchedBox;
    private Rigidbody _catchedBoxRb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_catchedBox != null && _catchedBox.TryGetComponent(out Rigidbody _rigidbody))
        {
            _rigidbody.position = catchPoint.position;
        }
    }

    public void InputCatch(InputAction.CallbackContext context)
    {
        if (context.performed)
            CatchBox();
        else if (context.canceled)
            ReleaseBox();
    }

    void CatchBox()
    {
        var hits = Physics.SphereCastAll(transform.position, 1, Vector3.forward);

        foreach (var hit in hits)
        {
            if (hit.collider.TryGetComponent(out Box box))
            {
                _catchedBox = box;
                ParentConstraint pc = _catchedBox.AddComponent<ParentConstraint>();
                pc.constraintActive = true;
                
                ConstraintSource source = new ConstraintSource();
                source.sourceTransform = catchPoint;
                source.weight = 1;
                
                pc.AddSource(source);

                _catchedBox.GetComponent<Rigidbody>().isKinematic = true;
                
                return;

            }
        }

    }

    void ReleaseBox()
    {
        
        if (_catchedBox != null)
        {
            ParentConstraint pc = _catchedBox.GetComponent<ParentConstraint>();
            Destroy(pc);
            
            _catchedBox.GetComponent<Rigidbody>().isKinematic = false;
            _catchedBox = null;
            
        }

    }


}
