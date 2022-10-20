using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GeneticAlgorithm
{
    public class Population
    {
        private Vector2 _warehouseA = new Vector2(10, 5);
        private List<Chromosome> _population; // parent container
        private List<Chromosome> _chromosomesChildren; // children container
        private Vector2[] _genes;// all house locations
        private int[,] _cityAreaGrid;// holds all grid information
        private int _currentGeneration;
        private float _pCrossOver;
        private float _pMutation;
        private int _numChromosomes;
        private int _numPopulation;
        private int _numGenerations;
        
        public int[,] CityAreaGrid => _cityAreaGrid;

        public Population(float pCrossover, float pMutation, int numChromosomes,int numPopulation, int numGenerations)
        {
            _pCrossOver = pCrossover;
            _pMutation = pMutation;
            _numChromosomes = numChromosomes;
            _numPopulation = numPopulation;
            _numGenerations = numGenerations;

            //initialize known information
            _population = new List<Chromosome>(); 
            _chromosomesChildren = new List<Chromosome>();
            _genes = new Vector2[45];
            _cityAreaGrid = new int[35, 35];
            _cityAreaGrid[10, 5] = 2; // a 2 represents a warehouse on this array
            GenerateHousesPositions(45); // create 45 houses
            GenerateRandomHousePopulation();
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
        }

        private void GenerateRandomHousePopulation()
        {//create houses (chromosomes) array this is the first family/population
            // here all house pos are at random positions
            for (int i = 0; i < _numPopulation; i++)
            {// sort _genes randomly
                _genes = RandomGenes(_genes);
                _population.Add(new Chromosome(_genes, _warehouseA)); 
            }
        }

        public void CreateGenerations()
        {
            _currentGeneration = 1;
            while (_currentGeneration <= _numGenerations)
            {
                // execute selection function (not yet implemented) to select the fittest chromosomes to reproduce
                    // (thinking: select chromosomes to reproduce exactly the same as the numPopulation

                // execute crossover function (not yet implemented to exchange houses positions and arrange to not repeat houses
                    // (thinking: get the children list and mix up the chromosomes inside the list

                // execute mutation function (not yet implemented to swap houses with other random house
                    // (thinking: just swap with another house position similar to the RandomGenes functions

                // execute sort by fitness function (not yet implemented) to sort houses by fitness
                    // (thinking: calculate the new fitness for each chromosomes 
                    
                    
                /*  lastly we clear the parents and copy in the children into the parent list  */

                _currentGeneration++;
            }
        }

        private Vector2[] RandomGenes(Vector2[] geneArray)
        {
            Vector2 tempGo;
            for (int i = 0; i < geneArray.Length- 1; i++) 
            {
                int rnd = Random.Range(i, geneArray.Length);
                tempGo = geneArray[rnd]; 
                geneArray[rnd] = geneArray[i]; 
                geneArray[i] = tempGo;
            }
            return geneArray;
        }
    }
}