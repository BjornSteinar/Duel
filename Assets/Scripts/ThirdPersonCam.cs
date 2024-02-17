using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCam : MonoBehaviour
{
    [Header("Keybinds")]
    public KeyCode camSwitch = KeyCode.LeftShift;

    [Header("References")]
    public Transform orientation;
    public Transform player;
    public Transform playerObj;
    public Rigidbody rb;

    public float rotationSpeed;

    public Transform combatLookAt;

    public GameObject mainCam;
    public GameObject thirdPersonCam;
    public GameObject combatCam;

    public CameraStyle currentStyle;
    public enum CameraStyle
    {
        Basic,
        Combat
    }



    private void Start()
    {
        combatCam.SetActive(true);
        thirdPersonCam.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        // switch camera style
        if (Input.GetKeyDown(camSwitch))
        {
            SwitchCameraStyle();
        }

        // rotate orientation
        Vector3 viewDir = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
        orientation.forward = viewDir.normalized;

        // roate player object
        if (thirdPersonCam.activeSelf)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");
            Vector3 inputDir = orientation.forward * verticalInput + orientation.right * horizontalInput;

            if (inputDir != Vector3.zero)
                player.forward = Vector3.Slerp(playerObj.forward, inputDir.normalized, Time.deltaTime * rotationSpeed);
        }

        else if (combatCam.activeSelf)
        {
            Vector3 dirToCombatLookAt = combatLookAt.position - new Vector3(transform.position.x, combatLookAt.position.y, transform.position.z);
            orientation.forward = dirToCombatLookAt.normalized;

            player.forward = dirToCombatLookAt.normalized;
        }
    }
  
    private void SwitchCameraStyle()
    {
        bool tPCcurrentState = thirdPersonCam.activeSelf;
        thirdPersonCam.SetActive(!tPCcurrentState);

        bool cCam = combatCam.activeSelf;
        combatCam.SetActive(!cCam);
    }
}
