namespace SudokuSolver;

public class NaiveSudokuSolver : ISudokuSolver {

    private readonly SudokuPuzzleValidator _validator = new();

    public SudokuPuzzle Solve(SudokuPuzzle puzzle) {
        // Holds bitmasks for valid options per cell given the current puzzle state
        ushort[] valids = new ushort[81];
        // Holds the number of valid options per cell given the current puzzle state. Calculated from the 'valids' array
        int[] counts = new int[81];

        // Every iteration attempts to find the cell with the least options given the current puzzle state. For each of
        // these options an alternative what-if-state is created where that option is the correct value for that cell.
        // A solution is then recursively attempted for each what-if-state until either a solution is found or an
        // invalid puzzle state is reached.
        while (!puzzle.Solved) {
            // Gather all valid option (as bitmask) and how many there are for each cell
            for (ushort i = 0; i < 81; i++) {
                valids[i] = puzzle.GetPotentialCellValues(i);
                counts[i] = SudokuPuzzleUtils.CountValidOptionsFromBits(valids[i]);
            }

            // Determine which one has the least options, ignoring cells with no options (i.e. 0)
            int minIndex = CollectionUtils.IndexOfMinValueIgnoreZero(counts);

            // If none was found we have a puzzle state without any options -> can't be solved from here
            if (minIndex == -1)
                throw new FailedToSolveException(puzzle);
            

            // Convert the valid options bitmask to an array of actual options
            ushort[] options = SudokuPuzzleUtils.GetValidOptionsFromBits(valids[minIndex]);
            
            ushort row = (ushort) (minIndex / 9);
            ushort col = (ushort) (minIndex % 9);

            // If we have only one option we can simply set it in its cell and move on to the next iteration
            if (options.Length == 1) {
                puzzle.Set(row, col, options[0]);

                // However, if validation fails at this point (puzzle state is invalid) the puzzle cannot be solved.
                // This can happen if the given puzzle was unsolvable in the first place, or if we are currently in
                // an invalid what-if recursion, which then ends at this point. 
                if (!_validator.Validate(puzzle))
                    throw new FailedToSolveException(puzzle);
                
                continue;
            }
            
            
            bool foundValidOption = false;
            // Create a what-if-state for each option and recursively try to solve each one
            for (int i = 0; i < options.Length && !foundValidOption; i++) {
                SudokuPuzzle whatIfPuzzleState = puzzle.Clone();
                whatIfPuzzleState.Set(row, col, options[i]);

                // If the recursion throws a FailedToSolveException at some point this was not a valid option to begin
                // with. We then move on to the next option
                try {
                    SudokuPuzzle solvedWhatIfState = Solve(whatIfPuzzleState);

                    // If the recursion was successful and the state validates we can take the result of the recursion
                    // as our new puzzle state. We do not test any further what-if-states from here
                    if (_validator.Validate(solvedWhatIfState)) {
                        puzzle = solvedWhatIfState;
                        
                        // This also breaks the loop
                        foundValidOption = true;
                    }
                }
                catch (FailedToSolveException) {
                    // The what-if-state failed to solve. Move on to the next one.
                    continue;
                }
            }

            // If ALL what-if-states for this cell failed to solve then the entire current puzzle state can not be
            // solved. This is most likely the result of a wrongful what-if-state of an earlier recursion, or that
            // of an unsolvable puzzle to begin with.
            // Either way, we cannot solve the puzzle from here.
            if (!foundValidOption)
                throw new FailedToSolveException(puzzle);
        }
        
        return puzzle;
    }
    
}
