using ICities;

namespace LinesDataFetcher
{
    public class LinesDataFetcher : IUserMod
    {
        public string Name
        {
            get
            {
                return "Transport Lines Fetcher";
            }
        }

        public string Description
        {
            get
            {
                return "Fetch transport lines data";
            }
        }
    }
}
