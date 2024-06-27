using Sudoku.Core;

namespace SudokuSolver;

public interface ISudokuPuzzleStream {
    
    /// <summary>
    /// Returns a sudoku puzzle state and the corresponding solution.
    /// The solution should have no unset cells and be consistend with the puzzle.
    ///
    /// If this method returns true, <param name="puzzle"></param> and <param name="solution"></param> have been
    /// successfully read from the stream.
    /// If this method returns false, <param name="puzzle"></param> and <param name="solution"></param> are in an
    /// undefined state and the stream is no longer to be considered open.
    /// </summary>
    bool TryGetNext(out SudokuPuzzle puzzle, out SudokuPuzzle solution);
    
}
