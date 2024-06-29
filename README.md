# Sudoku Solver-Runner

> **IMPORTANT:** This project is still very early in development! Features are missing, documentation
> is lacking and things will change.

## What is this?

This project aims to provide a library with which to implement programs
able to solve Sudoku puzzles, as well as a console application to run these 
solvers against datasets of Sudoku puzzles.

This repository contains the following three .NET projects:

### • The Sudoku.Core library

This library contains common code for both solvers and the runner. It provides
a data type for Sudoku puzzles, the `ISudokuSolver` interface to be implemented
by solvers and some utilities.

The `SudokuPuzzleValidator` type can be used to validate the state of a sudoku
puzzle.

### • The Solver-Runner

This CLI application can load assemblies (DLL files) you point it at and run all (or some) of
the solvers it finds within them. Any type implementing the `ISudokuSolver` found
within an assembly will be found.

Additionally, a dataset must be provided (e.g. a csv file) with Sudoku puzzles and respective solutions.

### • The BruteForceSolver

This is my own brute force Sudoku solver, provided in the `Sudoku.BruteForceSolver` assembly. It
implements a recursive trial-and-error approach.


### Getting started

* Download the runner from the releases on this github page (currently win64 only).
The runner comes bundled with the `Sudoku.BruteForceSolver` assembly, so you don't have to implement
one yourself to test the runner

* Get a Sudoku dataset. [This one on kaggle.com](https://www.kaggle.com/datasets/bryanpark/sudoku) is already in the required csv format
> Currently, csv files are expected to be in the same format as found in the dataset linked above, i.e.
> including a header line and two values per line, separated by `,` (comma). The first value is the puzzle, the second
> value the solution to that puzzle.
> 
> Both puzzle and solution are a contiguous string of 81 digits from 0 to 9 (both inclusive). The string represents the cells of the
> Sudoku puzzle, row by row, starting from top left and going to bottom right.
> The puzzle contains zeroes (`0`) in places where an empty cell is supposed to be filled in. The solution is expected to not
> contain any zeroes but only digits from 1 to 9.
> 
> This is an example for a puzzle: ```004300209005009001070060043006002087190007400050083000600000105003508690042910300```
> 
> This is an example for a solution to the puzzle above: ```864371259325849761971265843436192587198657432257483916689734125713528694542916378```


* Run the runner from the command line. Currently only csv input is supported.

Example for running the solver in cmd / Windows PowerShell: 

```
.\Sudoku.SolverRunner.exe -i "path\to\data.csv" -s "path\to\solver.dll"
```

> **Note:** currently the path to the solver assembly must be an *absolute path*, otherwise it can't find it. This will be fixed soon™.

## Attributions

This project uses the [Command Line Parser Library for CLR and NetStandard](https://github.com/commandlineparser/commandline)
for parsing the runner's command line arguments.
