using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using myFirstAPI.DTOs.Stock;
using myFirstAPI.Models;

namespace myFirstAPI.Mappers
{
    public static class StockMapper
    {
       public static StockDto ToStockDto(this Stock stockModel){
        return new StockDto{
            Id=stockModel.Id,
            Symbol=stockModel.Symbol,
            CompanyName=stockModel.CompanyName,
            LastDiv=stockModel.LastDiv,
            Purchase = stockModel.Purchase,
            Industry=stockModel.Industry,
            MarketCap=stockModel.MarketCap,
        };
       }

       public static Stock ToStockFromCreateDTO(this CreateStockRequestDto stockDto){
            return new Stock{
                Symbol=stockDto.Symbol,
                CompanyName=stockDto.CompanyName,
                LastDiv=stockDto.LastDiv,
                Purchase=stockDto.Purchase,
                Industry=stockDto.Industry,
                MarketCap=stockDto.MarketCap
        };
       }
    }
}