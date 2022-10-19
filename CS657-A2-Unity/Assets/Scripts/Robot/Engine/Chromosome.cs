using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GeneticAlgorithm
{
    public class Chromosome
    {
        private Gene[] _genes;
        private Vector2[] _houses;

        public Chromosome(Vector2[] housePositions)
        {
            _houses = housePositions;
        }

        public void AssignHousesToGenes()
        {
            // add a different house to all genes in the chromosome
        }

        public void MutateGene(Gene currentGene, float probabilityMutate)
        {
            // perform swap houses with a random house based on probability mutate
        }
    }
}

