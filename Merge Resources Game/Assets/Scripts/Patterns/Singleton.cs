using UnityEngine;

namespace NPLH
{
    public abstract class Singleton<T> : Actor where T : MonoBehaviour
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<T>();

                    if (_instance == null)
                    {
                        var temp = new GameObject("[" + typeof(T) + "]");
                        _instance = temp.AddComponent<T>();
                        StartPoint.Instance.AddToActorsList(_instance as Actor);
                    }
                }

                return _instance;
            }
        }
    }
}

