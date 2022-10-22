using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    public LineRenderer lineRenderer;

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
        lineRenderer = GetComponent<LineRenderer>();
    }

    public void GenerateValuesArray()
    {
        // change the house creation outside of class so we can give it as an input to each warehouse
        // make warehouse location a given variable to create the class
        // compare best fitness to each warehouse and remove values accordingly (maybe: make to list )
        
        Population test = new Population(0.7f, 0.5f, 20, 10000);
        ObstacleMap = test.CityAreaGrid;

        Vector2[] otherTest = test.GetPopulation();
        lineRenderer.positionCount = otherTest.Length+2;
        lineRenderer.SetPosition(0, new Vector3(10f,5f, 0f));
        for (int i = 1; i <= lineRenderer.positionCount-2; i++)
        {
            lineRenderer.SetPosition(i, new Vector3(otherTest[i-1].x,otherTest[i-1].y, 0f));
        }
        lineRenderer.SetPosition(lineRenderer.positionCount-1, new Vector3(10f,5f, 0f));
            
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