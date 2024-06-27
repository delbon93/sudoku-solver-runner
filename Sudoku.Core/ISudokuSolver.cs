namespace Sudoku.Core;

public interface ISudokuSolver {

    /// <summary>
    /// Solve a sudoku puzzle. If the solver fails to solve the sudoku for some reason, a <seealso cref="FailedToSolveException"/>
    /// is thrown.
    /// </summary>
    /// <param name="puzzle">The sudoku puzzle state to solve</param>
    /// <exception cref="FailedToSolveException"></exception>
    /// <returns>The solution state of the sudoku puzzle</returns>
    public SudokuPuzzle Solve(SudokuPuzzle puzzle);

}
