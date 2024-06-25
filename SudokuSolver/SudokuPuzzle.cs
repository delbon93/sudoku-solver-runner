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

    public bool Solved => _nums.All(n => n != UnsetNum);

    private static ushort Index(ushort row, ushort col) {
        return (ushort) (col + 9 * row);
    }

    public override string ToString() {
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
}
