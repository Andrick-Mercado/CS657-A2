using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GeneticAlgorithm;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;


public class StudentSolution : MonoBehaviour
{
    public static StudentSolution Instance;

    public List<Vector2> movesGraph = new List<Vector2>();
    public int[,] ObstacleMap;
    public Dropdown dropdownmoves;
    public int direction;
    public LineRenderer lineRendererWarehouseA;
    public LineRenderer LineRendererWarehouseB;

    [HideInInspector] public bool SolutionFound;
    [HideInInspector] public float envirement;

    public int maxGenerations;

    [HideInInspector] public int moves;
    
    private int[,] _cityAreaGrid;// holds all grid information
    private Vector2[] _genes;// all house locations
    private List<Vector2> _fittestWarehouseA;
    private List<Vector2> _fittestWarehouseB;

    public int[,] CityAreaGrid => _cityAreaGrid;


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
        Vector2 warehouseA = new Vector2(10,5);
        Vector2 warehouseB = new Vector2(25,30);

        GenerateHouses(45);
            
        Population warehouseRouteA = new Population(0.7f, 0.5f, 20, 10000, warehouseA, _genes);
        Population warehouseRouteB = new Population(0.7f, 0.5f, 20, 10000, warehouseB, _genes);
        
        _fittestWarehouseA = new List<Vector2>(warehouseRouteA.GetPopulation());
        _fittestWarehouseB = new List<Vector2>(warehouseRouteB.GetPopulation());
        
        CalculateFitnessWarehouses(warehouseA, warehouseB);
        GenerateLines();
            
        GameManager.Instance.ChangeState(GameState.GenerateGrid);
    }

    private void GenerateHouses(int numHouses)
    {
        //generate houses at random positions not in same as the warehouse (on a 35 by 35 grid)
        int numHousesMade = 0;
        _cityAreaGrid = new int[35, 35];
        _genes = new Vector2[45];
        _cityAreaGrid[10, 5] = 2;
        _cityAreaGrid[25, 30] = 2;
        while (numHousesMade < numHouses)
        {// inneficient but provides the randomness we want
            for (int houseNumRow = 0; houseNumRow < _cityAreaGrid.GetLength(0); houseNumRow++)
            {
                for (int houseNumCol = 0; houseNumCol < _cityAreaGrid.GetLength(1); houseNumCol++)
                {
                    if (numHousesMade < numHouses && Random.value < 0.005f && _cityAreaGrid[houseNumRow, houseNumCol] == 0)
                    {// check that its an empty cell or 0
                        _cityAreaGrid[houseNumRow, houseNumCol] = 1; // a 1 represents a house
                        _genes[numHousesMade] = new Vector2(houseNumRow, houseNumCol);
                        numHousesMade++;
                    }
                }
            }
        }
    }

    private void CalculateFitnessWarehouses(Vector2 warehouseA, Vector2 warehouseB)
    {
        int index = 0;
        int indexA = 0;
        int indexB = 0;
        while (index < _genes.Length)
        {
            indexA = _fittestWarehouseA.FindIndex(a => a == _genes[index]);
            indexB = _fittestWarehouseB.FindIndex(a => a == _genes[index]);
            
            if (Vector2.Distance(_fittestWarehouseA[indexA], warehouseA) <= Vector2.Distance(_fittestWarehouseB[indexB], warehouseB))
            {
                _fittestWarehouseB.RemoveAt(indexB);
            }
            else
            {
                _fittestWarehouseA.RemoveAt(indexA);
            }
            index++;
        }

    }

    private void GenerateLines()
    {
        lineRendererWarehouseA.positionCount = _fittestWarehouseA.Count+2;
        lineRendererWarehouseA.SetPosition(0, new Vector3(10f,5f, 0f));
        for (int i = 1; i <= lineRendererWarehouseA.positionCount-2; i++)
        {
            lineRendererWarehouseA.SetPosition(i, new Vector3(_fittestWarehouseA[i-1].x,_fittestWarehouseA[i-1].y, 0f));
        }
        lineRendererWarehouseA.SetPosition(lineRendererWarehouseA.positionCount - 1, new Vector3(10f, 5f, 0f));
        
        
        LineRendererWarehouseB.positionCount = _fittestWarehouseB.Count+2;
        LineRendererWarehouseB.SetPosition(0, new Vector3(25f,30f, 0f));
        for (int i = 1; i <= LineRendererWarehouseB.positionCount-2; i++)
        {
            LineRendererWarehouseB.SetPosition(i, new Vector3(_fittestWarehouseB[i-1].x,_fittestWarehouseB[i-1].y, 0f));
        }
        LineRendererWarehouseB.SetPosition(LineRendererWarehouseB.positionCount - 1, new Vector3(25f, 30f, 0f));
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