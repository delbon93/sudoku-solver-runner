using CommandLine;
using Sudoku.Core;

namespace SudokuSolver;

internal static class Program {

    private static void Main(string[] args) {
        Parser.Default.ParseArguments<CommandLineOptions>(args)
            .WithParsed(MainWithParsedOptions)
            .WithNotParsed(MainWithUnparsedOptions);
    }

    private static void MainWithUnparsedOptions(IEnumerable<Error> errors) {
        Console.Error.WriteLine("Errors in command line arguments:");

        foreach (Error error in errors) {
            Console.Error.WriteLine($"  - {error.Tag.ToString()}");
        }
    }

    private static void MainWithParsedOptions(CommandLineOptions options) {
        ISudokuPuzzleStream puzzleStream = new CsvFileSudokuPuzzleStream(options.InputLocation, delim: ",", skipHeader: true);

        ISudokuSolver solver = new BruteForceSudokuSolver();
        SudokuSolverRunner solverRunner = new SudokuSolverRunner();

        Console.WriteLine($"Running with solver '{solver.SolverName}':");
        SudokuSolverRunner.SolverRunResult totalResult = new();
        for (int i = 0; i < 2500; i++) {
            if (puzzleStream.TryGetNext(out SudokuPuzzle puzzle, out SudokuPuzzle solution)) {
                Console.Write($"  \u2554 solving {puzzle.ToCompactString('x')}: ");

                SudokuSolverRunner.SolverRunResult result = solverRunner.RunSingleAndValidate(
                    solver, puzzle, solution, out SudokuPuzzle solutionFromSolver);
                
                

                string status = "";
                if (result.SuccessfulRuns == 1)
                    status = "correct solution";
                if (result.IncorrectRuns == 1)
                    status = "incorrect solution";
                if (result.UnsolvedRuns == 1)
                    status = "could not solve";
                
                Console.WriteLine($"{status} [{result.TotalRuntimeMicros}µs]");
                Console.WriteLine($"  \u2560\u2550\u2550>  correct solution: {solution.ToCompactString()}");
                Console.WriteLine($"  \u255a\u2550\u2550> solver's solution: {solutionFromSolver.ToCompactString()}");
                

                totalResult += result;
            }
            else {
                Console.Error.WriteLine($"Stopped at i={i}, could not get next puzzle from stream");
                break;
            }
        }
        
        Console.WriteLine("\n\nTotal:");
        Console.WriteLine($"          Time: {totalResult.TotalRuntimeMillis}ms");
        Console.WriteLine($"          Runs: {totalResult.TotalRuns}");
        Console.WriteLine($"  Success Rate: {totalResult.SuccessRate * 100f:F2}%");
        Console.WriteLine($"    Solve Rate: {totalResult.SolveRate * 100f:F2}%");
        Console.WriteLine($"    Error Rate: {totalResult.ErrorRate * 100f:F2}%");
    }
    
}
