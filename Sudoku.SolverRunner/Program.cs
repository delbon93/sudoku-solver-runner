using Sudoku.Core;

namespace SudokuSolver;

internal static class Program {
    
    private static void Main(string[] args) {
        if (args.Length == 0) {
            Console.Error.WriteLine("No input csv file given! Aborting.");
            return;
        }
        
        string csvFile = args[0];

        SudokuPuzzleStream puzzleStream = new SudokuPuzzleStream();
        puzzleStream.ReadFromCsv(csvFile, delim: ",", skipHeader: true);

        ISudokuSolver solver = new NaiveSudokuSolver();
        SudokuSolverRunner solverRunner = new SudokuSolverRunner();

        Console.WriteLine($"Running with solver '{solver.GetType().Name}':");
        SudokuSolverRunner.SolverRunResult totalResult = new();
        for (int i = 0; i < 2500; i++) {
            if (puzzleStream.TryNext(out SudokuPuzzle puzzle, out SudokuPuzzle solution)) {
                Console.Write($"  • solving {puzzle.ToCompactString('x')}: ");

                SudokuSolverRunner.SolverRunResult result = solverRunner.RunSingleAndValidate(solver, puzzle, solution);

                string status = "";
                if (result.SuccessfulRuns == 1)
                    status = "correct solution";
                if (result.IncorrectRuns == 1)
                    status = "incorrect solution";
                if (result.UnsolvedRuns == 1)
                    status = "could not solve";
                
                Console.WriteLine($"{status} [{result.TotalRuntimeMillis}ms]");

                totalResult += result;
            }
            else {
                Console.Error.WriteLine($"Stopped at i={i}, could not get next puzzle from stream");
                break;
            }
        }
        
        Console.WriteLine("\n\nTotal:");
        Console.WriteLine($"          Runs: {totalResult.TotalRuns}");
        Console.WriteLine($"  Success Rate: {totalResult.SuccessRate * 100f:F2}%");
        Console.WriteLine($"    Solve Rate: {totalResult.SolveRate * 100f:F2}%");
        Console.WriteLine($"    Error Rate: {totalResult.ErrorRate * 100f:F2}%");
    }
    
}
