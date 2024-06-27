using Sudoku.Core.Utils;

namespace Sudoku.Core;

public class SudokuPuzzleValidator {

    public bool Validate(SudokuPuzzle puzzle, bool treatUnsetAsValid = true) {
        for (ushort row = 0; row < 9; row++)
        for (ushort col = 0; col < 9; col++) {
            if (!ValidateCell(puzzle, row, col, treatUnsetAsValid))
                return false;
        }

        return true;
    }

    private static bool ValidateCell(SudokuPuzzle puzzle, ushort row, ushort col, bool treatUnsetAsValid) {
        if (puzzle.IsUnset(row, col))
            return treatUnsetAsValid;

        return ValidateCellForBox(puzzle, row, col) && ValidateCellForRowAndCol(puzzle, row, col);
    }

    private static bool ValidateCellForBox(SudokuPuzzle puzzle, ushort row, ushort col) {
        ushort num = puzzle.Get(row, col);
        
        (ushort boxRow, ushort boxCol) = SudokuPuzzleUtils.GetBoxStartRowAndCol(row, col);
        
        // Check box
        for (ushort r = 0; r < 3; r++)
        for (ushort c = 0; c < 3; c++) {
            if (boxRow + r == row && boxCol + c == col)
                continue;

            if (puzzle.Get((ushort) (boxRow + r), (ushort) (boxCol + c)) == num)
                return false;
        }

        return true;
    }

    private static bool ValidateCellForRowAndCol(SudokuPuzzle puzzle, ushort row, ushort col) {
        ushort num = puzzle.Get(row, col);
        
        for (ushort i = 0; i < 9; i++) {
            if (i != col && puzzle.Get(row, i) == num)
                return false;

            if (i != row && puzzle.Get(i, col) == num)
                return false;
        }

        return true;
    }
    
}
