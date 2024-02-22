using System;
using Game;
using Game.Effects;
using UnityEngine;
using Object = UnityEngine.Object;

public class RoadElementModel : IDisposable
{
    private RoadElementEntry _entry;
    private Transform _socket;

    private GameObject _root;
    private CollisionTriggerComponent _component;

    public event Action<RoadElementModel, ITriggerSource> triggered;
    public RoadElementEntry entry { get { return _entry; } }

    public RoadElementModel(RoadElementEntry entry)
    {
        _entry = entry;
        _root = Object.Instantiate(_entry.prefab);

        if (entry.effect != null)
        {
            var component = _root.AddComponent<CollisionTriggerComponent>();
            var collider = _root.AddComponent<BoxCollider>();
            collider.isTrigger = true;
            collider.size = new Vector3(0.4f, 0.5f, 0.4f);
            collider.center = new Vector3(0, 0.25f, 0);

            _component = component;
            component.triggered += HandleTriggered;
        }
    }

    public void Dispose()
    {
        if (_component != null)
        {
            _component.triggered -= HandleTriggered;
        }

        if (_root != null)
        {
            Object.Destroy(_root);
        }
    }

    public void SetSocket(Transform socket)
    {
        _socket = socket;

        _root.transform.SetParent(socket);
        _root.transform.localPosition = Vector3.zero;
    }

    private void HandleTriggered(ITriggerSource source)
    {
        triggered?.Invoke(this, source);
    }
}