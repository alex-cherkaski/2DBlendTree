using System.Runtime.CompilerServices;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [Header("Speed Related")]
    public float acceleration;
    public float deccelaration;

    [Header("Blend Limits")]
    public float forwardLimit;
    public float backwardsLimit;
    public float rightLimit;
    public float leftLimit;

    private Transform characterTransform;
    private Animator animator;

    private int XBlendHash;
    private int ZBlendHash;

    private float zMovementSpeed;
    private float xMovementSpeed;

    // Start is called before the first frame update
    void Start()
    {
        characterTransform = GetComponent<Transform>();
        animator = GetComponent<Animator>();

        XBlendHash = Animator.StringToHash("X Blend");
        ZBlendHash = Animator.StringToHash("Z Blend");

        xMovementSpeed = 0;
        zMovementSpeed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        bool upArrowPressed = Input.GetKey(KeyCode.UpArrow);
        bool downArrowPressed = Input.GetKey(KeyCode.DownArrow);
        bool rightArrowPressed = Input.GetKey(KeyCode.RightArrow);
        bool leftArrowPressed = Input.GetKey(KeyCode.LeftArrow);

        bool leftShiftPressed = Input.GetKey(KeyCode.LeftShift);

        float x = characterTransform.position.x;
        float y = characterTransform.position.y;
        float z = characterTransform.position.z;

        /* Z Axis Speed Up */

        if (upArrowPressed && leftShiftPressed && zMovementSpeed < forwardLimit)
        {
            zMovementSpeed += acceleration * Time.deltaTime;
            zMovementSpeed = Mathf.Clamp(zMovementSpeed, zMovementSpeed, forwardLimit);
        }

        else if (upArrowPressed && zMovementSpeed < forwardLimit - 1)
        {
            zMovementSpeed += acceleration * Time.deltaTime;
            zMovementSpeed = Mathf.Clamp(zMovementSpeed, zMovementSpeed, forwardLimit - 1);
        }

        if (downArrowPressed && zMovementSpeed > backwardsLimit)
        {
            zMovementSpeed -= deccelaration * Time.deltaTime;
            zMovementSpeed = Mathf.Clamp(zMovementSpeed, backwardsLimit, zMovementSpeed);
        }

        /* X Axis Speed Up */

        if (rightArrowPressed && xMovementSpeed < rightLimit)
        {
            xMovementSpeed += acceleration * Time.deltaTime;
            xMovementSpeed = Mathf.Clamp(xMovementSpeed, xMovementSpeed, rightLimit);
        }

        if (leftArrowPressed && xMovementSpeed > leftLimit)
        {
            xMovementSpeed -= deccelaration * Time.deltaTime;
            xMovementSpeed = Mathf.Clamp(xMovementSpeed, leftLimit, xMovementSpeed);
        }

        /* Z Axis Slow Down */

        if (!leftShiftPressed && zMovementSpeed > forwardLimit - 1)
        {
            zMovementSpeed -= deccelaration * Time.deltaTime;
            zMovementSpeed = Mathf.Clamp(zMovementSpeed, forwardLimit - 1, zMovementSpeed);
        }

        if (!upArrowPressed && zMovementSpeed > 0)
        {
            zMovementSpeed -= deccelaration * Time.deltaTime;
            zMovementSpeed = Mathf.Clamp(zMovementSpeed, 0, zMovementSpeed);
        }

        if (!downArrowPressed && zMovementSpeed < 0)
        {
            zMovementSpeed += acceleration * Time.deltaTime;
            zMovementSpeed = Mathf.Clamp(zMovementSpeed, zMovementSpeed, 0);
        }

        /* X Axis Slow Down */

        if (!rightArrowPressed && xMovementSpeed > 0)
        {
            xMovementSpeed -= deccelaration * Time.deltaTime;
            xMovementSpeed = Mathf.Clamp(xMovementSpeed, 0, xMovementSpeed);
        }

        if (!leftArrowPressed && xMovementSpeed < 0)
        {
            xMovementSpeed += acceleration * Time.deltaTime;
            xMovementSpeed = Mathf.Clamp(xMovementSpeed, xMovementSpeed, 0);
        }

        animator.SetFloat(XBlendHash, xMovementSpeed);
        animator.SetFloat(ZBlendHash, zMovementSpeed);

        x += xMovementSpeed * Time.deltaTime;
        z += zMovementSpeed * Time.deltaTime;

        characterTransform.position = new Vector3(x, y, z);
    }
}
