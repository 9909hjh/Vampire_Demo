using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    UI_Base _sceneUI;
    Stack<UI_Base> _uistack = new Stack<UI_Base>();

    public T GetSceneUI<T>() where T : UI_Base
    {
        return _sceneUI as T;
    }

    public T ShowSceneUI<T>() where T : UI_Base
    {
        if(_sceneUI != null)
            return GetSceneUI<T>();

        string key = typeof(T).Name + ".prefab";
        T ui = Managers.Resource.Instantiate(key, pooling: true).GetOrAddComponent<T>();
        _sceneUI = ui;

        return ui;
    }

    public T ShowPop<T>() where T : UI_Base
    {
        string key = typeof(T).Name + ".prefab";
        T ui = Managers.Resource.Instantiate(key, pooling: true).GetOrAddComponent<T>();
        _uistack.Push(ui);

        return ui;
    }

    public void ClosePopup()
    {
        if (_uistack.Count == 0)
            return;

        UI_Base ui = _uistack.Pop();
        Managers.Resource.Destroy(ui.gameObject);
    }
}
