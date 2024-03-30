using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Transactions;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private const float gravityScale = 9.8f, speedScale = 1f, jumpForce = 8f, turnSpeed = 90f;
    private float verticalSpeed, mouseX, mouseY, currentCameraAngleX;
    private int inversion = -1;
    [SerializeField]
    private CharacterController characterController;
    [SerializeField]
    private GameObject playerCamera;
    [SerializeField]
    private GameObject particBlockObject, tool;
    private const float hitScaleSpeed = 15f;
    private float hitLastTime;
    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    } 

    private void FixedUpdate()
    {
        Rotate();
    }

    private void Update()
    {
        Move();
        RaycastHit hit;
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, 5f));
        {
            if(Input.GetMouseButtonDown(0))
            {
                ObjectInteraction(hit.transform.gameObject);
            }
        }
    }

    private void Rotate()
    {
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");
        transform.Rotate(new Vector3(0f, mouseX * turnSpeed * Time.fixedDeltaTime, 0f));

        currentCameraAngleX += mouseY * Time.fixedDeltaTime * turnSpeed * inversion;
        currentCameraAngleX = Mathf.Clamp(currentCameraAngleX, -60f, 60f);

        playerCamera.transform.localEulerAngles = new Vector3(currentCameraAngleX, 0f, 0f);
    }

    private void Move()
    {
        Vector3 velocity = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        velocity = transform.TransformDirection(velocity) * speedScale;
        if (characterController.isGrounded)
        {
            verticalSpeed = 0f;
            if(Input.GetKey(KeyCode.Space))
            {
                verticalSpeed = jumpForce;
            }
        }
        verticalSpeed -= gravityScale * Time.deltaTime;
        velocity.y = verticalSpeed;
        characterController.Move(velocity * Time.deltaTime);
    }
    private void Dig(Block block)
    {
        if(Time.time - hitLastTime > 1 / hitScaleSpeed)
        {
            tool.GetComponent<Animator>().SetTrigger("attack");
            hitLastTime = Time.time;
            block.health -= tool.GetComponent<Tool>().damageToBlock;
            GameObject particleObj = Instantiate(particBlockObject, block.transform.position, Quaternion.identity);
            particBlockObject.GetComponent<ParticleSystemRenderer>().material = block.GetComponent<MeshRenderer>().material;

            if(block.health <= 0)
            {
                block.OnDestroyBehaviour();
            }
        }
    }
    private void ObjectInteraction(GameObject currentObj)
    {
        switch(currentObj.tag)
        {
            case "Block":
                Dig(currentObj.GetComponent<Block>());
                break;
            case "Enemy":
                break;
        }
    }

}
