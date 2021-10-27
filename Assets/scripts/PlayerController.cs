using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour{

    public float speed;
    public double velocity;
    private Rigidbody playerRb;

    private Vector3 jump;
    private Vector3 pos;

    private bool onground = true;
    private bool ingame;

    private int score;
    public Text scoretext;
    public Text coordinatesText;
    public Text velocityText;
    public Text pauseText;
    public Button resumeButton;
    public Button quitButton;

    // Start is called before the first frame update
    void Start(){
        playerRb = this.GetComponent<Rigidbody> ();

        jump = new Vector3(0,10,0);
        resumeButton.onClick.AddListener(ResumeButtonAction);
        quitButton.onClick.AddListener(QuitButtonAction);

        ingame = true;

        score = 0;
        UpdateScoreText();

    }

    // Update is called once per frame
    void FixedUpdate(){
        if(ingame){
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            Vector3 camForward = Camera.main.transform.forward;
            Vector3 camRight = Camera.main.transform.right;

            camForward.y = 0;
            camRight.y = 0;
            camForward = camForward.normalized;
            camRight = camRight.normalized;

            //Vector3 move = new Vector3(moveHorizontal, 0.0f, moveVertical);
            
            if(Input.GetButtonDown("Jump") && onground){
                // Jump
                playerRb.AddForce(jump, ForceMode.Impulse);
                onground = false;
            } else if(Input.GetButtonDown("Cancel")){
                // Pause
                ingame = false;
                playerRb.velocity = Vector3.zero;
                playerRb.angularVelocity = Vector3.zero;
                pauseText.gameObject.SetActive(true);
                resumeButton.gameObject.SetActive(true);
                quitButton.gameObject.SetActive(true);
            }

            UpdateUiInfo();

            playerRb.AddForce((moveHorizontal*camRight + moveVertical*camForward) * speed * Time.deltaTime);

        }
    }

    private void UpdateUiInfo(){
        pos = playerRb.position;
        velocity = Math.Round(playerRb.velocity.magnitude *3.6 ,3);

        coordinatesText.text = " X, Y, Z: "+pos.ToString();
        velocityText.text = " Speed: "+velocity+" kp/h";
    }

    private void OnCollisionEnter(Collision collision){
        // Check if Ball is on the ground

        if(collision.gameObject.tag == "ground"){
            onground = true;
        }
    }

    void OnTriggerEnter(Collider collider){
        // Check for collectibles
        if(collider.gameObject.CompareTag("gem")){
            collider.gameObject.SetActive(false);
            score++;
            UpdateScoreText();
        }
    }

    void UpdateScoreText(){
        scoretext.text = " Score: "+score+"/6";
    }

    void ResumeButtonAction(){
        // Resume Game
        ingame = true;
        pauseText.gameObject.SetActive(false);
        resumeButton.gameObject.SetActive(false);
        quitButton.gameObject.SetActive(false);
    }

    void QuitButtonAction(){
        // Quit Game
        Application.Quit();
    }
}
