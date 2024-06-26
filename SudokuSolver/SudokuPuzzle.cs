using System.Text;

namespace SudokuSolver;

public readonly struct SudokuPuzzle {

    public const ushort UnsetNum = 0;

    public SudokuPuzzle(ushort[] nums) {
        _nums = nums;
    }

    private readonly ushort[] _nums = [];

    public ushort Get(ushort row, ushort col) {
        return _nums[Index(row, col)];
    }

    public void Set(ushort row, ushort col, ushort num) {
        _nums[Index(row, col)] = num is < 1 or > 9 ? UnsetNum : num;
    }

    public ushort Get(ushort index) => _nums[index];

    public ushort Set(ushort index, ushort num) => _nums[index] = num;

    public bool IsSet(ushort row, ushort col) {
        return _nums[Index(row, col)] != UnsetNum;
    }

    public bool IsUnset(ushort row, ushort col) => !IsSet(row, col);

    public bool Solved => _nums.All(n => n != UnsetNum);

    private static ushort Index(ushort row, ushort col) {
        return (ushort) (col + 9 * row);
    }

    public override string ToString() => ToCompactString();

    public string ToLongString() {
        StringBuilder sb = new();
        
        sb.AppendLine("+-------+-------+-------+");
        for (ushort row = 0; row < 9; row++) {
            sb.Append("| ");
            for (ushort col = 0; col < 9; col++) {
                ushort num = Get(row, col);

                if (num == UnsetNum)
                    sb.Append("  ");
                else
                    sb.Append($"{num} ");
                    
                if ((col + 1) % 3 == 0)
                    sb.Append("| ");
            }
            
            sb.AppendLine();
            if ((row + 1) % 3 == 0)
                sb.AppendLine("+-------+-------+-------+");
        }

        return sb.ToString();
    }

    public string ToCompactString(char unsetCharacter = '0') {
        StringBuilder sb = new();
        for (int i = 0; i < 81; i++) {
            if (_nums[i] == UnsetNum)
                sb.Append(unsetCharacter);
            else
                sb.Append(_nums[i]);
        }
        return sb.ToString();
    }

    public bool Equals(SudokuPuzzle other) {
        return _nums.SequenceEqual(other._nums);
    }

    public override int GetHashCode() {
        return _nums.GetHashCode();
    }

    public SudokuPuzzle Clone() {
        ushort[] newNums = new ushort[81];
        Array.Copy(_nums, newNums, _nums.Length);
        return new SudokuPuzzle(newNums);
    }
    
}
