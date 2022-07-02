using UnityEngine;
public class Player : MonoBehaviour
{
	public static int tries = 3;
	public static Vector3 intialPos;
	public CharacterController controller;
	public HealthBarController healthBar;
	public float movementSpeed=50f;
	public float gravity=-9.8f;
	public Vector3 velocity;
	public GameObject triesUI;

	public Transform groundCheck;
	public float groundDistance=0.4f;
	public LayerMask groundMask;
	bool isGrounded;
	public float jumpHeight = 5.0f;
	AudioSource walkSound;
	public static GameObject GameOverUI;
	public GameObject WinUI;
    private void Awake()
    {
		EnemyScript.isBossALive = true;
		   GameOverUI = GameObject.Find("GameOver");
		GameOverUI.SetActive(false);
		intialPos = new Vector3(482f, 56.7400017f, -944.900024f);
	}
    private void Start()
    {
		tries = 3;
		Cursor.lockState = CursorLockMode.Locked;
		triesUI.GetComponent<TMPro.TextMeshProUGUI>().text="Tries left: " + tries.ToString();
		walkSound = GetComponent<AudioSource>();
	}
    void Update()
	{
        if (!EnemyScript.isBossALive)
        {
			WinUI.SetActive(true);
			Cursor.lockState = CursorLockMode.None;
			Time.timeScale = 0f;
		}
        else
        {
			if (!PauseMenu.GameIsPaused)
			{
				Walk();
			}
		}
    }
	void Walk()
    {
		PlayWalkSound();
		float x = Input.GetAxis("Horizontal");
		float z = Input.GetAxis("Vertical");

		Vector3 move = transform.right * x + transform.forward * z;//to move right from the new position, not to take global rotation

		//Sprint Script
		DoubleMovementSpeed();
		controller.Move(move * movementSpeed * Time.deltaTime);//walk

		//Jump Script
		Jump();
	}
	void Jump()
    {
		if (isGrounded && velocity.y < 0)
		{
			velocity.y = -10.0f;
		}

		if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
		{
			walkSound.mute=true;
			velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
		}

		//falling Script
		velocity.y += gravity * Time.deltaTime;
		controller.Move(velocity * Time.deltaTime);
	}
	void DoubleMovementSpeed() { 
		if (Input.GetKeyDown(KeyCode.LeftShift))
		{
			walkSound.pitch = 1.25f;
			movementSpeed *= 1.5f;
		}
		if (Input.GetKeyUp(KeyCode.LeftShift))
		{
			walkSound.pitch = 1.0f;
			movementSpeed /= 1.5f;
		}
	}
	public int decreaseTries()
    {
		tries--;

		triesUI.GetComponent<TMPro.TextMeshProUGUI>().text = "Tries left: " + tries.ToString();
		return tries;
	}
	
	public void ResetPlayer()
	{
		Debug.Log("calling reset: " + intialPos);
		transform.position = intialPos;
		Debug.Log("calling postition: " + transform.position);
		healthBar.ResetHealth();
	}
	void PlayWalkSound()
    {
		isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);//true or false
		if(isGrounded)
        {
			walkSound.mute=false;
        }
		if (checkWalkBtnPress() )
		{
			walkSound.Play();
		}
		else if (checkWalkBtnRel())
		{
			walkSound.Stop();
		}
	}
	bool  checkWalkBtnPress()
    {
	return Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow);
	}
	bool checkWalkBtnRel()
	{
		return Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow);
	}
}

