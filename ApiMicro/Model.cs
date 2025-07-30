namespace ApiMicro
{
    public class Game
    {
        public int Id { get; set; }
        public int Size { get; set; }
        public char[][] Board { get; set; }
        public char CurrentPlayer { get; set; }
        public string Status { get; set; }
        public int MoveCount { get; set; }

        public class MoveRequest
        {
            public int GameId { get; set; }
            public int X { get; set; }
            public int Y { get; set; }
        }

        public void CheckWinner()
        {
            for (int i = 0; i < Size; i++)
            {
                // Горизоньаль
                if (Board[i][0] != ' ' && Board[i].All(c => c == Board[i][0]))
                {
                    Status = Board[i][0] == 'X' ? "XWon" : "OWon";
                    return;
                }

                // Вертикаль
                if (Board[0][i] != ' ' && Board.All(row => row[i] == Board[0][i]))
                {
                    Status = Board[0][i] == 'X' ? "XWon" : "OWon";
                    return;
                }
            }

            // Диагональ слева
            if (Board[0][0] != ' ' && Board[0][0] == Board[1][1] && Board[0][0] == Board[2][2])
            {
                Status = Board[0][0] == 'X' ? "XWon" : "OWon";
                return;
            }

            // Диагональ справа
            if (Board[0][2] != ' ' && Board[0][2] == Board[1][1] && Board[0][2] == Board[2][2])
            {
                Status = Board[0][2] == 'X' ? "XWon" : "OWon";
                return;
            }

            // Ничя
            if (MoveCount == Size * Size && Status == "InProgress")
            {
                Status = "Draw";
            }
        }
    }


}
