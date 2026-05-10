namespace GomokuNewVersion
{
    internal class MoveEvaluator
    {
        private readonly GomokuRule rule = new GomokuRule();

        // pos에 돌을 놓았음을 가정하여 가치를 점수로 평가.
        public int Evaluate(Board board, Position pos, Stone stone) 
        {
            int totalScore = 0;

            
            board.SetStone(pos, stone);

            try
            {
                if (rule.IsWin(board, pos, stone))
                    return 100000;

                // 4방향 검사
                for (int i = 0; i < 4; i++)
                {
                    int dRow = GomokuDirections.GetRow(i);
                    int dCol = GomokuDirections.GetCol(i);

                    // 정방향, 역방향으로 이어진 돌 개수 계산
                    int forward = rule.CountStone(board, pos, dRow, dCol, stone);
                    int backward = rule.CountStone(board, pos, -dRow, -dCol, stone);

                    // 현재 위치의 돌 1개를 포함한 총 연결 개수.
                    int count = 1 + forward + backward;

                    // 양끝 중 비어있는 끝의 개수 
                    int openEnds = rule.CountOpenEnds(board, pos, dRow, dCol, forward, backward);


                    totalScore += GetPatternScore(count, openEnds);
                }
            }
            finally
            {
                // 평가용 임시 배치돌은 반드시 제거한다.
                board.SetStone(pos, Stone.Empty);
            }

            return totalScore;
        }

        // 참고 논문에서 사용한 패턴 기반 가중치 값을 참고
        private int GetPatternScore(int count, int openEnds) // 돌 개수와 열린 끝 개수를 기준으로 점수 반환
        {
            if (count >= 5) return 100000;

            if (count == 4) 
                return openEnds >= 1 ? 5000 : 0;

            if (count == 3)
                return openEnds == 2 ? 600 : openEnds == 1 ? 57 : 0;

            if (count == 2)
                return openEnds == 2 ? 55 : openEnds == 1 ? 35 : 30;

            if (count == 1)
                return openEnds == 2 ? 13 : openEnds == 1 ? 7 : 5;

            return 0;
        }

    }
}
