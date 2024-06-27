using Sudoku.Core;

namespace SudokuSolver;

public class SudokuPuzzleStream : IDisposable {

    private StreamReader _streamReader;
    private string _delim;
    
    public void ReadFromCsv(string filePath, string delim, bool skipHeader) {
        _streamReader = new StreamReader(new FileStream(File.OpenHandle(filePath), FileAccess.Read));
        _delim = delim;

        if (skipHeader)
            _streamReader.ReadLine();
    }

    public bool TryNext(out SudokuPuzzle puzzle, out SudokuPuzzle solution) {
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

    ~SudokuPuzzleStream() {
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
