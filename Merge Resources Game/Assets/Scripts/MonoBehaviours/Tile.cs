using NPLH;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Tile : MonoBehaviour, IPoolable, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public static GameObject ItemBeginDragged { get; private set; }
    public string Type { get; private set; }
    public int Level { get; private set; }

    [SerializeField] private TileData _data;
    private int _lvlValueToGainScore;
    private Image _image;
    private Vector3 _startPosition;
    private Transform _startParent;

    public void InitTile(TileData data, string name)
    {
        _image = GetComponent<Image>();
        _data = data;
        Type = _data.Type;

        _image.sprite = _data.StartSprite;
        this.name = name;

        int lvl;
        int.TryParse(_image.sprite.name.Substring(_image.sprite.name.Length - 1, 1), out lvl);
        Level = lvl;
        _lvlValueToGainScore = StoreManager.Instance.GetMaxLevelValueOfSpriteType(Type);
    }

    public void LvlUpTile(int inheritedLvl)
    {
        if (inheritedLvl > Level) Level = inheritedLvl;
        Level++;
        _image.sprite = StoreManager.Instance.GetSpriteForLvlUp(Type, Level);
        if (Level == _lvlValueToGainScore - 1)
        {
            GameManager.Instance.GainScores();
            StartCoroutine(DeactivateTileRoutine());
        }
    }

    private IEnumerator DeactivateTileRoutine() 
    {
        gameObject.GetComponent<CanvasGroup>().blocksRaycasts = false;
        yield return new WaitForSeconds(0.5f);
        PoolManager.Instance.GetPool(GetType().ToString()).Deactivate(this);
    }

    #region IDrag methods

    public void OnBeginDrag(PointerEventData eventData)
    {
        ItemBeginDragged = gameObject;
        _startPosition = transform.position;
        _startParent = transform.parent;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
        GameManager.Instance.OnMouseDown?.Invoke();
    }
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1.0f));
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        ItemBeginDragged = null;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        if (transform.parent == _startParent)
        {
            transform.position = _startPosition;
        }
        GameManager.Instance.ClearTile?.Invoke();
    }

    #endregion

    #region Pool methods

    public void OnActivate(object argument = default)
    {
        gameObject.SetActive(true);
        gameObject.GetComponent<CanvasGroup>().blocksRaycasts = true;
        gameObject.transform.SetParent((Transform)argument);
        InitTile(StoreManager.Instance.GetRandomTileData(), name);
    }

    public void OnDeactivate(object argument = default)
    {
        gameObject.transform.SetParent(PoolManager.Instance.GetPool(typeof(Tile).ToString()).transform);
        gameObject.SetActive(false);
    }

    #endregion
}
