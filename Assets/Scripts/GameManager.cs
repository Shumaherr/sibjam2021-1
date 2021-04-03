﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

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
    [SerializeField] public Transform playerPrefab;
    [SerializeField] public PlayerInfo playerInfo;
    private Light2D _flashlight;

    private const float EnergyChangePeriod = 1.0f;
    private const float MaxEnergyAmount = 5;
    private const float MaxOxygenAmount = 100f;

    private GameObject _playerInstance;

    private
        // Start is called before the first frame update
        void Start()
    {
        _flashlight = GetComponent<Light2D>();
        playerInfo.IsLightFlickering = false;
        playerInfo.Energy = MaxEnergyAmount;
        StartCoroutine(SpawnTimer());
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
}