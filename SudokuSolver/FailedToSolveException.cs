namespace SudokuSolver;

public class FailedToSolveException(SudokuPuzzle stateAtFailure, string reason = "") : Exception {
    
    public SudokuPuzzle StateAtFailure { get; private set; } = stateAtFailure.Clone();
    public string Reason { get; private set; } = reason;
}
