using Automaton.DataAccesLayer.Context;
using Automaton.Domain.Models;
using Automaton.Domain.Repositories;

namespace Automaton.DataAccesLayer.Repositories
{
    public class SummaryRepository : ISummaryRepository
    {
        private readonly IAutomatonDbContext _automatonDbContext;

        public SummaryRepository(
            IAutomatonDbContext automatonDbContext)
        {
            _automatonDbContext = automatonDbContext;
        }

        public Summary GetSummary()
        {
            return _automatonDbContext.Summaries.SingleOrDefault();
        }
    }
}
