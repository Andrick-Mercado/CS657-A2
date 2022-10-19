using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GeneticAlgorithm
{
    public class Population
    {
        private Vector2 _warehouseA = new Vector2(10, 5);
        private Vector2[] _chromosomes; // parent container
        private Vector2[] _chromosomesChildren; // children container
        private Vector2[] _genes;// all house locations
        private int[,] _cityAreaGrid;// holds all grid information
        private int _currentGeneration;
        private float _pCrossOver;
        private float _pMutation;
        private int _numChromosomes;
        private int _numGenerations;
        public int[,] CityAreaGrid => _cityAreaGrid;

        public Population(float pCrossover, float pMutation, int numChromosomes, int numGenerations)
        {
            _pCrossOver = pCrossover;
            _pMutation = pMutation;
            _numChromosomes = numChromosomes;
            _numGenerations = numGenerations;
            
            //initialize known information
            _genes = new Vector2[45];
            _cityAreaGrid = new int[35, 35];
            _cityAreaGrid[10, 5] = 2; // a 2 represents a warehouse on this array
            GenerateHousesPositions(45); // create 45 houses
        }

        private void GenerateHousesPositions(int numHouses)
        {//generate houses at random positions not in same as the warehouse (on a 35 by 35 grid)
            int numHousesMade = 0;

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
            GenerateRandomHousePopulation();
        }

        private void GenerateRandomHousePopulation()
        {//create houses (chromosomes) array with unique houses (chromosomes) on each position
            _chromosomes = _genes.Clone() as Vector2[];// here all house pos are at random positions
        }

        public void CreateGenerations()
        {
            while (_currentGeneration < _numGenerations)
            {
                // execute selection function (not yet implemented) to select the fittest chromosomes

                // execute crossover function (not yet implemented to exchange houses positions and arrange to not repeat houses

                // execute mutation function (not yet implemented to swap houses with other random house

                // execute sort by fitness function (not yet implemented) to sort houses by fitness

                _currentGeneration++;
            }
        }
    }
}