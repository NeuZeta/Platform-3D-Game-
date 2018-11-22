using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public Transform[] spawnPositions;
    public GameObject[] enemiesToSpawn;
    public GameObject energyBlast;
    public AudioClip deathWindSound;


    private int enemyActivated = 0;
    private int totalEnemies;

    // Use this for initialization
	void Start () {
        totalEnemies = enemiesToSpawn.Length;

        InvokeRepeating("SpawnNewEnemy", 1, 1000*Time.deltaTime);
	}
	


    void SpawnNewEnemy()
    {
        if (enemyActivated < totalEnemies)
        {
            enemiesToSpawn[enemyActivated].transform.position = spawnPositions[Random.Range(0, spawnPositions.Length)].position;
            Instantiate(energyBlast, enemiesToSpawn[enemyActivated].transform);
            enemiesToSpawn[enemyActivated].SetActive(true);
            AudioSource.PlayClipAtPoint(deathWindSound, enemiesToSpawn[enemyActivated].transform.position);
        }

        enemyActivated++;
    }



}
