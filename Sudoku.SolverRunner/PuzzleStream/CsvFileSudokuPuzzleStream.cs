using Sudoku.Core;

namespace SudokuSolver;

public class CsvFileSudokuPuzzleStream : IDisposable, ISudokuPuzzleStream {

    private StreamReader _streamReader;
    private string _delim;
    
    public CsvFileSudokuPuzzleStream(string filePath, string delim, bool skipHeader) {
        _streamReader = new StreamReader(new FileStream(File.OpenHandle(filePath), FileAccess.Read));
        _delim = delim;

        if (skipHeader)
            _streamReader.ReadLine();
    }

    public bool TryGetNext(out SudokuPuzzle puzzle, out SudokuPuzzle solution) {
        string? line = _streamReader.ReadLine();
        if (line == null) {
            puzzle = default;
            solution = default;
            return false;
        }

        string[] comps = line.Split(_delim);
        (string puzzleString, string solutionString) = (comps[0], comps[1]);

        puzzle = SudokuPuzzleParser.Parse(puzzleString);
        solution = SudokuPuzzleParser.Parse(solutionString);
        return true;
    }

    ~CsvFileSudokuPuzzleStream() {
        Dispose(false);
    }

    private void ReleaseUnmanagedResources() { }

    private void Dispose(bool disposing) {
        ReleaseUnmanagedResources();
        if (disposing) {
            _streamReader.Dispose();
        }
    }

    public void Dispose() {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    
}
