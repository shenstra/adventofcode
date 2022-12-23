using Advent.Util;

namespace Advent.Aoc2020
{
    public class Day11
    {
        private readonly IInput input;

        public Day11(IInput input)
        {
            this.input = input;
        }

        public void Part1()
        {
            var seats = GetSeats(input.GetLines());
            while (ApplyRound(seats, out seats, Algorithm.Neighbours))
            {
                // NoOp
            }

            int occupiedSeats = seats.Count(seat => seat.Value == SeatState.Occupied);
            Console.WriteLine(occupiedSeats);
        }
        public void Part2()
        {
            var seats = GetSeats(input.GetLines());
            while (ApplyRound(seats, out seats, Algorithm.Vision))
            {
                // NoOp
            }

            int occupiedSeats = seats.Count(seat => seat.Value == SeatState.Occupied);
            Console.WriteLine(occupiedSeats);
        }

        private static bool ApplyRound(Dictionary<(int row, int col), SeatState> seats, out Dictionary<(int, int), SeatState> newSeats, Algorithm algorithm)
        {
            bool changed = false;
            newSeats = new Dictionary<(int, int), SeatState>();
            foreach (var seat in seats)
            {
                var newSeatState = seat.Value;
                if (seat.Value == SeatState.Empty && ShouldBeFilled(seats, algorithm, seat.Key.row, seat.Key.col))
                {
                    newSeatState = SeatState.Occupied;
                    changed = true;
                }

                if (seat.Value == SeatState.Occupied && ShouldBeVacated(seats, algorithm, seat.Key.row, seat.Key.col))
                {
                    newSeatState = SeatState.Empty;
                    changed = true;
                }

                newSeats[seat.Key] = newSeatState;
            }

            return changed;
        }

        private static bool ShouldBeFilled(Dictionary<(int, int), SeatState> seats, Algorithm algorithm, int row, int col)
        {
            return (algorithm == Algorithm.Neighbours && !GetAdjacentSeatStates(seats, row, col).Any(s => s == SeatState.Occupied))
                || (algorithm == Algorithm.Vision && !GetVisibleSeatStates(seats, row, col).Any(s => s == SeatState.Occupied));
        }

        private static bool ShouldBeVacated(Dictionary<(int, int), SeatState> seats, Algorithm algorithm, int row, int col)
        {
            return (algorithm == Algorithm.Neighbours && GetAdjacentSeatStates(seats, row, col).Count(s => s == SeatState.Occupied) >= 4)
                || (algorithm == Algorithm.Vision && GetVisibleSeatStates(seats, row, col).Count(s => s == SeatState.Occupied) >= 5);
        }

        private static IEnumerable<SeatState> GetAdjacentSeatStates(Dictionary<(int, int), SeatState> seats, int row, int col)
        {
            for (int rowOffset = -1; rowOffset <= 1; rowOffset++)
            {
                for (int colOffset = -1; colOffset <= 1; colOffset++)
                {
                    if (!(rowOffset == 0 && colOffset == 0))
                    {
                        if (seats.ContainsKey((row + rowOffset, col + colOffset)))
                        {
                            yield return seats[(row + rowOffset, col + colOffset)];
                        }
                    }
                }
            }
        }

        private static IEnumerable<SeatState> GetVisibleSeatStates(Dictionary<(int, int), SeatState> seats, int row, int col)
        {
            for (int rowDirection = -1; rowDirection <= 1; rowDirection++)
            {
                for (int colDirection = -1; colDirection <= 1; colDirection++)
                {
                    if (!(rowDirection == 0 && colDirection == 0))
                    {
                        int stepSize = 1;
                        var nextSeatKey = (row + (stepSize * rowDirection), col + (stepSize * colDirection));
                        while (seats.ContainsKey(nextSeatKey) && seats[nextSeatKey] == SeatState.Floor)
                        {
                            stepSize++;
                            nextSeatKey = (row + (stepSize * rowDirection), col + (stepSize * colDirection));
                        }

                        if (seats.ContainsKey(nextSeatKey))
                        {
                            yield return seats[nextSeatKey];
                        }
                    }
                }
            }
        }

        private static Dictionary<(int, int), SeatState> GetSeats(IEnumerable<string> lines)
        {
            var seats = new Dictionary<(int, int), SeatState>();
            for (int row = 0; row < lines.Count(); row++)
            {
                for (int col = 0; col < lines.ElementAt(0).Length; col++)
                {
                    seats[(row, col)] = GetSeatState(lines.ElementAt(row).ElementAt(col));
                }
            }

            return seats;
        }

        private static SeatState GetSeatState(char character)
        {
            return character switch
            {
                'L' => SeatState.Empty,
                '.' => SeatState.Floor,
                '#' => SeatState.Occupied,
                _ => throw new ApplicationException($"Invalid character {character}"),
            };
        }

        private enum SeatState
        {
            Floor,
            Empty,
            Occupied
        }

        private enum Algorithm
        {
            Neighbours,
            Vision
        }
    }
}
