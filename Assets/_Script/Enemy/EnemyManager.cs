using System;
using System.Collections;
using System.Collections.Generic;
using _Script.Actors;
using _Script.PersonalAPI.Data.RuntimeSet;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class EnemyManager : MonoBehaviour
{        
    // Pool
    private ObjectPool<GameObject> _pool_enemy;
    [SerializeField] private GameObject _pfb_enemy;
    private const bool _COLLECTION_CHECK = true;
    [SerializeField] private int _enemyDefaultCount;
    [SerializeField] private int _enemyMaxCount;
    private List<GameObject> _instantiatedEnemyList = new();
    // Cache fields
    [SerializeField] private PlayerDataSO _so_playerData;
    [SerializeField] private GameObjectRuntimeSet _so_rs_tilemap_base;
    private Tilemap _baseTilemap;
    
    private void Awake()
    {
        _baseTilemap = _so_rs_tilemap_base.Items[0].GetComponent<Tilemap>();
        CreateEnemyPool();
    }
    
    private void CreateEnemyPool()
    {
        _enemyDefaultCount = _so_playerData.MaxMoveCount;
        _enemyMaxCount = _so_playerData.MaxMoveCount * 5 /*TODO: add max move count in game settings */;
        _pool_enemy = new ObjectPool<GameObject>(CreateEnemy, OnGetEnemyFromPool,
            OnReturnEnemyToPool, OnDestroyEnemy, _COLLECTION_CHECK,
            _enemyDefaultCount, _enemyMaxCount);
    }

    private void OnDestroyEnemy(GameObject obj)
    {
        throw new NotImplementedException();
    }

    private void OnReturnEnemyToPool(GameObject obj)
    {
        throw new NotImplementedException();
    }

    private GameObject CreateEnemy()
    {
        return Instantiate(_pfb_enemy);
    }

    private void OnGetEnemyFromPool(GameObject enemy)
    {
        Vector3 enemyPosition = GetRandomPosition();
        enemy.transform.position = enemyPosition;
        Vector3Int enemyCoord = _baseTilemap.WorldToCell(enemyPosition);
        //enemy.gameObject.GetComponent<Enemy>().Coord = enemyCoord;
        enemy.gameObject.SetActive(true);
        // _movableAttributeChangeTilePos.Raise(enemyCoord);
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