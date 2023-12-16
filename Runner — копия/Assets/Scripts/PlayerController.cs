using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    public UnityEngine.Vector3 dir;
    [SerializeField] private float jumpForce;
    [SerializeField] private float gravity;
    [SerializeField] private int coins;
    [SerializeField] private GameObject losePanel;
    [SerializeField] private Text coinsText;


    private int lineToMove = 1;
    public float lineDistance = 4;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        Time.timeScale = 1;
        coins = PlayerPrefs.GetInt("coins");
        coinsText.text = coins.ToString();
    }


    private void Update()
    {
        if (SwipeController.swipeRight)
        {
            if (lineToMove < 2)
                lineToMove++;
        }

        if (SwipeController.swipeLeft)
        {
            if (lineToMove > 0)
                lineToMove--;
        }

        if (SwipeController.swipeUp)
        {
            if(controller.isGrounded)
            Jump();
        }

        if (controller.isGrounded)
        {
        }

        UnityEngine.Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;
        if (lineToMove == 0)
            targetPosition += UnityEngine.Vector3.left * lineDistance;
        else if (lineToMove == 2)
            targetPosition += UnityEngine.Vector3.right * lineDistance;

        if (transform.position == targetPosition)
            return;
        UnityEngine.Vector3 diff = targetPosition - transform.position;
        UnityEngine.Vector3 moveDir = diff.normalized * 25 * Time.deltaTime;
        if (moveDir.sqrMagnitude < diff.sqrMagnitude)
            controller.Move(moveDir);
        else
            controller.Move(diff);


    }

    private void Jump()
    {
        dir.y = jumpForce;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        dir.y += gravity * Time.fixedDeltaTime;
        controller.Move(dir * Time.fixedDeltaTime);
    }


    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "obstacle")
        {
            losePanel.SetActive(true);
            Time.timeScale = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.tag);
        if (other.gameObject.tag == "coin")
        {
            coins++;
            PlayerPrefs.SetInt("coins", coins);
            coinsText.text = coins.ToString();
            Destroy(other.gameObject);
        }
    }
}
