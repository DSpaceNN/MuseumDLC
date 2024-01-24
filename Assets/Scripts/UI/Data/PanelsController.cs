using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PanelsController
{
    private CanvasController _canvasController;
    private PanelsStorage _storage;
    private PanelBase _mainPanel;
    private Dictionary<Type, PanelBase> _panelsOnScene = new Dictionary<Type, PanelBase>();

    public PanelsController(CanvasController canvasController, PanelsStorage storage)
    {
        _canvasController = canvasController;
        _storage = storage;
    }

    public void ShowPanelById(string id, IPanelBaseData dataForOpen = null)
    {
        PanelInStorage storPanel = _storage.Panels.FirstOrDefault(x => x.PanelId == id);
        if (storPanel == null)
        {
            Debug.Log("По этому Id нет панели");
            return;
        }

        GameObject panelPrefab = storPanel.Panel;
        Type panelType = panelPrefab.GetComponent<PanelBase>().GetType();

        if (IsPanelOnScene(panelType, out PanelBase panelOnScene))
        {
            if (panelOnScene is IMainPanel)
                ClearPanels();
            else
                DeletePanelOnScene(panelType);
        }

        GameObject panelGo = GameObject.Instantiate(panelPrefab, _canvasController.transform);
        PanelBase panelMb = panelGo.GetComponent<PanelBase>();

        if (panelMb is IPopupPanel)
            panelGo.transform.SetAsFirstSibling();

        if (panelMb is IMainPanel)
        {
            ClearPanels();
            _mainPanel = panelMb;
        }

        panelMb.Initialize(_canvasController, this, dataForOpen);
        _panelsOnScene.Add(panelMb.GetType(), panelMb);
    }

    public void DeletePanelOnScene(Type type)
    {
        PanelBase panelMb = _panelsOnScene[type];
        if (panelMb == null)
            return;

        panelMb.CleanUpPanel();
        panelMb.DestroyPanel();
        _panelsOnScene.Remove(type);
    }

    private void ClearPanels()
    {
        if (_mainPanel != null)
            _mainPanel = null;
        
        foreach(var panel in _panelsOnScene)
        {
            panel.Value.CleanUpPanel();
            panel.Value.DestroyPanel();
        }

        _panelsOnScene.Clear();
    }

    private bool IsPanelOnScene(Type type, out PanelBase panelOnScene)
    {
        panelOnScene = null;
        if(_panelsOnScene.Count > 0)
        {
            foreach (var panel in _panelsOnScene)
            {
                if (panel.Key == type)
                {
                    panelOnScene = panel.Value;
                    return true;
                }
            }
        }
        
        return false;
    }
}