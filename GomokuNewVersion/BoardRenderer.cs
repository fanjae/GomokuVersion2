namespace GomokuNewVersion
{
    internal class BoardRenderer
    {
        private const string BlackCell = "●"; // 1칸 유지(칸 늘리면 어긋남)
        private const string WhiteCell = "○"; // 1칸 유지(칸 늘리면 어긋남)
        private const string MarkerCell = "＋ ";
        private const string InvalidCell = "×"; // 1칸 유지(칸 늘리면 어긋남)

        public void Render(Board board, int cursorX, int cursorY, Stone turn, bool showCursor = true) // 보드 렌더러
        {
            Console.CursorVisible = false;
            RenderColumnLabels(cursorY, showCursor);

            // 보드 전체 칸 순회하여 보드판 출력
            for (int row = 0; row < Board.Size; row++)
            {
                RenderRowLabel(row, cursorX, showCursor);

                for (int col = 0; col < Board.Size; col++)
                {
                    Console.Write(GetCellSymbol(board, row, col, cursorX, cursorY, turn, showCursor));
                }

                Console.WriteLine();
            }
        }

        private void RenderColumnLabels(int cursorY, bool showCursor)
        {
            Console.Write("   ");

            for (int col = 0; col < Board.Size; col++)
            {
                if (showCursor && col == cursorY)
                    Console.Write(MarkerCell);
                else
                    Console.Write($"{(char)('A' + col)} ");
            }

            Console.WriteLine();
        }

        private void RenderRowLabel(int row, int cursorX, bool showCursor)
        {
            if (showCursor && row == cursorX)
                Console.Write("＋ ");
            else
                Console.Write($"{row + 1,2} ");
        }

        private string GetCellSymbol(Board board, int row, int col, int cursorX, int cursorY, Stone turn, bool showCursor) // 해당 위치의 문자 출력
        {
            if (showCursor && row == cursorX && col == cursorY)
            {
                return board.IsEmpty(row, col) ? GetStoneSymbol(turn) : InvalidCell; // 유효하지 않은 칸 반환
            }

            Stone stone = board.GetStone(row, col); // 돌 정보 획득

            if (stone != Stone.Empty)  
            {
                return GetStoneSymbol(stone);
            }

            return GetEmptyBoardCellSymbol(board, row, col);
        }
        private string GetEmptyBoardCellSymbol(Board board, int row, int col) // 빈 칸일때 어떤 보드 선 문자를 출력할지 결정
        {
            bool top = row == 0;
            bool bottom = (row == Board.Size - 1);
            bool left = col == 0;
            bool right = (col == Board.Size - 1);

            bool rightHasStone = (col + 1 < Board.Size) && (board.GetStone(row, col + 1) != Stone.Empty); // 돌이 있는 경우, - 하나 제거할 것.

            string horizontal = rightHasStone ? " " : "─";

            if (top && left) return "┌" + horizontal;  
            if (top && right) return "┐ ";
            if (bottom && left) return "└" + horizontal;
            if (bottom && right) return "┘ ";
            if (top) return "┬" + horizontal; 
            if (bottom) return "┴" + horizontal;
            if (left) return "├" + horizontal;
            if (right) return "┤ ";

            return "┼"  + horizontal;
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
                    throw new ArgumentOutOfRangeException(nameof(stone), stone, null); // 예외 처리 (들어오면 안되는 값)
            }
        }
    }
}
