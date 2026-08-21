using UnityEngine;
using UnityEngine.InputSystem;

public class ResidentPlayer : MonoBehaviour
{
    [SerializeField] private AudioListener myAudioListener;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float rotationAngleDivide;
    [SerializeField] private float gravity;
    [SerializeField] private Transform rotation_transform;

    private CharacterController characterController;

    private PlayerInput playerInput;

    [SerializeField]
    private Animator animator;

    private void Awake()
    {
        playerInput = new PlayerInput();

        playerVelocity = Vector3.zero;
        characterController = GetComponent<CharacterController>();
    }

    public PlayerInput Get_PlayerInput() => playerInput;

    public void OnEnable()
    {
        playerInput.Enable();
    }

    public void OnDisable()
    {
        playerInput.Disable();
    }

    private void Update()
    {
        //move character
        MovePlayer();
    }

    public Vector3 Get_Movement_Direction(bool _forward_default = false)
    {
        Vector3 _direction = (transform.forward * playerInput.Standard.Movement.ReadValue<Vector2>().y);
        return _direction;
    }

    Vector3 playerVelocity = Vector3.zero;

    public void MovePlayer()
    {
        //if (transform.position.y <= -15) Hit_Entity();

        Vector3 _moveAxis = Get_Movement_Direction();
        // Apply gravity
        playerVelocity.y += gravity * Time.deltaTime;
        print("moveAxis: " + _moveAxis);
        if (playerVelocity.z > 0)
        {
            playerVelocity.z += (gravity * Time.deltaTime);
            if (playerVelocity.z <= 0) playerVelocity.z = 0;
        }

        bool _walk = false;
        if (_moveAxis != Vector3.zero) _walk = true;

        animator.SetBool("Walk", _walk);

        // Move
        Vector3 finalMove = _moveAxis * (moveSpeed) + Vector3.up * playerVelocity.y + (Vector3.back * playerVelocity.z);
        characterController.Move(finalMove * Time.deltaTime);

        rotation_transform.Rotate(new Vector3(0, playerInput.Standard.Movement.ReadValue<Vector2>().x, 0) * rotationSpeed);
        float _new_rotation = Mathf.Round(rotation_transform.localEulerAngles.y / rotationAngleDivide) * rotationAngleDivide;
        print("new rotation: " + _new_rotation);
        transform.eulerAngles = new Vector3(0, _new_rotation, 0);
    }
}
