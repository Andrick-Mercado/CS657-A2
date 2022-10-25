using System;
using System.Globalization;
using System.IO;
using ImGuiNET;
using UnityEngine;
using ImPlotNET;

public class GUIDebug : MonoBehaviour
{
    [Header("Dependencies")] [SerializeField]
    private UImGui.UImGui uimGuiInstance;

    [Header("Settings")] [SerializeField] private bool isPlayerDebugOpen;

    private float[] fitnessA;
    private float[] fitnessB;
    private float fitnessAValue;
    private float fitnessBValue;

    private void Awake()
    {
        if (uimGuiInstance == null)
        {
            Debug.LogError(
                "Must assign a UImGuiInstance or use UImGuiUtility with Do Global Events on UImGui component.");
        }

        uimGuiInstance.Layout += OnLayout;
        uimGuiInstance.OnInitialize += OnInitialize;
        uimGuiInstance.OnDeinitialize += OnDeinitialize;
    }

    private void OnEnable()
    {
        fitnessA = StudentSolution.Instance.FitnessArrayWarehouseA;
        fitnessB = StudentSolution.Instance.FitnessArrayWarehouseB;
        fitnessAValue = fitnessA[^1];
        fitnessBValue = fitnessB[^1];
        
        //print out here to file
         
        var  fileName = "fitnessA.txt";
        var sr = File.CreateText(fileName);
        foreach (var tmp in fitnessA)
        {
            sr.WriteLine ("{0},", tmp);
        }
        sr.Close();
        
        var  fileName2 = "fitnessB.txt";
        var src = File.CreateText(fileName2);
        foreach (var tmp in fitnessB)
        {
            src.WriteLine ("{0},", tmp);
        }
        src.Close();
    }

    private void OnLayout(UImGui.UImGui obj)
    {
        if (isPlayerDebugOpen)
        {
            ImGui.Begin("Genetic Algorithm Info", ref isPlayerDebugOpen);

            ImGui.Text("Average fitness Warehouse A not normalized");
            ImGui.PlotLines("AVG fitness WA", ref fitnessA[0], fitnessA.Length, 5, null, 0, 1100,
                new Vector2(0, 80.0f));
            ImGui.Text($"Average fitness Warehouse A normalized: {fitnessAValue}");
            
            ImGui.Indent();

            ImGui.Text("Average fitness Warehouse B not normalized");
            ImGui.PlotLines("AVG fitness WB", ref fitnessB[0], fitnessB.Length, 5, null, 100, 1100,
                new Vector2(0, 80.0f));
            ImGui.Text($"Average fitness Warehouse B normalized: {fitnessBValue}");
            

            ImGui.End();
        }
        //ImPlot.ShowDemoWindow();
    }

    private void OnInitialize(UImGui.UImGui obj)
    {
        // runs after UImGui.OnEnable();
    }

    private void OnDeinitialize(UImGui.UImGui obj)
    {
        // runs after UImGui.OnDisable();
    }

    private void OnDisable()
    {
        uimGuiInstance.Layout -= OnLayout;
        uimGuiInstance.OnInitialize -= OnInitialize;
        uimGuiInstance.OnDeinitialize -= OnDeinitialize;
    }
}