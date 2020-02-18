using NPLH;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#pragma warning disable 0649
public class UIManager : Singleton<UIManager>, IStarter
{
    [SerializeField] private Text _scores;

    public UnityAction<int> UpdateScore;

    public void StartLocal()
    {
        UpdateScore += OnUpdateScore;
    }
    public void OnUpdateScore(int value) 
    {
        _scores.text = value.ToString();
    }

}
