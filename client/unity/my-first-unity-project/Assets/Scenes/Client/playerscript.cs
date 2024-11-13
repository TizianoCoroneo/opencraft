using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerscript : MonoBehaviour
{
    public CharacterController characterController;
    public Camera playerCamera;

    [SerializeField] private Networking networking = default;

    [SerializeField] private UserInputManager inputManager = default;
    [SerializeField] private GameManager gameManager = default;

    // Speed of the object movement
    public float moveSpeed;

    public float gravity;
    public float lookSpeed;


    // Variable to store movement input
    private Vector2 moveInput;
    private Vector2 lookInput;


    // Start is called before the first frame update
    void Start() { }

    // Initialize Input System
    private void Awake() { }

    // Enable the input actions when the script is enabled
    private void OnEnable()
    {
        inputManager.MoveEvent += OnMovePerformed;
        inputManager.LookEvent += OnLookPerformed;
        inputManager.FireEvent += OnFirePerformed;
    }

    // Disable the input actions when the script is disabled
    private void OnDisable()
    {
        inputManager.MoveEvent -= OnMovePerformed;
        inputManager.LookEvent -= OnLookPerformed;
        inputManager.FireEvent -= OnFirePerformed;
    }

    // Update the movement input when the 'Move' action is performed
    private void OnMovePerformed(Vector2 move)
    {
        moveInput = move;
    }

    private void OnLookPerformed(Vector2 look)
    {
        lookInput = look;
    }

    private void OnFirePerformed()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // Do not update the player before we log into the server
        if (gameManager.PlayerID == 0)
            return;

        // Do not update the player location if there is no input
        if (moveInput.Equals(Vector2.zero) && lookInput.Equals(Vector2.zero))
            return;

        var ct = playerCamera.transform;

        // Move the object based on player input
        Vector3 moveVector = ct.forward * moveInput.y + ct.right * moveInput.x;
        moveVector *= moveSpeed;
        moveVector.y = -gravity;
        moveVector *= Time.deltaTime;
        characterController.Move(moveVector);

        // Look: Adjust camera
        var ca = ct.localEulerAngles;
        var y = ca.x > 90 ? ca.x - 360 : ca.x;
        var x = ca.y;
        var vertical = Mathf.Clamp(y - (lookInput.y * lookSpeed), -90, 90);
        var horizontal = x + (lookInput.x * lookSpeed);
        ct.localEulerAngles = new(
            vertical,
            horizontal,
            ca.z
        );

        var newPos = gameObject.transform.position;
        networking.SendToServer(new Opencraft.NetCode.ToServer
        {
            IWantMovePlayer = new Opencraft.NetCode.IWantMovePlayer
            {
                PlayerID = gameManager.PlayerID,
                NewPosition = new Opencraft.NetCode.Vec3
                {
                    X = newPos.x,
                    Y = newPos.y,
                    Z = newPos.z,
                }
            }
        });
    }

    public void Teleport(Vector3 newPos)
    {
        var cc = gameObject.GetComponent<CharacterController>();
        cc.enabled = false;
        gameObject.transform.position = newPos;
        cc.enabled = true;
    }
}
