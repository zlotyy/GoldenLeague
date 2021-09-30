namespace GoldenLeague.Database
{
    public interface IDbContextFactory
    {
        GoldenLeagueDB Create();
    }

    public class DbContextFactory : IDbContextFactory
    {
        public GoldenLeagueDB Create()
        {
            return new GoldenLeagueDB();
        }
    }
}
