using System;
using System.Collections;
using System.Collections.Generic;
using GeneticAlgorithm;
using UnityEngine;
using UnityEngine.UI;


public class StudentSolution : MonoBehaviour
{
    public static StudentSolution Instance;

    public List<Vector2> movesGraph = new List<Vector2>();
    public int[,] ObstacleMap;
    public Dropdown dropdownmoves;
    public int direction;

    [HideInInspector] public bool SolutionFound;
    [HideInInspector] public float envirement;

    public int maxGenerations;

    [HideInInspector] public int moves;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        envirement = 0.9f;
        direction = 0;
        maxGenerations = 600;
    }

    public void GenerateValuesArray()
    {
        
        Population test = new Population(0.7f, 0.1f, 10,10, maxGenerations);
        ObstacleMap = test.CityAreaGrid;

        GameManager.Instance.ChangeState(GameState.GenerateGrid);
    }

    public void SetMaxGenerations()
    {
        switch (dropdownmoves.value)
        {
            case 0:
                maxGenerations = 600;
                break;
            case 1:
                maxGenerations = 500;
                break;
            case 2:
                maxGenerations = 400;
                break;
            case 3:
                maxGenerations = 300;
                break;
            case 4:
                maxGenerations = 200;
                break;
            case 5:
                maxGenerations = 100;
                break;
            default:
                maxGenerations = 600;
                break;
        }
    }
}