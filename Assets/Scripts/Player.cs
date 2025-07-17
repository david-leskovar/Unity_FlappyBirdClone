using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.XR.Haptics;

public class Player : MonoBehaviour
{

    public string state = "Jumping";
    private float gravity = -9.8f;
    private float jumpForce = 3.0f;
    private float verticalVelocity = 0f;
    private int score = 0;
    public GameObject scoreText;
    public event EventHandler OnPlayerDead;

    void Update()
    {

        if (state == "Jumping")

        {

            RaycastHit2D hitright = Physics2D.CircleCast(transform.position,0.5f, Vector2.right,0f); 
            Debug.DrawRay(transform.position, Vector2.left, Color.red);

            if (hitright.collider != null)
            {
                if (hitright.collider.name == "Scorer")
                {
                    score++;
                    hitright.collider.gameObject.SetActive(false);
                    var scoreTextComponent = scoreText.GetComponent<TextMeshPro>();
                    scoreTextComponent.text = "Score: " + score.ToString();
                    Debug.Log("Score: " + score);
                }
                else
                {

                    Debug.Log(transform.position);
                    Debug.Log("Hit right: " + hitright.collider.name);
                    OnPlayerDead?.Invoke(this, EventArgs.Empty);
                    state = "Stopped";
                    return;
                }
            }

            if (Input.GetKeyDown(KeyCode.W))
            {
                Debug.Log("Jumping");
                verticalVelocity = jumpForce;
            }

           
            verticalVelocity += gravity * Time.deltaTime;

           
            transform.Translate(0, verticalVelocity * Time.deltaTime, 0);
        }
        

    }

}