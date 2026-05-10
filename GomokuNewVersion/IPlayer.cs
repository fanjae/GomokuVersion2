namespace GomokuNewVersion
{
    internal interface IPlayer
    {
        Stone Stone { get; }
        Position SelectMove(Board board);
    }
}
