using KingLemurJulian.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KingLemurJulian.Core
{
    public interface IQuoteRepository
    {
        Task<Quote> GetQuoteAsync(int id);
        Task<List<Quote>> GetQuotesAsync();
        Task<int> SaveAsync(Quote quote);
    }
}
