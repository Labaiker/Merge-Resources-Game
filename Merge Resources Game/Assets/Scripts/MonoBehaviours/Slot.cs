using UnityEngine;
using UnityEngine.EventSystems;


public class Slot : MonoBehaviour, IDropHandler
{
    public GameObject Item
    {
        get
        {
            if (transform.childCount > 0)
            {
                return transform.GetChild(0).gameObject;
            }

            return null;
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (!Item)
        {
            Tile.ItemBeginDragged.transform.SetParent(transform);
        }
        else if (Tile.ItemBeginDragged.GetComponent<Tile>().Type == Item.GetComponent<Tile>().Type && Tile.ItemBeginDragged.GetComponent<Tile>() != Item.GetComponent<Tile>())
        {
            Tile.ItemBeginDragged.transform.SetParent(transform);
            GameManager.Instance.TilesComparison?.Invoke(transform.GetChild(0).GetComponent<Tile>());
        }
    }
}
