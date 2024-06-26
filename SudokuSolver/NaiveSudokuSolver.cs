using Microsoft.VisualBasic.CompilerServices;

namespace SudokuSolver;

public class NaiveSudokuSolver : ISudokuSolver {

    private readonly SudokuPuzzleValidator _validator = new();

    private int IndexOfMin(int[] ints) {
        int min = Int32.MaxValue;
        int minIndex = -1;
        for (int i = 0; i < ints.Length; i++) {
            if (ints[i] == 0) continue;
            
            if (ints[i] >= min) continue;
            
            min = ints[i];
            minIndex = i;
        }

        return minIndex;
    }
    
    public SudokuPuzzle Solve(SudokuPuzzle puzzle) {
        ushort[] valids = new ushort[81];
        int[] counts = new int[81];

        while (!puzzle.Solved) {
            for (ushort i = 0; i < 81; i++) {
                valids[i] = puzzle.GetPotentialCellValues(i);
                counts[i] = SudokuPuzzleUtils.CountValidOptionsFromBits(valids[i]);
            }

            int minIndex = IndexOfMin(counts);

            if (minIndex == -1)
                throw new FailedToSolveException(puzzle.Clone());
            
            ushort row = (ushort) (minIndex / 9);
            ushort col = (ushort) (minIndex % 9);

            ushort[] options = SudokuPuzzleUtils.GetValidOptionsFromBits(valids[minIndex]);

            if (options.Length == 1) {
                puzzle.Set(row, col, options[0]);

                if (!_validator.Validate(puzzle))
                    throw new FailedToSolveException(puzzle.Clone());
                
                continue;
            }
            
            bool foundValidOption = false;
            for (int i = 0; i < options.Length && !foundValidOption; i++) {
                SudokuPuzzle optionPuzzle = puzzle.Clone();
                optionPuzzle.Set(row, col, options[i]);

                try {
                    SudokuPuzzle solvedOption = Solve(optionPuzzle);

                    if (_validator.Validate(solvedOption)) {
                        puzzle = solvedOption;
                        foundValidOption = true;
                    }
                }
                catch (FailedToSolveException) {
                    
                }
            }

            if (!foundValidOption)
                throw new FailedToSolveException(puzzle.Clone());
        }

        return puzzle;
    }
    
}
