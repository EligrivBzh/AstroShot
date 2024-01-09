using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Bullet : MonoBehaviour
{

    public float speed = 10f;
    public float maxLifeTime = 6f;

    public Vector3 targetVector;

    private GameObject scoreText;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, maxLifeTime);
        scoreText = GameObject.FindGameObjectWithTag("GameController");
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(speed * Time.deltaTime * targetVector);
    }

    private void OnCollisionEnter(Collision coll){
        if(coll.gameObject.tag.Equals("Enemy")){
            Destroy(coll.gameObject);
            IncreaseScore();
        }

        Destroy(gameObject);
    }

    private void IncreaseScore(){

        PlayerController.SCORE++;
        scoreText.GetComponent<TextMeshProUGUI>().text = "Score : " + PlayerController.SCORE;

    }
}
