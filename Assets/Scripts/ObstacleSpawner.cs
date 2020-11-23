using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject obstaclePrefab;

    public float spawnDelayMin;
    public float spawnDelayMax;

    public float scrollingSpeed;

    private float spawnDelay;

    public GameController gameController;

    private void OnEnable()
    {
        gameController.GameStarted += OnGameStarted;
    }

    private void OnDisable()
    {
        gameController.GameStarted -= OnGameStarted;
    }

    private void OnGameStarted(object sender, EventArgs e)
    {
        StartCoroutine(ISpawnLoop());
    }

    private void SpawnObstacle()
    {
        GameObject obstacleGameObject = Instantiate(obstaclePrefab) as GameObject;
        Obstacle obstacle = obstacleGameObject.GetComponent<Obstacle>();
        obstacle.gameController = gameController;
        obstacle.scrollingSpeed = scrollingSpeed;
    }

    private IEnumerator ISpawnLoop()
    {
        while (gameController.IsPlaying)
        {
            SpawnObstacle();
            spawnDelay = Random.Range(spawnDelayMin, spawnDelayMax);
            yield return new WaitForSeconds(spawnDelay);
        }
    }
}
