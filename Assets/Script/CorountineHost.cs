using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorountineHost : MonoBehaviour
{
    public static CorountineHost Instance
    {
        get
        {
            if (m_Instance == null)
            {
                m_Instance = (CorountineHost)FindObjectOfType(typeof(CorountineHost));
                
                if (m_Instance == null)
                {
                    GameObject go = new GameObject();
                    m_Instance = go.AddComponent<CorountineHost>();
                }
                DontDestroyOnLoad(m_Instance.gameObject);
            }
            return m_Instance;
        }
    }
    private static CorountineHost m_Instance = null;
}
