using NPLH;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable 0649

public class StoreManager : Singleton<StoreManager>, IAwaker
{
    [SerializeField] private TileData[] _tileData;
    private Dictionary<string, List<Sprite>> _allSprites;

    public void AwakeLocal()
    {
        Initialize();
    }
    public void Initialize()
    {
        _allSprites = new Dictionary<string, List<Sprite>>();

        var loadedSprites = Resources.LoadAll("Textures/Textures_Resources", typeof(Sprite));
        FillAllSpritesDictionary(GetSpriteKeysList(loadedSprites), loadedSprites);
    }

    private void FillAllSpritesDictionary(List<string> keys, Object[] sprites)
    {
        for (int i = 0; i < keys.Count; i++)
        {
            var spritesByKey = new List<Sprite>();

            foreach (Object obj in sprites)
            {
                if (obj.name.Contains(keys[i])) spritesByKey.Add(obj as Sprite);
            }

            _allSprites.Add(keys[i], spritesByKey);
        }
    }
    private List<string> GetSpriteKeysList(Object[] tempSprites)
    {
        var keys = new List<string>();

        foreach (Object obj in tempSprites)
        {
            var temp = obj.name.Split('_');
            var key = temp[1].Substring(0, temp[1].Length - 1);

            if (!keys.Contains(key)) keys.Add(key);
        }

        return keys;
    }

    public Sprite GetSpriteForLvlUp(string resourceType, int Lvl)
    {
        List<Sprite> sprites;
        _allSprites.TryGetValue(resourceType, out sprites);

        if (Lvl < sprites.Count)
        {
            return sprites[Lvl];
        }
        else
        {
            return sprites[sprites.Count - 1];
        }
    }
    public int GetMaxLevelValueOfSpriteType(string resourceType)
    {
        List<Sprite> spriteType;
        _allSprites.TryGetValue(resourceType, out spriteType);
        return spriteType.Count;
    }
    public TileData GetRandomTileData() 
    {
        return _tileData[Random.Range(0, _tileData.Length)];
    }
}
