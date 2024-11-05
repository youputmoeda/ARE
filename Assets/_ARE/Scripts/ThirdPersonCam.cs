using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCam : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Transform orientation;
    [SerializeField] Transform player;
    [SerializeField] Transform playerObj;

    [SerializeField] float rotationSpeed = 1;

    [SerializeField] Transform combatLookAt;

    [SerializeField] GameObject FreeLookCam;
    [SerializeField] GameObject CombatCam;
    [SerializeField] GameObject TopDownCam;

    [SerializeField] GameObject reticle;

    public CameraStyle currentStyle;
    public enum CameraStyle
    {
        Basic,
        Combat,
        TopDown
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // Swith styles
        if (Input.GetKeyDown(KeyCode.Alpha1)) SwitchCameraStyle(CameraStyle.Basic);
        if (Input.GetKeyDown(KeyCode.Alpha2)) SwitchCameraStyle(CameraStyle.Combat);
        if (Input.GetKeyDown(KeyCode.Alpha3)) SwitchCameraStyle(CameraStyle.TopDown);

        // rotate orientation
        Vector3 viewDir = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
        orientation.forward = viewDir.normalized;

        // rotate player object
        if (currentStyle == CameraStyle.Basic || currentStyle == CameraStyle.TopDown)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");
            Vector3 inputDir = orientation.forward * verticalInput + orientation.right * horizontalInput;

            if (inputDir != Vector3.zero)
                playerObj.forward = Vector3.Slerp(playerObj.forward, inputDir.normalized, Time.deltaTime * rotationSpeed);
        }

        else if (currentStyle == CameraStyle.Combat)
        {
            Vector3 dirToCombatLookAt = combatLookAt.position - new Vector3(transform.position.x, combatLookAt.position.y, transform.position.z);
            orientation.forward = dirToCombatLookAt.normalized;

            playerObj.forward = dirToCombatLookAt.normalized;
        }
    }

    private void SwitchCameraStyle(CameraStyle newStyle)
    {
        FreeLookCam.SetActive(false);
        CombatCam.SetActive(false);
        TopDownCam.SetActive(false);
        reticle.SetActive(false);

        if (newStyle == CameraStyle.Basic) FreeLookCam.SetActive(true);
        else if (newStyle == CameraStyle.Combat)
        {
            CombatCam.SetActive(true);
            reticle.SetActive(true);
        }
        else if (newStyle == CameraStyle.TopDown) TopDownCam.SetActive(true);

        currentStyle = newStyle;
    }
}
