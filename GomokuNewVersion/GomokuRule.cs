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

        public bool IsWin(Board board, int x, int y, Stone stone) // 승리 체크
        {
            for (int i = 0; i < 4; i++)
            {
                int dx = directions[i, 0];
                int dy = directions[i, 1];

                int count = 1; // 본인 돌을 전제로 계산.
                count += CountStone(board, x, y, dx, dy, stone);
                count += CountStone(board, x, y, -dx, -dy, stone);

                if (count >= 5)
                    return true;
            }

            return false;
        }

        public bool IsDoubleThree(Board board, int x, int y, Stone stone) // 3,3 체크
        {
            // 이미 돌이 놓인 위치라면 3,3이 아님
            if(!board.IsEmpty(x,y))
            {
                return false;
            }

            // 돌을 놓은 것을 전제하여 검사
            board.PlaceStone(x, y, stone);

            int openThreeCnt = 0;

            // 가로, 세로, 대각선 2개 방향 검사(4방향 탐색)            
            for (int i = 0; i < 4; i++)
            {
                int dx = directions[i, 0];
                int dy = directions[i, 1];

                // 현재 방향에서 열린 3이 만들어졌다면 개수 증가
                if (IsOpenThree(board, x, y, dx, dy, stone))
                    openThreeCnt++;
            }

            // 검사 후 임시로 놓았던 돌 제거
            board.PlaceStone(x, y, Stone.Empty);

            return openThreeCnt >= 2;
        }
        private bool IsOpenThree(Board board, int x, int y, int dx, int dy, Stone stone) // 열린 3,3 체크
        {
            // 현재 위치 기준 정방향 역방향 돌 개수 계산
            int forwardCnt = CountStone(board, x, y, dx, dy, stone);
            int backwardCnt = CountStone(board, x, y, -dx, -dy, stone);

            int totalCnt = 1 + forwardCnt + backwardCnt;

            // 열린 3은 정확히 돌 3개가 전제
            if (totalCnt != 3)
                return false;

            // 정방향 돌 좌표 계산
            int forwardEndX = x + dx * (forwardCnt + 1);
            int forwardEndY = y + dy * (forwardCnt + 1);

            // 역방향 돌 좌표 계산
            int backwardEndX = x - dx * (backwardCnt + 1);
            int backwardEndY = y - dy * (backwardCnt + 1);

            // 정방향 끝이 보드 안에 있고 비어 있는가
            bool forwardOpen =
                board.IsInside(forwardEndX, forwardEndY) &&
                board.GetStone(forwardEndX, forwardEndY) == Stone.Empty;

            // 역방향 끝이 보드 안에 있고 비어 있는가
            bool backwardOpen =
                board.IsInside(backwardEndX, backwardEndY) &&
                board.GetStone(backwardEndX, backwardEndY) == Stone.Empty;

            // 양쪽 끝이 모두 열려 있다면 열린 3이 된다.
            return forwardOpen && backwardOpen;
        }


        private int CountStone(Board board, int x, int y, int dx, int dy, Stone stone) // 돌 개수 세기
        {
            int cnt = 0;

            int nextX = x + dx;
            int nextY = y + dy;

            while (board.IsInside(nextX, nextY) && board.GetStone(nextX, nextY) == stone)
            {
                cnt++;
                nextX += dx;
                nextY += dy;
            }

            return cnt;
        }
    }
}
