using NPLH;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Events;

public class StartPoint : Singleton<StartPoint>
{
    private static List<Actor> _actors;

    public UnityAction<Actor> AddToActorsList;
    
    private void Initialize()
    {
        AddToActorsList += OnAddToActors;
        _actors = FindObjectsOfType<Actor>().ToList();
    }
    private void Awake()
    {
        Initialize();
        AwakeActors();
    }

    private void Start()
    {
        StartActors();
    }

    private void AwakeActors()
    {
        foreach (Actor actor in _actors)
        {
            if (actor is IAwaker)
            {
                (actor as IAwaker).AwakeLocal();
            }
        }
    }

    private void StartActors()
    {
        foreach (Actor actor in _actors)
        {
            if (actor is IStarter)
            {
                (actor as IStarter).StartLocal();
            }
        }
    }

    public List<Actor> GetActors() 
    {
        return _actors;
    }

    private void OnAddToActors(Actor actor) 
    {
        if (actor is IAwaker) (actor as IAwaker).AwakeLocal();
        if (actor is IStarter) (actor as IStarter).StartLocal();
        _actors.Add(actor);
    }
}
