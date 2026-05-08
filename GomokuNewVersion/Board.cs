namespace GomokuNewVersion
{
    internal class Board
    {
        public const int Size = 15;

        private readonly Stone[,] cells = new Stone[Size, Size];

        public bool IsInside(int x, int y) // 범위 안에 있는지 체크
        {
            return x >= 0 && x < Size && y >= 0 && y < Size;
        }

        public bool IsEmpty(int x, int y) // 빈칸 체크
        {
            return cells[x, y] == Stone.Empty;
        }

        public Stone GetStone(int x, int y) // 돌 정보 얻어오기
        {
            return cells[x, y];
        }

        public void PlaceStone(int x, int y, Stone stone) // 돌 놓기
        {
            cells[x, y] = stone;
        }
    }
}
