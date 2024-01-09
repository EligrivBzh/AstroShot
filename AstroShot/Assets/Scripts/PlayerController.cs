using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour
{
    public float dashSpeed = 20f;
    public float dashDuration = 0.5f;
    public float dashCooldown = 3f;

    private float currentDashCooldown = 0f;
    private bool isDashing = false;

    Rigidbody _rigidbody;
    Vector2 thrustDirection;
    [SerializeField] float thrustForce = 2f;
    [SerializeField] float rotationSpeed = 120f;
    [SerializeField] float brakeStrength = 1f;


    public GameObject gunLeft = null;
    public GameObject gunRight = null;
    public GameObject bulletPrefab = null;
    public GameObject Spaceship = null;
    private bool left = true;
    private bool isPaused = false;

    public Canvas PauseCanvas;
    public Image DashOn;
    public Image DashOff;
    public AudioSource CooldownSound;
    public bool IsCooldown = false;

    public AudioSource GameMusic;

    public static float xBorderLimit, yBorderLimit;
    private List<int> highScores = new List<int>();
    private const int maxHighScores = 8;


    public static int SCORE;

    // Start is called before the first frame update



    void Start()
    {
        //_rigidbody = this.gameObject.GetComponent<Rigidbody>();
        GameMusic.Play();

        _rigidbody = GetComponent<Rigidbody>();

        yBorderLimit = Camera.main.orthographicSize+1;
        xBorderLimit = (Camera.main.orthographicSize+1) * Screen.width / Screen.height;

        highScores.Clear();

        for (int i = 0; i < maxHighScores; i++)
        {
            if (PlayerPrefs.HasKey("HighScore" + i))
            {
                highScores.Add(PlayerPrefs.GetInt("HighScore" + i));
            }
        }
        
    }
    

    // Update is called once per frame
    void Update()
    {

        var newPos = transform.position;
        if (newPos.x > xBorderLimit)
            newPos.x = -xBorderLimit+1;
        else if (newPos.x < -xBorderLimit)
            newPos.x = xBorderLimit-1;
        else if (newPos.y > yBorderLimit)
            newPos.y = -yBorderLimit+1;
        else if (newPos.y < -yBorderLimit)
            newPos.y = yBorderLimit-1;
        transform.position = newPos;


        float rotation = Input.GetAxis("Rotate") * Time.deltaTime * rotationSpeed;
        float thrust = Input.GetAxis("Thrust") * thrustForce * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space)){
            if(left == false){
                GameObject bullet = Instantiate(bulletPrefab, gunRight.transform.position, gunRight.transform.rotation);
                left = true;
            }
            else{
                GameObject bullet = Instantiate(bulletPrefab, gunLeft.transform.position, gunLeft.transform.rotation);
                left = false;
            }
        }

        if(currentDashCooldown <= 0f){
            if(IsCooldown == true){
                Debug.Log("Yes");
                CooldownSound.Play();
                IsCooldown = false;
            }
            DashOn.gameObject.SetActive(true);
            DashOff.gameObject.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if(currentDashCooldown <= 0f){
                currentDashCooldown = dashCooldown;
                DashOn.gameObject.SetActive(false);
                DashOff.gameObject.SetActive(true);
                IsCooldown = true;
                for (float timer = 0; timer < dashDuration; timer += Time.deltaTime)
                {
                    float moveSpeed = isDashing ? dashSpeed : 5f;
                    transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
                }
            }
        }

        if (currentDashCooldown > 0)
        {
            currentDashCooldown -= Time.deltaTime;
        }

        
        if (Input.GetKeyDown(KeyCode.Return)){
            if (isPaused)
            {
                GameMusic.volume = Mathf.Clamp01(GameMusic.volume + 0.4f);
                Time.timeScale = 1f; 
                isPaused = false;
                PauseCanvas.gameObject.SetActive(false);
            }
            else
            {
                GameMusic.volume = Mathf.Clamp01(GameMusic.volume - 0.4f);
                Time.timeScale = 0f; // Paused the game
                isPaused = true;
                PauseCanvas.gameObject.SetActive(true);

            }

        }

         if (isPaused && Input.GetKeyDown(KeyCode.T))
        {
            SceneManager.LoadScene("Menu");

        }
        

        //thrustDirection
        thrustDirection = transform.right;

       // this.gameObject.transform
        transform.Rotate(Vector3.forward, -rotation);

        float currentSpeed = _rigidbody.velocity.magnitude;
        Vector3 brakeForce = -_rigidbody.velocity.normalized * brakeStrength * Time.deltaTime;
        _rigidbody.AddForce(brakeForce);

        //_rigidbody.AddForce((Vector3)(thrustDirection * thrust * thrustSpeed));
        _rigidbody.AddForce(thrust * thrustDirection);



    }

    private void OnCollisionEnter(Collision coll){
        if(coll.gameObject.tag.Equals("Enemy")){
            highScores.Add(SCORE);
            highScores.Sort((a, b) => b.CompareTo(a));

            if (highScores.Count > maxHighScores)
            {
                highScores.RemoveAt(maxHighScores);
            }
            SceneManager.LoadScene("Spaceship");

            for (int i = 0; i < highScores.Count; i++)
            {
                PlayerPrefs.SetInt("HighScore" + i, highScores[i]);
            }

            PlayerPrefs.Save();
        }
    }
}
