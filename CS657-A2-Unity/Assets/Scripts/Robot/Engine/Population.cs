using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GeneticAlgorithm
{
    public class Population
    {
        private Vector2 _warehouse;// = new Vector2(10, 5);
        private List<Chromosome> _population; // parent container
        private List<Chromosome> _chromosomesChildren; // children container
        private Vector2[] _genes;// all house locations
        private int[,] _cityAreaGrid;// holds all grid information
        private int _currentGeneration;
        private float _pCrossOver;
        private float _pMutation;
        private int _numChromosomes;
        //private int _numPopulation;
        private int _numGenerations;
        
        public int[,] CityAreaGrid => _cityAreaGrid;

        public Population(float pCrossover, float pMutation, int numChromosomes, int numGenerations, Vector2 warehouse, Vector2[] genes)
        {
            _pCrossOver = pCrossover;
            _pMutation = pMutation;
            _numChromosomes = numChromosomes;
            _warehouse = warehouse;
            _genes = genes;
            //_numPopulation = numPopulation;
            _numGenerations = numGenerations;

            //initialize known information
            _population = new List<Chromosome>(); 
            _chromosomesChildren = new List<Chromosome>();
            /*_genes = new Vector2[45];
            _cityAreaGrid = new int[35, 35];
            _cityAreaGrid[10, 5] = 2;*/ // a 2 represents a warehouse on this array
            //GenerateHousesPositions(45); // create 45 houses
            GenerateRandomHousePopulation();
            CreateGenerations();
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
            for (int i = 0; i < _numChromosomes; i++)
            {// sort _genes randomly
                _genes = RandomGenes(_genes);
                _population.Add(new Chromosome(_genes, _warehouse, _pMutation, true)); 
            }
        }

        public void CreateGenerations()
        {
            _currentGeneration = 1;
            _population = ChromosomeFitnessSort(_population);// sort the first generation
            
            while (_currentGeneration <= _numGenerations)
            {
                // execute selection function (not yet implemented) to select the fittest chromosomes to reproduce
                    // (thinking: select chromosomes to reproduce exactly the same as the numPopulation
                    
                        
                // execute crossover function (not yet implemented to exchange houses positions and arrange to not repeat houses
                    // (thinking: get the children list and mix up the chromosomes inside the list
                ChromosomeMating();
                    
                // execute mutation function (not yet implemented to swap houses with other random house
                    // (thinking: just swap with another house position similar to the RandomGenes functions
                ChromosomeMutation();
                    
                // execute sort by fitness function (not yet implemented) to sort houses by fitness
                    // (thinking: calculate the new fitness for each chromosomes 
                _chromosomesChildren = ChromosomeFitnessSort(_chromosomesChildren);    
                    
                /*  lastly we clear the parents and copy in the children into the parent list  */
                ChromosomeSwapPopulation();
                
                _currentGeneration++;
            }

            _population = ChromosomeFitnessSort(_population);
            Debug.Log("Fittest king: "+_population[0].FitnessChromosome());
        }

        private void ChromosomeMating()
        {// select fittest and make numPopulation amount of children add the children to _chromosomesChildren
            // Perform Elitism, that mean 10% of fittest population
            int s = (10*_numChromosomes)/100;
            s = _numChromosomes % 2 == 0 ? s+1 : s;
            for(int i = 0;i<s;i++)
            {
                Chromosome elite = new Chromosome(_population[i].GetHouses(), _warehouse, _pMutation, false);
                _chromosomesChildren.Add(elite);
            }
  
            // From 50% of fittest population, Individuals, will mate to produce offspring
            s = (45*_numChromosomes)/100;// remaining population (90)
            for(int i = 0;i<s;i++)
            {
                int r = Random.Range(0, _numChromosomes);//random_num(0, 50);
                Chromosome parent1 = _population[r];
                r = Random.Range(0, _numChromosomes );//r = random_num(0, 50);
                Chromosome parent2 = _population[r];
                List<Chromosome> offspring = parent1.OrderCrossover(parent2);// perform crossover and produce one offspring

                Chromosome offspring1 = new Chromosome(offspring[0].GetHouses(), _warehouse, _pMutation, false);
                Chromosome offspring2 = new Chromosome(offspring[1].GetHouses(), _warehouse, _pMutation, false);
                _chromosomesChildren.Add(offspring1);//offspring[0]);
                _chromosomesChildren.Add(offspring2);//offspring[1]);
            } 
            
        }

        private void ChromosomeMutation()
        {
            for (int i = 0; i < _chromosomesChildren.Count; i++) 
            { 
                _chromosomesChildren[i].MutateChromosome();
            }
        }

        private static List<Chromosome> ChromosomeFitnessSort(List<Chromosome> chromosomeList)
        {// sort by fittest chromosomes first
            return chromosomeList.OrderBy(o=>o.FitnessChromosome()).ToList();
        }

        private void ChromosomeSwapPopulation()
        {
            _population.Clear();
            _population = _chromosomesChildren.GetRange(0, _chromosomesChildren.Count);
            _chromosomesChildren.Clear();
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

        public Vector2[] GetPopulation()
        {
            return _population[0].GetHouses();
        }
    }
}
