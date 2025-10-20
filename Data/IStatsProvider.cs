using System.Threading.Tasks;

namespace PremierInsight.Data
{
    public interface IStatsProvider
    {
        string SourceName { get; }

        Task<bool> TestConnectionAsync();
    }
}