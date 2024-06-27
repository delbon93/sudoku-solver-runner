namespace Sudoku.Core.Utils;

public static class SudokuPuzzleUtils {

    public const ushort BitmaskUnset = 0b_000_000_000;
    public const ushort Bitmask1 = 0b_000_000_001;
    public const ushort Bitmask2 = 0b_000_000_010;
    public const ushort Bitmask3 = 0b_000_000_100;
    public const ushort Bitmask4 = 0b_000_001_000;
    public const ushort Bitmask5 = 0b_000_010_000;
    public const ushort Bitmask6 = 0b_000_100_000;
    public const ushort Bitmask7 = 0b_001_000_000;
    public const ushort Bitmask8 = 0b_010_000_000;
    public const ushort Bitmask9 = 0b_100_000_000;

    public static readonly ushort[] BitmasksZeroIndexed = [
        Bitmask1, Bitmask2, Bitmask3, Bitmask4, Bitmask5, Bitmask6, Bitmask7, Bitmask8, Bitmask9
    ];
    
    public static readonly ushort[] BitmasksOneIndexed = [
        BitmaskUnset, Bitmask1, Bitmask2, Bitmask3, Bitmask4, Bitmask5, Bitmask6, Bitmask7, Bitmask8, Bitmask9
    ];


    public static ushort Bitmask(ushort num) {
        // Avoid assumtion that UnsetNum == 0, therefore explicit check
        return num == SudokuPuzzle.UnsetNum ? BitmaskUnset : BitmasksOneIndexed[num];
    }

    public static (ushort, ushort) GetBoxStartRowAndCol(ushort row, ushort col) {
        // return ((ushort, ushort)) (row % 3, col % 3);
        return ((ushort, ushort)) ((row / 3) * 3, (col / 3) * 3);
    }

    public static ushort GetPotentialCellValues(this SudokuPuzzle puzzle, ushort index) {
        ushort row = (ushort) (index / 9);
        ushort col = (ushort) (index % 9);
        return puzzle.GetPotentialCellValues(row, col);
    }
    
    public static ushort GetPotentialCellValues(this SudokuPuzzle puzzle, ushort row, ushort col) {
        if (puzzle.IsSet(row, col))
            return 0;
        
        ushort valids = 0b_111_111_111;

        (ushort boxRow, ushort boxCol) = GetBoxStartRowAndCol(row, col);
        
        // Check box
        for (ushort r = 0; r < 3; r++)
        for (ushort c = 0; c < 3; c++) {
            Invalid(boxRow + r, boxCol + c);
        }
        
        // Check row & col
        for (ushort i = 0; i < 9; i++) {
            Invalid(row, i);
            Invalid(i, col);
        }

        return valids;


        void Invalid(int _row, int _col) {
            ushort num = puzzle.Get((ushort) _row, (ushort) _col);
            valids = (ushort) (valids & ~Bitmask(num));
        }
    }

    public static int CountValidOptionsFromBits(ushort bits) {
        int result = 0;
        for (int i = 0; i < 9; i++) {
            result += bits & 0x01;
            bits >>= 1;
        }
        return result;
    }

    public static ushort[] GetValidOptionsFromBits(ushort bits) {
        ushort[] result = new ushort[CountValidOptionsFromBits(bits)];
        int r = 0;
        ushort nextNum = 1;
        for (int i = 0; i < 9; i++) {
            if ((bits & 0x01) != 0) {
                result[r++] = nextNum;
            }

            bits >>= 1;
            nextNum++;
        }

        return result;
    }
    
}
