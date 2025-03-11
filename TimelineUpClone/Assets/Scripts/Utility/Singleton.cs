using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    /// <summary>
    /// This bool determines that this singleton is "DontDestroyOnLoad" or not
    /// </summary>
    [SerializeField] private bool _dontDestroy = false;

    static T m_Instance;
    public static T Instance
    {
        get
        {
            if (m_Instance == null)
            {
                m_Instance = FindObjectOfType<T>();
            }
            return m_Instance;
        }
    }

    protected virtual void Awake()
    {
        if (m_Instance == null)
        {
            m_Instance = this as T;
            if (_dontDestroy)
            {
                transform.parent = null;
                DontDestroyOnLoad(this.gameObject);
            }
        }
        else
        {
            if (m_Instance != this as T)
            {
                Destroy(gameObject);
            }
        }
    }
}