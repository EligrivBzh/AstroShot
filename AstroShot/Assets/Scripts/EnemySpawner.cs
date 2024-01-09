using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    //public GameObject meteorPrefab;
    //public float spawnRatePerMinute = 30;
    //public float spawnRateIncrement = 1;



    [SerializeField] GameObject meteorPrefab;
    [SerializeField] float spawnRatePerMinute = 30;
    [SerializeField] float spawnRateIncrement = 1;



    private float spawnNext = 0;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        // instanciamos enemigos sólo si ha pasado tiempo suficiente desde el último.
        if(Time.time > spawnNext){
            // indicamos cuándo podremos volver a instanciar otro enemigo
            spawnNext = Time.time + (60/spawnRatePerMinute);
            // con cada spawn incrementamos los asteroides por minuto para incrementar la dificultad
            spawnRatePerMinute += spawnRateIncrement;

            //float randX = Random.Range(-8, 8);
            //var randX = Random.Range(-Player.xBorderLimit, Player.xBorderLimit);
            var randX = Random.Range(-12, 12);

            var spawnPosition = new Vector2(randX,transform.position.y);
            //var spawnPosition = new Vector2(randX,Player.xBorderLimit);

            // instanciamos el asteroide en el punto y con el ángulo aleatorios
            Instantiate(meteorPrefab, spawnPosition, Quaternion.identity);
        }
        
    }
}
