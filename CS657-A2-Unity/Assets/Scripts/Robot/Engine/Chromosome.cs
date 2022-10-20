using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GeneticAlgorithm
{
    public class Chromosome
    {
        private Gene[] _genes;
        private Vector2[] _houses;
        private float _chromosomeFitness;
        private Vector2 _warehouseA;

        public Chromosome(Vector2[] housePositions, Vector2 warehouseA)
        {
            _warehouseA = warehouseA;
            _houses = new Vector2[housePositions.Length];
            _genes = new Gene[housePositions.Length];
            Array.Copy(housePositions, _houses, housePositions.Length);
            AssignHousesToGenes();
            FitnessChromosome();
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
            
            for (int i = 1; i < _genes.Length; i++)
            {//iterate through genes to calculate their fitness
                currentFitness += Vector2.Distance(_genes[i-1].HousePosition(), _genes[i].HousePosition()); 
            }
            _chromosomeFitness = currentFitness;
            return currentFitness;
        }

        public void MutateChromosome(Gene currentGene, float probabilityMutate)
        {// perform swap houses with a random house based on probability mutate
            /*Vector2 tempGo;
            for (int i = 0; i < geneArray.Length- 1; i++) 
            {
                if some probability
                int rnd = Random.Range(i, geneArray.Length);
                tempGo = geneArray[rnd]; 
                geneArray[rnd] = geneArray[i]; 
                geneArray[i] = tempGo;
            }
            return geneArray;*/
        }
    }
}


