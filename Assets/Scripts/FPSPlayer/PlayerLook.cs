using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
	public static float mouseSensitivity=50f; 
	public Transform playerBody;
	private float xRotation=0f;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState=CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX=Input.GetAxis("Mouse X")*mouseSensitivity*Time.deltaTime; //mouse x movement
		float mouseY=Input.GetAxis("Mouse Y")*mouseSensitivity*Time.deltaTime; //mouse y movement

		xRotation-=mouseY;
		xRotation=Mathf.Clamp(xRotation,-90f,90f);
		transform.localRotation=Quaternion.Euler(xRotation,0,0);//rotate around x axis(up is (1,0,0)


        playerBody.Rotate(Vector3.up * mouseX);//rotate around y-axis(up is (0,1,0)
    }
}
