using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using myFirstAPI.Models;

namespace myFirstAPI.Interface
{
    public interface IStockRepository
    {
        Task<List<Stock>> GetAllAsync();
    }
}