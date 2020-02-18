using UnityEngine;

#pragma warning disable 0649

[CreateAssetMenu(menuName = "Tiles/Tile", fileName = "New Tile")]
public class TileData : ScriptableObject
{
    [SerializeField] private string _type;
    [SerializeField] private Sprite _startSprite;
    public Sprite StartSprite
    {
        get { return _startSprite; }
        protected set { }
    }
    public string Type 
    {
        get { return _type; }
        private set { }
    }
}
