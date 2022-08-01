using System;
using System.Linq;
using System.Collections.Generic;

/*
Sudoku Background
Sudoku is a game played on a 9x9 grid. The goal of the game is to fill all cells of thegrid
with digits from 1 to 9, so that each column, each row, and each of the nine 3x3 sub-grids
(also known as blocks) contain all of the digits from 1 to 9

Sudoku Solution Validator
Write a function valid Solution/Validate Solution/valid_solution() that accepts a 2D array
representing a Sudoku board, and returns true if it is a valid solution, or false otherwise.
The cells of the sudoku board may also contain 0's, which will represent empty cells.
Boards containing one or more zeroes are considered to be invalid solutions.
The board is always 9 cells by 9 cells, and every cell only contains integers from 0 to 9
*/

namespace z14
{
    class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine(Sudoku.ValidateSolution(
                new int[][]{
                    new int[] {5, 3, 4, 6, 7, 8, 9, 1, 2}, 
                    new int[] {6, 7, 2, 1, 9, 5, 3, 4, 8},
                    new int[] {1, 9, 8, 3, 4, 2, 5, 6, 7},
                    new int[] {8, 5, 9, 7, 6, 1, 4, 2, 3},
                    new int[] {4, 2, 6, 8, 5, 3, 7, 9, 1},
                    new int[] {7, 1, 3, 9, 2, 4, 8, 5, 6},
                    new int[] {9, 6, 1, 5, 3, 7, 2, 8, 4},
                    new int[] {2, 8, 7, 4, 1, 9, 6, 3, 5},
                    new int[] {3, 4, 5, 2, 8, 6, 1, 7, 9},
                }));
        }
    }

    class Sudoku
    {
        private static HashSet<int> _exceptedNumbers = Enumerable.Range(1, 9).ToHashSet();

        private static bool CheckAllLinesOfValidation(int[][] board) => board.Where(x => !x.ToHashSet().SetEquals(_exceptedNumbers)).Count() == 0;

        private static bool CheckAllColumnsOfValidation(int[][] board)
        {
            for (int row = 0; row < board.Length; ++row)
            {
                HashSet<int> numbersOfColumn = new HashSet<int>();
                
                for (int column = 0; column < board.Length; ++column)
                    numbersOfColumn.Add(board[column][row]);

                if (!numbersOfColumn.SetEquals(_exceptedNumbers)) return false;
            }
            return true;
        }

        private static bool CheckAllCellsOfValidation(int[][] board)
        {
            for (int index = 0; index < board.Length; index += 3)
            {
                for (int rowIndent = 0; rowIndent < board.Length; rowIndent += 3)
                {
                    HashSet<int> numbersOfCell = new HashSet<int>();

                    for (int row = rowIndent; row < rowIndent + 3; ++row)
                        numbersOfCell.UnionWith(board[row][index..(index + 3)]);
                    if (!numbersOfCell.SetEquals(_exceptedNumbers)) return false;
                }
            }
            return true;
        }

        public static bool ValidateSolution(int[][] board)
        {
            if (!CheckAllLinesOfValidation(board)) return false;
            if (!CheckAllColumnsOfValidation(board)) return false;
            return CheckAllCellsOfValidation(board);
        }
    }
}