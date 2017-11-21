using System;
using System.Collections.Generic;
using UnityEngine;

public class EventController
{
    private readonly Dictionary<string, Delegate> _router = new Dictionary<string, Delegate>();
    // 清理事件
    public void Clear()
    {
        _router.Clear();
    }

    // 添加事件
    private void OnListenerAdding(string eventType, Delegate listenerBeingAdded)
    {
        if (!_router.ContainsKey(eventType)) {
            _router.Add(eventType, null);
        }

        var handler = _router[eventType];
        if (handler != null && handler.GetType() != listenerBeingAdded.GetType()) {
            throw new Exception(string.Format("Try to add not correct event {0}. Current type is {1}, adding type is {2}.", eventType, handler.GetType().Name, listenerBeingAdded.GetType().Name));
        }
    }

    private bool OnListenerRemoving(string eventType, Delegate listenerBeingRemoved)
    {
        bool result;
        if (!_router.ContainsKey(eventType)) {
            result = false;
        } else {
            var handler = _router[eventType];
            if (handler != null && handler.GetType() != listenerBeingRemoved.GetType()) {
                throw new Exception(string.Format("Remove listener {0}\" failed, Current type is {1}, adding type is {2}.", eventType, handler.GetType(), listenerBeingRemoved.GetType()));
            }
            result = true;
        }
        return result;
    }

    private void OnListenerRemoved(string eventType)
    {
        if (_router.ContainsKey(eventType) && _router[eventType] == null) {
            _router.Remove(eventType);
        }
    }

    //注册事件
    public void AddEventListener(string eventType, Action handler)
    {
        OnListenerAdding(eventType, handler);
        _router[eventType] = Delegate.Combine(_router[eventType], handler);
    }

    public void AddEventListener<T>(string eventType, Action<T> handler)
    {
        OnListenerAdding(eventType, handler);
        _router[eventType] = Delegate.Combine(_router[eventType], handler);
    }

    public void AddEventListener<T, TU>(string eventType, Action<T, TU> handler)
    {
        OnListenerAdding(eventType, handler);
        _router[eventType] = Delegate.Combine(_router[eventType], handler);
    }

    public void AddEventListener<T, TU, TV>(string eventType, Action<T, TU, TV> handler)
    {
        OnListenerAdding(eventType, handler);
        _router[eventType] = Delegate.Combine(_router[eventType], handler);
    }

    public void AddEventListener<T, TU, TV, TW>(string eventType, Action<T, TU, TV, TW> handler)
    {
        OnListenerAdding(eventType, handler);
        _router[eventType] = Delegate.Combine(_router[eventType], handler);
    }

    // 移除事件
    public void RemoveEventListener(string eventType, Action handler)
    {
        if (OnListenerRemoving(eventType, handler)) {
            _router[eventType] = Delegate.Remove(_router[eventType], handler);
            OnListenerRemoved(eventType);
        }
    }

    public void RemoveEventListener<T>(string eventType, Action<T> handler)
    {
        if (OnListenerRemoving(eventType, handler)) {
            _router[eventType] = Delegate.Remove(_router[eventType], handler);
            OnListenerRemoved(eventType);
        }
    }

    public void RemoveEventListener<T, TU>(string eventType, Action<T, TU> handler)
    {
        if (OnListenerRemoving(eventType, handler)) {
            _router[eventType] = Delegate.Remove(_router[eventType], handler);
            OnListenerRemoved(eventType);
        }
    }

    public void RemoveEventListener<T, TU, TV>(string eventType, Action<T, TU, TV> handler)
    {
        if (OnListenerRemoving(eventType, handler)) {
            _router[eventType] = Delegate.Remove(_router[eventType], handler);
            OnListenerRemoved(eventType);
        }
    }

    public void RemoveEventListener<T, TU, TV, TW>(string eventType, Action<T, TU, TV, TW> handler)
    {
        if (OnListenerRemoving(eventType, handler)) {
            _router[eventType] = Delegate.Remove(_router[eventType], handler);
            OnListenerRemoved(eventType);
        }
    }

    // 触发事件
    public void TriggerEvent(string eventType)
    {
        Delegate handler;
        if (_router.TryGetValue(eventType, out handler)) {
            var invocationList = handler.GetInvocationList();
            for (var i = 0; i < invocationList.Length; i++) {
                var action = invocationList[i] as Action;
                if (action == null) {
                    Debug.LogError(string.Format("TriggerEvent {0} error: types of parameters are not match.", eventType));
                    continue;
                }

                try {
                    action();
                } catch (Exception ex) {
                    Debug.LogException(ex, null);
                }
            }
        }
    }

    public void TriggerEvent<T>(string eventType, T arg1)
    {
        Delegate handler;
        if (_router.TryGetValue(eventType, out handler)) {
            var invocationList = handler.GetInvocationList();
            for (var i = 0; i < invocationList.Length; i++) {
                var action = invocationList[i] as Action<T>;
                if (action == null) {
                    Debug.LogError(string.Format("TriggerEvent {0} error: types of parameters are not match.", eventType));
                    continue;
                }

                try {
                    action(arg1);
                } catch (Exception ex) {
                    Debug.LogException(ex, null);
                }
            }
        }
    }

    public void TriggerEvent<T, TU>(string eventType, T arg1, TU arg2)
    {
        Delegate handler;
        if (_router.TryGetValue(eventType, out handler)) {
            var invocationList = handler.GetInvocationList();
            for (var i = 0; i < invocationList.Length; i++) {
                var action = invocationList[i] as Action<T, TU>;
                if (action == null) {
                    Debug.LogError(string.Format("TriggerEvent {0} error: types of parameters are not match.", eventType));
                    continue;
                }

                try {
                    action(arg1, arg2);
                } catch (Exception ex) {
                    Debug.LogException(ex, null);
                }
            }
        }
    }

    public void TriggerEvent<T, TU, TV>(string eventType, T arg1, TU arg2, TV arg3)
    {
        Delegate handler;
        if (_router.TryGetValue(eventType, out handler)) {
            var invocationList = handler.GetInvocationList();
            for (var i = 0; i < invocationList.Length; i++) {
                var action = invocationList[i] as Action<T, TU, TV>;
                if (action == null) {
                    Debug.LogError(string.Format("TriggerEvent {0} error: types of parameters are not match.", eventType));
                    continue;
                }

                try {
                    action(arg1, arg2, arg3);
                } catch (Exception ex) {
                    Debug.LogException(ex, null);
                }
            }
        }
    }

    public void TriggerEvent<T, TU, TV, TW>(string eventType, T arg1, TU arg2, TV arg3, TW arg4)
    {
        Delegate handler;
        if (_router.TryGetValue(eventType, out handler)) {
            var invocationList = handler.GetInvocationList();
            for (var i = 0; i < invocationList.Length; i++) {
                var action = invocationList[i] as Action<T, TU, TV, TW>;
                if (action == null) {
                    Debug.LogError(string.Format("TriggerEvent {0} error: types of parameters are not match.", eventType));
                    continue;
                }

                try {
                    action(arg1, arg2, arg3, arg4);
                } catch (Exception ex) {
                    Debug.LogException(ex, null);
                }
            }
        }
    }
}

// 事件分派
public class EventDispatcher
{
    private static EventController _eventController = new EventController();

    public static void Clear()
    {
        _eventController.Clear();
    }

    public static void AddEventListener(string eventType, Action handler)
    {
        _eventController.AddEventListener(eventType, handler);
    }

    public static void AddEventListener<T>(string eventType, Action<T> handler)
    {
        _eventController.AddEventListener(eventType, handler);
    }

    public static void AddEventListener<T, TU>(string eventType, Action<T, TU> handler)
    {
        _eventController.AddEventListener(eventType, handler);
    }

    public static void AddEventListener<T, TU, TV>(string eventType, Action<T, TU, TV> handler)
    {
        _eventController.AddEventListener(eventType, handler);
    }

    public static void AddEventListener<T, TU, TV, TW>(string eventType, Action<T, TU, TV, TW> handler)
    {
        _eventController.AddEventListener(eventType, handler);
    }

    public static void RemoveEventListener(string eventType, Action handler)
    {
        _eventController.RemoveEventListener(eventType, handler);
    }

    public static void RemoveEventListener<T>(string eventType, Action<T> handler)
    {
        _eventController.RemoveEventListener(eventType, handler);
    }

    public static void RemoveEventListener<T, TU>(string eventType, Action<T, TU> handler)
    {
        _eventController.RemoveEventListener(eventType, handler);
    }

    public static void RemoveEventListener<T, TU, TV>(string eventType, Action<T, TU, TV> handler)
    {
        _eventController.RemoveEventListener(eventType, handler);
    }

    public static void RemoveEventListener<T, TU, TV, TW>(string eventType, Action<T, TU, TV, TW> handler)
    {
        _eventController.RemoveEventListener(eventType, handler);
    }

    public static void TriggerEvent(string eventType)
    {
        _eventController.TriggerEvent(eventType);
    }

    public static void TriggerEvent<T>(string eventType, T arg1)
    {
        _eventController.TriggerEvent(eventType, arg1);
    }

    public static void TriggerEvent<T, TU>(string eventType, T arg1, TU arg2)
    {
        _eventController.TriggerEvent(eventType, arg1, arg2);
    }

    public static void TriggerEvent<T, TU, TV>(string eventType, T arg1, TU arg2, TV arg3)
    {
        _eventController.TriggerEvent(eventType, arg1, arg2, arg3);
    }

    public static void TriggerEvent<T, TU, TV, TW>(string eventType, T arg1, TU arg2, TV arg3, TW arg4)
    {
        _eventController.TriggerEvent(eventType, arg1, arg2, arg3, arg4);
    }
}
