using System;
using System.Collections;
using System.Collections.Generic;
using _Script.Actors;
using _Script.Enemy;
using _Script.Level;
using _Script.PersonalAPI.Data.RuntimeSet;
using _Script.Tile;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{        
    // Pool
    private List<ObjectPool<GameObject>> _pool_enemies = new();
    private const bool _COLLECTION_CHECK = true;
    private List<GameObject> _instantiatedEnemyList = new();

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
    
    private void Awake()
    {
        _baseTilemap = _so_rs_tilemap_base.Items[0].GetComponent<Tilemap>();
        CreateEnemyPool();
    }
    
    private void CreateEnemyPool()
    {
        for (int i = 0; i < _so_levelSettings.SpawnableEnemies.Length; i++)
        {
            _so_currentEnemyType = _so_levelSettings.SpawnableEnemies[i].so_Enemy;
            int enemyMaxCount = _so_levelSettings.SpawnableEnemies[i].Count;
            _pfb_enemyGO = _so_levelSettings.SpawnableEnemies[i].so_Enemy.EnemyPrefab;
            _pool_enemies.Add(new ObjectPool<GameObject>(OnCreate, OnGet,
                OnReturn, OnDestroy, _COLLECTION_CHECK,
                enemyMaxCount, enemyMaxCount));
        }
    }

    private void OnDestroy(GameObject obj)
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
        Vector3 enemyPosition = GetRandomPosition();
        enemy.transform.position = enemyPosition;
        Vector3Int enemyCoord = _baseTilemap.WorldToCell(enemyPosition);
        _enemyController = enemy.gameObject.GetComponent<EnemyController>();
        _enemyController.so_Type = _so_currentEnemyType;
        _enemyController.EType = _so_currentEnemyType.EType;
        _enemyController.Coord = enemyCoord;
        _enemyController.CurTileDictIndex = _so_tileDictionary.GetTileData(enemyCoord).DictIndex;
        _enemyController.Initialize();
        enemy.gameObject.SetActive(true);
    }

    
    private Vector3 GetRandomPosition()
    {
        Vector3Int randPos;
        Vector3Int playerCoord = _baseTilemap.WorldToCell(_so_playerData.PlayerCoord);

        do
        {
            randPos = new Vector3Int(Random.Range(playerCoord.x - 4, playerCoord.x + 5),
                Random.Range(playerCoord.y - 4, playerCoord.y + 5), 0);
        } while (Vector3.Distance(randPos, playerCoord) < 3 &&
                 _baseTilemap.HasTile(randPos));

        return _baseTilemap.CellToWorld(randPos);
    }
}