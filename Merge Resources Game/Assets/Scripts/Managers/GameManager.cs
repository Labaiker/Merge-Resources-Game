using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using NPLH;
using UnityEngine.Events;

public class GameManager : Singleton<GameManager>, IStarter
{
    private int _scores = 0;
    private int _scoreGainValuation = 100;

    private Tile _selectedTile;
    private Tile _targetTile;

    public UnityAction OnMouseDown;
    public UnityAction ClearTile;
    public UnityAction<Tile> TilesComparison;
    public UnityAction GainScores;

    public void StartLocal()
    {
        OnMouseDown += OnSetSelectedTile;
        ClearTile += OnClearTiles;
        TilesComparison += OnTilesComparison;
        GainScores += OnGaineScores;
    }
    private void OnSetSelectedTile()
    {
        if (_selectedTile == null) _selectedTile = Tile.ItemBeginDragged.GetComponent<Tile>();
    }
    private void OnTilesComparison(Tile targetTile)
    {
        _targetTile = targetTile;

        if (_targetTile != null)
        {
            if (_selectedTile.Type == _targetTile.Type)
            {
                TilesMerge();
            }
        }

        OnClearTiles();
    }
    private void OnGaineScores()
    {
        _scores += _scoreGainValuation;
        UIManager.Instance.UpdateScore?.Invoke(_scores);
    }
    private void TilesMerge()
    {
        _targetTile.LvlUpTile(_selectedTile.Level);
        PoolManager.Instance.GetPool(_selectedTile.GetType().ToString()).Deactivate(_selectedTile);
    }
    private void OnClearTiles()
    {
        if (_selectedTile != null) _selectedTile = null;
        if (_targetTile != null) _targetTile = null;
    }
}


