﻿using System;
using System.Collections.Generic;
using UnityEngine;
using AYellowpaper;


public class Bootstrapper : MonoBehaviour
{
    [SerializeField] private List<InterfaceReference<IManager>> serializedManagers;
    [SerializeField] private List<IManager> _managers = new();

    private void Start()
    {
        StartManagers();
    }

    private void StartManagers()
    {
        serializedManagers.ForEach(manager => _managers.Add(manager.Value));

        List<IStartable> starts = new();
        Utilities.TryCast(_managers, starts);
        starts.ForEach(start =>
        {
            try
            {
                start.CustomStart();
            }
            catch (Exception ex) { Debug.LogException(ex); }

        });
    }

    private void OnDisable()
    {
        Reset();
    }

    private void Reset()
    {
        serializedManagers.ForEach(manager => manager.Value.Reset());
    }
}
