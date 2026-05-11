namespace GomokuNewVersion
{
    internal class AIPlayer : IPlayer
    {
        private readonly GomokuRule rule = new GomokuRule();
        private readonly MoveEvaluator evaluator = new MoveEvaluator();
        public Stone Stone { get; }

        public AIPlayer(Stone stone)
        {
            Stone = stone;
        }

        public Position SelectMove(Board board) // AI가 둘 위치 선택
        {
            Position bestMove = new Position(Board.Size / 2, Board.Size / 2); // 빈 보드에서 중앙 선택
            int bestScore = int.MinValue;
            int totalScore = 0;
            int score = 0;
            int opponentScore = 0;

            // 상대 돌
            Stone opponent = GetOpponent(Stone);

            // 모든 위치 순회하며 후보 검사
            for (int row = 0; row < Board.Size; row++)
            {
                for (int col = 0; col < Board.Size; col++)
                {
                    Position pos = new Position(row, col);

                    // 돌 놓인 곳 제외
                    if (!board.IsEmpty(pos))
                        continue;

                    // 33 금수 위치 후보 에서 제외
                    if (rule.IsDoubleThree(board, pos, Stone))
                        continue;

                    // AI의 공격(평가) 점수
                    score = evaluator.Evaluate(board, pos, Stone);

                    // 상대의 공격(평가) 점수 -> AI 입장에서 방어 해야할 점수
                    opponentScore = evaluator.Evaluate(board, pos, opponent);
                    totalScore = score + opponentScore;


                    // 가장 높은 점수를 가진 위치에 AI가 둔다. (합산 점수가 높다는 것은 나에게도 상대에게도 전략적으로 중요한 자리)
                    if (totalScore > bestScore)
                    {
                        bestScore = totalScore;
                        bestMove = pos;
                    }
                }
            }

            // 최종적인 위치가 AI가 둘 위치
            return bestMove;
        }
        private Stone GetOpponent(Stone stone)
        {
            return stone == Stone.Black ? Stone.White : Stone.Black;
        }
    }
}
