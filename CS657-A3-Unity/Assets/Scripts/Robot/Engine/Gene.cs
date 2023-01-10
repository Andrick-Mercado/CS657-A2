using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GeneticAlgorithm
{
    public class Gene
    {
        private Vector2 _housePosition;

        public Gene(Vector2 housePosition)
        {
            _housePosition = housePosition;
        }

        public Vector2 HousePosition()
        {
            return _housePosition;
        }
    }
}