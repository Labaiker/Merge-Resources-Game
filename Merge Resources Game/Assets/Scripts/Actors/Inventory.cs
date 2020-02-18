using NPLH;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable 0649

public class Inventory : Actor, IStarter
{
    private const int _inventorySize = 5;

    [SerializeField] private GameObject _slotPrefab;
    [SerializeField] private GameObject _tilePrefab;

    private List<Slot> _slots = new List<Slot>();

    public void StartLocal()
    {
        FillingInventoryWithSlots();
    }

    private void FillingInventoryWithSlots()
    {
        for (int i = 0; i < _inventorySize * _inventorySize; i++)
        {
            var slot = Instantiate(_slotPrefab, transform);
            slot.name = $"Slot[{i + 1}]";
            _slots.Add(slot.GetComponent<Slot>());

            var tileGO = Instantiate(_tilePrefab, slot.transform);
            var tile = tileGO.GetComponent<Tile>();          
            tile.InitTile(StoreManager.Instance.GetRandomTileData(), tile.GetType().ToString() + ": [" + _slots[i].transform.transform.name + "]");
        }
    }
    public void AddTileToInventory() 
    {
        for (int i = 0; i < _slots.Count; i++)
        {
            if (_slots[i].transform.childCount == 0)
            {
                PoolManager.Instance.GetPool(typeof(Tile).ToString()).Activate(_slots[i].transform);

                break;
            }
        }
    }
}
