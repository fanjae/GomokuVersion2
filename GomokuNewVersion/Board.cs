namespace GomokuNewVersion
{
    internal class Board
    {
        public const int Size = 15;

        private readonly Stone[,] cells = new Stone[Size, Size];

        public bool IsInside(Position pos) // 범위 안에 있는지 체크
        {
            return pos.Row >= 0 && pos.Row < Size && pos.Col >= 0 && pos.Col < Size;
        }

        public bool IsEmpty(Position pos) // 빈칸 체크
        {
            return cells[pos.Row, pos.Col] == Stone.Empty;
        }

        public Stone GetStone(Position pos) // 돌 정보 얻어오기
        {
            return cells[pos.Row, pos.Col];
        }

        public bool TryPlaceStone(Position pos, Stone stone) // 실제 돌 놓기 
        {
            if (!IsInside(pos)) return false;

            if (!IsEmpty(pos)) return false;

            cells[pos.Row, pos.Col] = stone;
            return true;
        }


        public void SetStone(Position pos, Stone stone) // 룰 체크, 시뮬레이션 등등에 활용할 용도의 돌 놓기
        {
            cells[pos.Row, pos.Col] = stone;
        }
    }
}
