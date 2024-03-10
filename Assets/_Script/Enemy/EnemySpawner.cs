using System;
using System.Collections.Generic;
using _Script.Actors;
using _Script.Enemy;
using _Script.Level;
using _Script.PersonalAPI.Data.RuntimeSet;
using _Script.Tile;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{        
    // Pool
    private List<ObjectPool<GameObject>> _pool_enemies = new();
    private const bool _COLLECTION_CHECK = true;

    // Reference fields
    [SerializeField] private LevelSettingsSO _so_levelSettings;
    [SerializeField] private PlayerDataSO _so_playerData;
    [SerializeField] private TileDictionarySO _so_tileDictionary;
    [SerializeField] private GameObjectRuntimeSet _so_rs_tilemap_base;
    private Tilemap _baseTilemap;
    
    // Cache fields
    private GameObject _pfb_enemyGO;
    private EnemyController _enemyController;
    private EnemyTypeSO _so_currentEnemyType;
    
    private void Start()
    {
        _baseTilemap = _so_rs_tilemap_base.Items[0].GetComponent<Tilemap>(); 
        CreateEnemyPool();
        SpawnEnemies();
    }
    
    private void CreateEnemyPool()
    {
        for (int i = 0; i < _so_levelSettings.SpawnableEnemies.Length; i++)
        {
            _so_currentEnemyType = _so_levelSettings.SpawnableEnemies[i].so_Enemy;
            int enemyMaxCount = _so_levelSettings.SpawnableEnemies[i].Count;
            _pfb_enemyGO = _so_levelSettings.SpawnableEnemies[i].so_Enemy.pfb_EnemyParent;
            _pool_enemies.Add(new ObjectPool<GameObject>(OnCreate, OnGet,
                OnReturn, OnDestroyEnemy, _COLLECTION_CHECK,
                enemyMaxCount, enemyMaxCount));
        }
    }

    [Button()]
    public void SpawnEnemies()
    {
        for (var i = 0; i < _so_levelSettings.SpawnableEnemies.Length; i++)
        {
            var spawnableEnemy = _so_levelSettings.SpawnableEnemies[i];
            for (var j = 0; j < spawnableEnemy.Count; j++) 
                _pool_enemies[i].Get();
        }
    }
    
    private void OnDestroyEnemy(GameObject obj)
    {
        throw new NotImplementedException();
    }

    private void OnReturn(GameObject obj)
    {
        throw new NotImplementedException();
    }

    private GameObject OnCreate()
    {
        return Instantiate(_pfb_enemyGO);
    }

    private void OnGet(GameObject enemy)
    {
        _enemyController = enemy.GetComponentInChildren<EnemyController>();
        _enemyController.Initialize();
        Vector3 spawnPosition = GetSpawnablePosition();
        enemy.transform.position = spawnPosition;
        Vector3Int enemyCoord = _baseTilemap.WorldToCell(spawnPosition);
        GroundTileData tileItWasSpawnOn = _so_tileDictionary.GetTileData(enemyCoord);
        tileItWasSpawnOn.ThisIsOnIt = WhatIsOnIt.Enemy;
        _enemyController.so_Type = _so_currentEnemyType;
        _enemyController.EType = _so_currentEnemyType.EType;
        _enemyController.Coord = enemyCoord;
        _enemyController.CurTileDictIndex = tileItWasSpawnOn.DictIndex;
        _enemyController.TileUnderTheEnemy = tileItWasSpawnOn;
        _enemyController.Initialize();
        enemy.gameObject.SetActive(true);
    }
    
    private Vector3 GetSpawnablePosition()
    {
        Vector3Int randPos;
        Vector3Int playerCoord = _so_playerData.PlayerCoord;

        do
        {
            randPos = new Vector3Int(
                Random.Range(playerCoord.x - _enemyController.SpawnDistanceFromPlayer * 2,
                    playerCoord.x + _enemyController.SpawnDistanceFromPlayer * 2),
                Random.Range(playerCoord.y - _enemyController.SpawnDistanceFromPlayer * 2,
                    playerCoord.y + _enemyController.SpawnDistanceFromPlayer * 2), 0);
        } while (Vector3.Distance(randPos, playerCoord) < _enemyController.SpawnDistanceFromPlayer ||
                 !_baseTilemap.HasTile(randPos) || _so_tileDictionary.GetTileData(randPos).ThisIsOnIt != WhatIsOnIt.Nothing);

        return _baseTilemap.CellToWorld(randPos);
    }
}