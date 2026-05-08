namespace GomokuNewVersion
{
    internal class BoardRenderer
    {
        private const string EmptyCell = "□";
        private const string BlackCell = "●";
        private const string WhiteCell = "○";
        private const string MarkerCell = "＋ ";
        private const string InvalidCell = "×";
        private const string BlankCell = "　";

        public void Render(Board board, int cursorX, int cursorY, Stone turn, bool showCursor = true) // 보드 렌더러
        {
            Console.CursorVisible = false; 

            // 보드 상단의 열 위치 십자 표시
            for (int j = 0; j < Board.Size; j++)
            {
                Console.Write(showCursor && j == cursorY ? MarkerCell : BlankCell); 
            }
            Console.WriteLine();

            // 보드 전체 칸 순회하여 보드판 출력
            for (int i = 0; i < Board.Size; i++)
            {
                for (int j = 0; j < Board.Size; j++)
                {
                    Console.Write(GetCellSymbol(board, i, j, cursorX, cursorY, turn, showCursor));
                }

                Console.Write(showCursor && i == cursorX ? MarkerCell : BlankCell); // 행 위치 표시
                Console.WriteLine();
            }
        }

        private string GetCellSymbol(Board board, int row, int col, int cursorX, int cursorY, Stone turn, bool showCursor) // 해당 위치의 문자 출력
        {
            if (showCursor && row == cursorX && col == cursorY)
            {
                return board.IsEmpty(cursorX, cursorY) ? GetStoneSymbol(turn) : InvalidCell;
            }

            return GetStoneSymbol(board.GetStone(row, col));
        }

        private string GetStoneSymbol(Stone stone) // 돌 상태에 맞는 출력 문자
        {
            switch(stone)
            {
                case Stone.Black:
                    return BlackCell;
                case Stone.White:
                    return WhiteCell;
                default :
                    return EmptyCell;
            }
        }
    }
}
