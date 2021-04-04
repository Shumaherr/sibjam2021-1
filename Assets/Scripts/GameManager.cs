using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using Random = UnityEngine.Random;

public enum PlayerStat
{
    Swim,
    Stop,
    Die,
    Sprint
}

public struct PlayerInfo
{
    public float Oxygen { get; set; }

    public float Energy { get; set; }

    public PlayerStat PlayerStatus { get; set; }

    public bool IsLightFlickering { get; set; }
}

public class GameManager : Singleton<GameManager>
{
    public Transform PlayerInstance
    {
        get => _playerInstance;
        set => _playerInstance = value;
    }

    [SerializeField] public Transform playerPrefab;
    [SerializeField] public PlayerInfo playerInfo;
    [SerializeField] public Transform[] enemyPrefabs;
    private Light2D _flashlight;

    private const float EnergyChangePeriod = 1.0f;
    private const float MaxEnergyAmount = 100f;
    private const float MaxOxygenAmount = 100f;
    private const float EnemySpawnPeriod = 5f;

    private Transform _playerInstance;
    private Camera _camera;

    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;
        _flashlight = GetComponent<Light2D>();
        playerInfo.IsLightFlickering = false;
        playerInfo.Energy = MaxEnergyAmount;
        _playerInstance = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(SpawnTimer());
        StartCoroutine(SpawnEnemy());
    }

    private IEnumerator SpawnEnemy()
    {

        while (true)
        {
            yield return new WaitForSeconds(EnemySpawnPeriod);
            
            float spawnX = Random.Range( _camera.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x,
                _camera.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x * 2);
            float spawnY = Random.Range(_camera.ScreenToWorldPoint(new Vector2(0, 0)).y + 20,
                _camera.ScreenToWorldPoint(new Vector2(0, Screen.height)).y );

            Vector2 newEnemyPos = new Vector2(spawnX, spawnY);
            Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)], newEnemyPos,
                Quaternion.identity);
        }
    }

    private IEnumerator SpawnTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(EnergyChangePeriod);
            playerInfo.Energy--;
            switch (playerInfo.PlayerStatus)
            {
                case PlayerStat.Swim:
                    playerInfo.Oxygen -= 2;
                    break;
                case PlayerStat.Stop:
                    playerInfo.Oxygen--;
                    break;
                case PlayerStat.Die:
                    break;
                case PlayerStat.Sprint:
                    playerInfo.Oxygen -= 4;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void LateUpdate()
    {
        CheckStats();
    }

    private void CheckStats()
    {
        if(playerInfo.PlayerStatus == PlayerStat.Die)
            return;
        if (playerInfo.Energy < MaxEnergyAmount * 0.15f)
        {
            playerInfo.IsLightFlickering = true;
        }
    }

    public bool IsSprinting()
    {
        return playerInfo.PlayerStatus == PlayerStat.Sprint;
    }
    
    
}