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


## Attributions

This project uses the [Command Line Parser Library for CLR and NetStandard](https://github.com/commandlineparser/commandline)
for parsing the runner's command line arguments.
