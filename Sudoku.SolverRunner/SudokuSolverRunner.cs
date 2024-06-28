using System.Diagnostics;
using Sudoku.Core;

namespace SudokuSolver;

public class SudokuSolverRunner {

    private readonly Stopwatch _stopwatch = new();
    
    public SolverRunResult RunSingleAndValidate(ISudokuSolver solver, SudokuPuzzle puzzle, SudokuPuzzle solution, out SudokuPuzzle solutionFromSolver) {
        SolverRunResult result = new() {
            TotalRuns = 1,
        };

        try {
            _stopwatch.Restart();
            solutionFromSolver = solver.Solve(puzzle.Clone());
            _stopwatch.Stop();

            bool success = solutionFromSolver.Equals(solution);

            result.TotalRuntimeMicros += _stopwatch.Elapsed.Microseconds;

            if (success)
                result.SuccessfulRuns++;
            else
                result.IncorrectRuns++;
        }
        catch (FailedToSolveException e) {
            result.UnsolvedRuns++;
        }

        return result;
    }

    public struct SolverRunResult {


        public int TotalRuns { get; set; } = 0;
        
        /// <summary>
        /// Number of runs that ended with a correct solution
        /// </summary>
        public int SuccessfulRuns { get; set; } = 0;
        
        /// <summary>
        /// Number of runs that ended without a solution
        /// </summary>
        public int UnsolvedRuns { get; set; } = 0;

        /// <summary>
        /// Number of runs that ended with an incorrect solution
        /// </summary>
        public int IncorrectRuns { get; set; } = 0;

        public float SuccessRate => TotalRuns != 0 ? (float) SuccessfulRuns / TotalRuns : 0.0f;
        public float ErrorRate => TotalRuns != 0 ? (float) IncorrectRuns / TotalRuns : 0.0f;
        public float SolveRate => TotalRuns != 0 ? 1.0f - (float) UnsolvedRuns / TotalRuns : 0.0f;

        public long TotalRuntimeMicros { get; set; } = 0;
        public long TotalRuntimeMillis => TotalRuntimeMicros / 1000L;
        public float AverageRuntimeMillis => TotalRuns != 0 ? (float) TotalRuntimeMillis / TotalRuns : 0.0f;
        public float AverageRuntimeMicros => TotalRuns != 0 ? (float) TotalRuntimeMicros / TotalRuns : 0.0f;
        
        public SolverRunResult() { }

        public static SolverRunResult operator+(SolverRunResult r1, SolverRunResult r2) {
            SolverRunResult s = new() {
                TotalRuns = r1.TotalRuns + r2.TotalRuns,
                SuccessfulRuns = r1.SuccessfulRuns + r2.SuccessfulRuns,
                TotalRuntimeMicros = r1.TotalRuntimeMicros + r2.TotalRuntimeMicros,
                UnsolvedRuns = r1.UnsolvedRuns + r2.UnsolvedRuns,
            };
            
            return s;
        }
        
    }
    
}
