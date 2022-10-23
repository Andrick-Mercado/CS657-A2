using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GeneticAlgorithm
{
    public class Chromosome
    {
        private Gene[] _genes;
        private Vector2[] _houses;
        private float _chromosomeFitness;
        private Vector2 _warehouse;
        private float _probabilityMutate;

        public Chromosome(Vector2[] housePositions, Vector2 warehouse, float probabilityMutate, bool firstParent)
        {
            //Debug.Log("called chromosome");
            _warehouse = warehouse;
            _probabilityMutate = probabilityMutate;
            _houses = new Vector2[housePositions.Length];
            _genes = new Gene[housePositions.Length];
            Array.Copy(housePositions, _houses, housePositions.Length);
            
            //if(firstParent)// assign random genes if its the first generation
                AssignHousesToGenes();
        }

        public void AssignHousesToGenes()
        {// add a different house to all genes in the chromosome
            for (int i = 0; i < _houses.Length; i++)
            {
                _genes[i] = new Gene(_houses[i]);
            }
        }

        public float FitnessChromosome()
        {// calculate the fitness of the chromosome (adding the fitness of the genes fitness)
            float currentFitness = 0f;
            
            if (_genes.Length >=1)
                currentFitness+= Vector2.Distance(_warehouse, _genes[0].HousePosition()); 
            
            for (int i = 1; i < _genes.Length; i++)
            {//iterate through genes to calculate their fitness
                currentFitness += Vector2.Distance(_genes[i-1].HousePosition(), _genes[i].HousePosition()); 
            }

            currentFitness += Vector2.Distance(_genes[_genes.Length-1].HousePosition(), _warehouse);
            _chromosomeFitness = currentFitness;
            return currentFitness;
        }

        public List<Chromosome> OrderCrossover(Chromosome otherParent)
        {
            int size = _genes.Length;//int(parent1.size());

            //MyRandom rand;
            int number1 = Random.Range(0, size);//rand.nextInt(7);
            int number2 = Random.Range(0, size);//rand.nextInt(7);

            int start = Math.Min(number1, number2);//fmin(number1, number2);
            int end = Math.Max(number1, number2);//fmax(number1, number2);

            Gene[] child1 = new Gene[_genes.Length];//std::vector<int> child1;
            Gene[] child2 = new Gene[_genes.Length];//std::vector<int> child2;

            for(int i = start; i<end; i++)
            {
                child1[i] = _genes[i];
                child2[i] = otherParent._genes[i];
            }

            int geneIndex = 0;
            Gene geneInparent1;
            Gene geneInparent2;

            for (int i = 0; i<size; i++)
            {
                geneIndex = (end + i) % size;
                geneInparent1 = _genes[geneIndex];
                geneInparent2 = otherParent._genes[geneIndex];

                bool is_there = false;
                for(int i1 = 0; i1<child1.Length; i1++)
                {
                    if(child1[i1] == geneInparent2)
                    {
                        is_there = true;
                    }
                }
                if(!is_there)
                {
                    child1[i] = geneInparent2; //child1.push_back(geneInparent2);
                }

                bool is_there1 = false;
                for(int i1 = 0; i1<child2.Length; i1++)
                {
                    if(child2[i1] == geneInparent1)
                    {
                        is_there1 = true;
                    }
                }
                if(!is_there1)
                {
                    child2[i] = geneInparent1;//child2.push_back(geneInparent1);
                }
            }
            
            for(int i = 0; i<size; i++)
            {
                _genes[i] = child2[i];
                otherParent._genes[i] = child1[i];
            }
            List<Chromosome> children = new List<Chromosome>{this, otherParent };
            return children;
        }

        public void MutateChromosome()
        {// perform swap houses with a random house based on probability mutate
            Gene tempGo;
            for (int i = 0; i < _genes.Length- 1; i++) 
            {
                if (Random.value < _probabilityMutate)
                {
                    int rnd = Random.Range(i, _genes.Length);
                    tempGo = _genes[rnd]; 
                    _genes[rnd] = _genes[i]; 
                    _genes[i] = tempGo;
                }
            }
        }

        public Vector2[] GetHouses()
        {
            return _houses.Clone() as Vector2[];//_houses;
        }
    }
}


