using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GeneticAlgorithm
{
    public class Gene
    {
        private Vector2 _housePosition;
        private float _fitness;

        public Gene(Vector2 housePosition)
        {
            _housePosition = housePosition;
        }
        public float GetGeneFitnessWarehouse(Vector2 wareHousePosition)
        {
            return Vector2.Distance(_housePosition, wareHousePosition);
        }

        public float GetGeneFitnessToLeftGene(Vector2 leftGene)
        {
            return Vector2.Distance(_housePosition, leftGene);
        }
        
        public float GetGeneFitnessToRightGene(Vector2 rightGene)
        {
            return Vector2.Distance(_housePosition, rightGene);
        }
    }
}

