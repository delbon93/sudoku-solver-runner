namespace Sudoku.Core.Utils;

public static class CollectionUtils {
    
    public static int IndexOfMinValueIgnoreZero(IEnumerable<int> collection) {
        int min = int.MaxValue;
        int minIndex = -1;
        
        int[] items = collection.ToArray();
        
        for (int i = 0; i < items.Length; i++) {
            if (items[i] == 0) continue;
            
            if (items[i] >= min) continue;
            
            min = items[i];
            minIndex = i;
        }

        return minIndex;
    }
    
}
