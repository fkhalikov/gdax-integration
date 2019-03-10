using GDAX.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TradingPlatform.Common
{
    public class PriceMovementTracker
    {
        Queue<Direction> priceMovement = new Queue<Direction>();

        int _step = 0;
        private readonly decimal _precision;
        decimal _lastPrice = 0;

        public PriceMovementTracker(int step, decimal precision)
        {
            _step = step;
            this._precision = precision;
            for (int i = 0; i < _step; i++)
            {
                priceMovement.Enqueue(Direction.None);
            }
        }

        public Direction Track(decimal price)
        {
            Direction direction = Direction.None;

            decimal difference = (price - _lastPrice);

            if (Math.Abs(difference) < _precision || _lastPrice == 0)
            {
                direction = Direction.None;
            }
            else
            {
                direction = difference > 0 ? Direction.UP : Direction.DOWN;

                priceMovement.Dequeue();
                priceMovement.Enqueue(direction);
            }

            _lastPrice = price;

            return direction;
        }

        public bool AllUP()
        {
            return priceMovement.All(x => x == Direction.UP);
        }

        public bool AllDOWN()
        {
            return priceMovement.All(x => x == Direction.DOWN);
        }
    }
}
