package pl.banq.brokerapi.controller;

import pl.banq.brokerapi.dto.StockDataDto;
import pl.banq.brokerapi.model.StockData;

import java.util.List;
import java.util.stream.Collectors;

public class StockDataDtoMapper {

    private StockDataDtoMapper(){

    }

    public static List<StockDataDto> mapToStockDataDtos(List<StockData> stockData) {
        return stockData.stream()
                .map(StockDataDtoMapper::mapToStockDataDto)
                .collect(Collectors.toList());
    }

    private static StockDataDto mapToStockDataDto(StockData sdata) {
        int lastIndex = sdata.getStock().getStockPrices().size()-1;
        int quantity = sdata.getQuantity();
        double value = sdata.getStock().getStockPrices().get(lastIndex).getPrice();
        return StockDataDto.builder()
                .id(sdata.getDataID())
                .name(sdata.getStock().getStockName())
                .quantity(quantity)
                .value(value)
                .total(Math.round(value * quantity * 100.0)/100.0)
                .build();

    }

}
