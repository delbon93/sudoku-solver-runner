using Sudoku.Core;

namespace SudokuSolver;

public static class SudokuPuzzleParser {

    public static SudokuPuzzle Parse(string puzzleString) {
        ushort[] nums = new ushort[81];
        for (int i = 0; i < 81; i++) {
            nums[i] = ParseNum(puzzleString[i]);
        }

        return new SudokuPuzzle(nums);
    }

    private static ushort ParseNum(char c) {
        if (c is '1' or '2' or '3' or '4' or '5' or '6' or '7' or '8' or '9')
            return ushort.Parse($"{c}");

        return SudokuPuzzle.UnsetNum;
    }
    
}
