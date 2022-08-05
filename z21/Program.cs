using System;
using System.Linq;
using System.Collections.Generic;

namespace Solution
{
    public class BattleshipField
    {
        private static int _countBattleships = 0,
                            _countCruisers = 0,
                            _countDestroyers = 0,
                            _countSubmarines = 0;

        private static int m(int cell, int count, bool isSubmarine = false)
        {
            if (cell == 1) return ++count;
            switch (count)
            {
                case 1: if (isSubmarine) ++_countSubmarines; break;
                case 2: ++_countDestroyers; break;
                case 3: ++_countCruisers; break;
                case 4: ++_countBattleships; break;
            }
            return 0;
        }

        public static bool ValidateBattlefield(int[,] field)
        {
            for (int row = 0; row < field.GetLength(0); ++row)
            {
                int countPerRow = 0, countPerColumn = 0;

                for (int column = 0; column < field.GetLength(0); ++column)
                {
                    if (countPerRow > 4 || countPerColumn > 4) return false;

                    int cellPerRow = field[row, column], cellPerColumn = field[column, row];
                    bool isSubmarine = countPerRow == 1 
                                    && field[(row == 0) ? 0 : row - 1, column - 1] == 0 
                                    && field[row + 1, column - 1] == 0;

                    countPerRow = m(cellPerRow, countPerRow, isSubmarine);
                    countPerColumn = m(cellPerColumn, countPerColumn);
                }
            }

            if (_countBattleships != 1 || _countCruisers != 2 || _countDestroyers != 3 || _countSubmarines != 4) return false;

            return true;
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine(
                BattleshipField.ValidateBattlefield(new int[10,10]{
                    {1, 0, 0, 0, 0, 1, 1, 0, 0, 0},
                    {1, 0, 1, 0, 0, 0, 0, 0, 1, 0},
                    {1, 0, 1, 0, 1, 1, 1, 0, 1, 0},
                    {1, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0, 0, 1, 0},
                    {0, 0, 0, 0, 1, 1, 1, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0, 0, 1, 0},
                    {0, 0, 0, 1, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0, 1, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0, 0, 0, 0}
                })
            );
        }
    }
}