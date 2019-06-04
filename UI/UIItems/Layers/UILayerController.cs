using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILayerController : MonoBehaviour {

    [SerializeField]
    protected string id;
    [SerializeField]
    protected List<UILayer> layers = new List<UILayer>();

    public string Id
    {
        get { return id; }
        set { id = value; }
    }

    public List<UILayer> Layers
    {
        get { return layers; }
    }

    public virtual void LoadLayers(string id, Vector2 regionMinCoords, Vector2 regionMaxCoords)
    {
        this.id = id;
        for (int i = 0; i < layers.Count; i++)
        {
            var layer = layers[i];
            layer.Init(regionMinCoords, regionMaxCoords);
            layer.Load(id);
        }
    }

    public virtual void UnloadLayers()
    {
        for (int i = 0; i < layers.Count; i++)
        {
            var layer = layers[i];
            layer.Save(id);
        }
    }

    public T GetLayer<T>() where T : UILayer
    {
        var layer = layers.Find(x => x is T);
        if (layer)
        {
            return layer as T;
        }
        return null;
    }

    public T GetLayer<T>(int id) where T: UILayer
    {
        var tempLayers = layers.FindAll(x => x is T);
        if(id < tempLayers.Count)
        {
            return tempLayers[id] as T;
        }
        return null;
    }
}
