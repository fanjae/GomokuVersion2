namespace GomokuNewVersion
{
    internal static class GomokuDirections // 이동 방향값 정보
    {
        private static readonly int[,] Directions = 
        {
            {0 ,1},
            {1, 0},
            {1, 1},
            {1,-1}
        };

        public static int GetRow(int index)
        {
            ValidateIndex(index);
            return Directions[index, 0];
        }

        public static int GetCol(int index)
        {
            ValidateIndex(index);
            return Directions[index, 1];
        }
        private static void ValidateIndex(int index) // 인덱스 넘어간 경우 예외처리
        {
            if (index < 0 || index >= 4)
                throw new ArgumentOutOfRangeException(nameof(index), "방향 인덱스가 범위를 벗어났습니다.");
        }
    }
}
