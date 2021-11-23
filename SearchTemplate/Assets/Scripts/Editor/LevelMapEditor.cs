using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Game.LevelMap))]
public class LevelMapEditor : Editor
{
    private Game.LevelMap _levelMap;
    private Vector3 _cursor3D;
    private bool _needsRepaint;
    private bool _mouseDown;
    private bool _editMode;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (!_editMode)
        {
            if (GUILayout.Button("Enter Edit Mode"))
            {
                _editMode = true;
                _levelMap.Clear();
            }
        }
        else
        {
            if (GUILayout.Button("Exit Edit Mode"))
            {
                _editMode = false;
                _levelMap.Clear();
                _levelMap.InstantiatePoints();
            }

            GUILayout.Label("LMB to place wall \nLMB + Ctrl to remove wall");
        }

        if (GUILayout.Button("Save Map"))
        {
            _levelMap.SavePoints();
        }

        if (GUILayout.Button("Load Map"))
        {
            _levelMap.LoadPoints();
        }

        if (GUILayout.Button("Clear Map"))
        {
            _levelMap.ClearPoints();
        }
    }

    private void OnSceneGUI()
    {
        if (!_editMode)
            return;

        Event guiEvent = Event.current;

        if (guiEvent.type == EventType.Repaint)
        {
            Draw();
        }
        else if (guiEvent.type == EventType.Layout)
        {
            HandleUtility.AddDefaultControl(GUIUtility.GetControlID(FocusType.Passive));
        }
        else
        {
            HandleInput(guiEvent);
            if (_needsRepaint)
            {
                HandleUtility.Repaint();
            }
        }
        
        if (guiEvent.type == EventType.Layout)
        {
            HandleUtility.AddDefaultControl(GUIUtility.GetControlID(FocusType.Passive));
        }
    }

    private void HandleInput(Event guiEvent)
    {
        Ray mouseRay = HandleUtility.GUIPointToWorldRay(guiEvent.mousePosition);
        float dstToDrawPlane = (_levelMap.GroundHeight - mouseRay.origin.y) / mouseRay.direction.y;
        _cursor3D = mouseRay.GetPoint(dstToDrawPlane);
        _cursor3D = new Vector3(Mathf.Round(_cursor3D.x), _levelMap.GroundHeight, Mathf.Round(_cursor3D.z));

        if (guiEvent.type == EventType.MouseDown && guiEvent.button == 0)
        {
            _mouseDown = true;
        }

        if (guiEvent.type == EventType.MouseUp && guiEvent.button == 0)
        {
            _mouseDown = false;
        }

        if (_mouseDown && guiEvent.modifiers == EventModifiers.None)
        {
            Undo.RecordObject(_levelMap, "Add point");
            _levelMap.AddPoint(_cursor3D);
            _needsRepaint = true;
        }

        if (_mouseDown && guiEvent.modifiers == EventModifiers.Control)
        {
            Undo.RecordObject(_levelMap, "Remove point");
            _levelMap.RemovePoint(_cursor3D);
            _needsRepaint = true;
        }
    }

    private void Draw()
    {
        Handles.color = Color.yellow;
        for (int i = 0; i < _levelMap.Points.Count; i++)
        {
            Handles.CubeHandleCap(0, _levelMap.Points[i], Quaternion.identity, 1, EventType.Repaint);
        }

        if ((_cursor3D.x * _cursor3D.x > _levelMap.Size * _levelMap.Size) || (_cursor3D.z * _cursor3D.z > _levelMap.Size * _levelMap.Size))
            return;
        Handles.color = Color.white;
        Handles.DrawWireCube(_cursor3D, Vector3.one);
    }

    private void OnEnable()
    {
        _levelMap = target as Game.LevelMap;
    }
}