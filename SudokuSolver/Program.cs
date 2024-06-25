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

        if (puzzleStream.TryNext(out SudokuPuzzle puzzle, out SudokuPuzzle solution)) {
            Console.WriteLine("PUZZLE");
            Console.WriteLine(puzzle.ToString());
            Console.WriteLine("SOLUTION");
            Console.WriteLine(solution.ToString());
        }
    }
    
}
