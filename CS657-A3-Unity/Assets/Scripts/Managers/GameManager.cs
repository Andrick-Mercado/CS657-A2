using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameState GameState;
    private bool hasStarted;

    void Awake()
    {
        Instance = this;
        hasStarted = false;
    }

    public void StartGame()
    {
        if (hasStarted) return;
        ChangeState(GameState.GenerateGridData);
        hasStarted = true;
    }

    public void ChangeState(GameState newState)
    {
        GameState = newState;
        switch (newState)
        {
            case GameState.GenerateGridData:
                StudentSolution.Instance.GenerateValuesArray();
                break;
            case GameState.GenerateGrid:
                GridManager.Instance.GenerateGrid();
                break;
            case GameState.SpawnHeroes:
                UnitManager.Instance.SpawnHeroes();
                break;
            case GameState.SpawnEnemies:
                StudentSolution.Instance.GeneratePlot();
                break;
            case GameState.StartMoving:
                StartCoroutine(UnitManager.Instance.StartMovingTank());
                break;
            case GameState.HeroesTurn:
                break;
            case GameState.EnemiesTurn:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
}

public enum GameState
{
    GenerateGrid = 0,
    SpawnHeroes = 1,
    SpawnEnemies = 2,
    HeroesTurn = 3,
    EnemiesTurn = 4,
    GenerateGridData = 5,
    StartMoving = 6
}
