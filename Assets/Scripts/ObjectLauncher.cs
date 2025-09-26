using UnityEngine;
using UnityEngine.InputSystem;

public class ObjectLauncher : MonoBehaviour
{
    [SerializeField] private GameObject _defaultObjectToThrowPrefab;
    [SerializeField] private int _acceleration;
    private bool _launch = false;

#if ENABLE_INPUT_SYSTEM
    private PlayerInput playerInput;

	private void Start()
	{
		playerInput = gameObject.GetComponent<PlayerInput>();
        playerInput.onActionTriggered += ctx => TriggerLaunch(ctx);
        
	}

    private void TriggerLaunch(InputAction.CallbackContext ctx)
    {
        _launch = true;
    }
#endif

    // Update is called once per frame
    void Update()
    {
#if ENABLE_LEGACY_INPUT_MANAGER
        if (Input.GetKeyDown(KeyCode.B))
        {
            _launch = true;
        }
#endif
    }

	private void FixedUpdate()
	{
        if (_launch)
            ThrowObject();
	}

    private void ThrowObject()
    {
        _launch = false;

        GameObject throwableObject = Instantiate(_defaultObjectToThrowPrefab, transform.position, transform.rotation);
        Rigidbody throwBody = throwableObject.GetComponent<Rigidbody>();

		AddThrowForce(throwableObject, throwBody);
	}

    public void ThrowObject(GameObject objectToThrow)
    {
        _launch = false;

        AddThrowForce(objectToThrow, objectToThrow.GetComponent<Rigidbody>()); 
    }

    private void AddThrowForce(GameObject throwableObject, Rigidbody throwBody)
    {
        Rigidbody objectRigidBody = throwableObject.GetComponent<Rigidbody>();

        if (objectRigidBody != null)
        {
            objectRigidBody.isKinematic = false;
            throwBody = throwableObject.GetComponentInChildren<Rigidbody>();
        }

		throwBody.AddForce(transform.forward * _acceleration * throwBody.mass, ForceMode.Impulse);
	}
}
