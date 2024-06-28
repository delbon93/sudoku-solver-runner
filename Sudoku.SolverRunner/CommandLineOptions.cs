using CommandLine;

namespace SudokuSolver;

public class CommandLineOptions {
    
    [Option('i', "input", Required = false, HelpText = "Input location for sudoku puzzles")]
    public string InputLocation { get; set; }
    
    [Option('f', "format", Required = false, HelpText = "Input format, determines how input location is interpreted",
        Default = "csv")]
    public string InputFormat { get; set; }
    
    [Option('s', "solver", Required = true, HelpText = "Path to assembly containing solver implementations")]
    public string SolverAssembly { get; set; }
    
}
