namespace GomokuNewVersion
{
    internal class GomokuRule
    {
        private readonly int[,] directions =
        {
            { 0, 1 },
            { 1, 0 },
            { 1, 1 },
            { 1, -1 }
        };

        public bool IsWin(Board board, Position pos, Stone stone) // 승리 체크
        {
            for (int i = 0; i < 4; i++)
            {
                int dRow = directions[i, 0];
                int dCol = directions[i, 1];

                int count = 1; // 본인 돌을 전제로 계산.
                count += CountStone(board, pos, dRow, dCol, stone);
                count += CountStone(board, pos, -dRow, -dCol, stone);

                if (count >= 5)
                    return true;
            }

            return false;
        }

        public bool IsDoubleThree(Board board, Position pos, Stone stone) // 3,3 체크
        {
            // 이미 돌이 놓인 위치라면 3,3이 아님
            if(!board.IsEmpty(pos))
            {
                return false;
            }

            // 돌을 놓은 것을 전제하여 검사
            board.SetStone(pos, stone);

            try
            {
                int openThreeCnt = 0;

                // 가로, 세로, 대각선 2개 방향 검사(4방향 탐색)  
                for (int i = 0; i < 4; i++)
                {
                    int dRow = directions[i, 0];
                    int dCol = directions[i, 1];

                    // 현재 방향에서 열린 3이 만들어졌다면 개수 증가
                    if (IsOpenThree(board, pos, dRow, dCol, stone))
                        openThreeCnt++;
                }
                return openThreeCnt >= 2;
            }
            finally
            {
                board.SetStone(pos, Stone.Empty);
            }
        }
        private bool IsOpenThree(Board board, Position pos, int dRow, int dCol, Stone stone) // 열린 3,3 체크
        {
            // 현재 위치 기준 정방향 역방향 돌 개수 계산
            int forwardCnt = CountStone(board, pos, dRow, dCol, stone);
            int backwardCnt = CountStone(board, pos, -dRow, -dCol, stone);

            int totalCnt = 1 + forwardCnt + backwardCnt;

            // 열린 3은 정확히 돌 3개가 전제
            if (totalCnt != 3)
                return false;

            // 정방향 돌 좌표 계산
            Position forwardEnd = new Position(pos.Row + dRow * (forwardCnt + 1), pos.Col + dCol * (forwardCnt + 1));

            // 역방향 돌 좌표 계산
            Position backwardEnd = new Position(pos.Row - dRow * (backwardCnt + 1), pos.Col - dCol * (backwardCnt + 1));

            // 정방향 끝이 보드 안에 있고 비어 있는가
            bool forwardOpen = board.IsInside(forwardEnd) && board.GetStone(forwardEnd) == Stone.Empty;

            // 역방향 끝이 보드 안에 있고 비어 있는가
            bool backwardOpen = board.IsInside(backwardEnd) && board.GetStone(backwardEnd) == Stone.Empty;

            // 양쪽 끝이 모두 열려 있다면 열린 3이 된다.
            return forwardOpen && backwardOpen;
        }


        private int CountStone(Board board, Position pos, int dRow, int dCol, Stone stone) // 돌 개수 세기
        {
            int cnt = 0;

            Position next = new Position(pos.Row + dRow, pos.Col + dCol);

            while (board.IsInside(next) && board.GetStone(next) == stone)
            {
                cnt++;
                next = new Position(next.Row + dRow, next.Col + dCol);
            }

            return cnt;
        }
    }
}
