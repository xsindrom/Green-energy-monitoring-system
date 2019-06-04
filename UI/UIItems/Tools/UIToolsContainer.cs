using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;
public class UIToolsContainer : MonoBehaviour {

    [SerializeField]
    protected RectTransform root;

    public RectTransform Root
    {
        get { return root; }
    }

    public virtual void Save()
    {

    }

    public virtual void Load()
    {

    }

    protected virtual void OnEnable()
    {
        Load();
    }

    protected virtual void OnDisable()
    {
        Save();
    }
}
