using System;
using System.Globalization;
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
    }

    private void OnLayout(UImGui.UImGui obj)
    {
        if (isPlayerDebugOpen)
        {
            ImGui.Begin("Genetic Algorithm Info", ref isPlayerDebugOpen);

            ImGui.Text("Average fitness Warehouse A not normalized");
            ImGui.PlotLines("AVG fitness WA", ref fitnessA[0], fitnessA.Length, 5, null, 0, 1100,
                new Vector2(0, 80.0f));

            ImGui.Text("Average fitness Warehouse B not normalized");
            ImGui.PlotLines("AVG fitness WB", ref fitnessB[0], fitnessB.Length, 5, null, 100, 1100,
                new Vector2(0, 80.0f));

            
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