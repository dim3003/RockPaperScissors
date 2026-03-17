namespace RockPaperScissors;

public class RockPaperScissors
{
    public Outcomes Play(PlayerMoves playerMove, PlayerMoves opponentMove)
    {
        if (playerMove == PlayerMoves.Paper)
        {
            if(opponentMove == PlayerMoves.Scissors)
                return Outcomes.PlayerLoses;
            
            return Outcomes.PlayerWins;
        }
        if (playerMove == PlayerMoves.Scissors)
            return Outcomes.PlayerWins;

        return Outcomes.PlayerLoses;
    }
}
