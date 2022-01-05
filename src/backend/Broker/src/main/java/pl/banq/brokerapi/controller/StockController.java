package pl.banq.brokerapi.controller;

import io.swagger.v3.oas.annotations.parameters.RequestBody;
import lombok.RequiredArgsConstructor;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RestController;
import pl.banq.brokerapi.dto.StockDataDto;
import pl.banq.brokerapi.dto.StockOfferDto;
import pl.banq.brokerapi.service.StockDataService;

import java.util.List;

import static pl.banq.brokerapi.controller.StockDataDtoMapper.mapToStockDataDtos;

@RestController
@RequiredArgsConstructor
public class StockController {

    private final StockDataService stockDataService;

    @GetMapping("/stockdata")
    public List<StockDataDto> getStockData() {
        return mapToStockDataDtos(stockDataService.getStockData());
    }

    @PostMapping("/stockdata/buy")
    public void buyStockData(@RequestBody StockOfferDto stockOfferDto){
        stockDataService.buyStockData(stockOfferDto);
    }
    @PostMapping("/stockdata/sell")
    public void sellStockData(@RequestBody StockOfferDto stockOfferDto){
        stockDataService.sellStockData(stockOfferDto);
    }

}
