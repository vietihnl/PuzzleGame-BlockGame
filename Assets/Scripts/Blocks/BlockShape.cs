using UnityEngine;

public static class BlockShape
{
    public static Vector2Int[] GetCells(BlockType type)
    {
        switch (type)
        {
            case BlockType.I:
                return new Vector2Int[]
                {
                    new Vector2Int(0, 0),
                    new Vector2Int(1, 0),
                    new Vector2Int(2, 0),
                    new Vector2Int(3, 0),
                };
            case BlockType.I1:
                return new Vector2Int[]
                {
                    new Vector2Int(0,0),
                    new Vector2Int(0,1),
                    new Vector2Int(0,2),
                    new Vector2Int(0,3),
                };
            case BlockType.I2:
                return new Vector2Int[]
                {
                    new Vector2Int(0,0),
                    new Vector2Int(1,0),
                    new Vector2Int(2,0),
                    
                };
            case BlockType.O:
                return new Vector2Int[]
                {
                    new Vector2Int(0, 0),
                    new Vector2Int(1, 0),
                    new Vector2Int(0, 1),
                    new Vector2Int(1, 1),
                };
            case BlockType.O1:
                return new Vector2Int[]
                {
                    new Vector2Int(0, 0),
                    new Vector2Int(1, 0),
                    new Vector2Int(0, 1),
                    new Vector2Int(1, 1),
                    new Vector2Int(2,0),
                    new Vector2Int(2,1),
            
                };
            case BlockType.O2:
                return new Vector2Int[]
                {
                    new Vector2Int(0, 0),
                    new Vector2Int(1, 0),
                    new Vector2Int(0, 1),
                    new Vector2Int(1, 1),
                    new Vector2Int(0,2),
                    new Vector2Int(1,2),
            
                };
            case BlockType.O3:
                return new Vector2Int[]
                {
                    new Vector2Int(0, 0),
                    new Vector2Int(1, 0),
                    new Vector2Int(0, 1),
                    new Vector2Int(1, 1),
                    new Vector2Int(0,2),
                    new Vector2Int(1,2),
                    new Vector2Int(2,2),
                    new Vector2Int(2,0),
                    new Vector2Int(2,1),
                
            
                };
            case BlockType.T:
                return new Vector2Int[]
                {
                    new Vector2Int(1, 1),
                    new Vector2Int(0, 0),
                    new Vector2Int(1, 0),
                    new Vector2Int(2, 0),
                };
            case BlockType.L:
                return new Vector2Int[]
                {
                    new Vector2Int(0, 0),
                    new Vector2Int(0, 1),
                    new Vector2Int(0, 2),
                    new Vector2Int(1, 0),
                };
            case BlockType.L1:
                return new Vector2Int[]
                {
                    new Vector2Int(0, 0),
                    new Vector2Int(0, 1),
                    new Vector2Int(0, 2),
                    new Vector2Int(1, 2),
                };
            case BlockType.L2:
                return new Vector2Int[]
                {
                    new Vector2Int(1, 0),
                    new Vector2Int(1, 1),
                    new Vector2Int(1, 2),
                    new Vector2Int(0, 2),
                };
            case BlockType.L3:
                return new Vector2Int[]
                {
                    new Vector2Int(0, 0),
                    new Vector2Int(1, 0),
                    new Vector2Int(0, 1),
                    
                };
             case BlockType.L4:
                return new Vector2Int[]
                {
                    new Vector2Int(1, 1),
                    new Vector2Int(1, 0),
                    new Vector2Int(0, 1),
                    
                };
            case BlockType.L5:
                return new Vector2Int[]
                {
                    new Vector2Int(0, 0),
                    new Vector2Int(1, 0),
                    new Vector2Int(0, 1),
                    new Vector2Int(2,0),
                    
                };
            case BlockType.L6:
                return new Vector2Int[]
                {
                    new Vector2Int(2, 1),
                    new Vector2Int(1, 0),
                    new Vector2Int(2, 0),
                    new Vector2Int(0,0),
                };
            case BlockType.J:
                return new Vector2Int[]
                {
                    new Vector2Int(1, 0),
                    new Vector2Int(1, 1),
                    new Vector2Int(1, 2),
                    new Vector2Int(0, 0),
                };
            case BlockType.S:
                return new Vector2Int[]
                {
                    new Vector2Int(1, 0),
                    new Vector2Int(2, 0),
                    new Vector2Int(0, 1),
                    new Vector2Int(1, 1),
                };
            case BlockType.Z:
                return new Vector2Int[]
                {
                    new Vector2Int(0, 0),
                    new Vector2Int(1, 0),
                    new Vector2Int(1, 1),
                    new Vector2Int(2, 1),
                };
        }

        return new Vector2Int[] { new Vector2Int(0, 0) };
    }
}