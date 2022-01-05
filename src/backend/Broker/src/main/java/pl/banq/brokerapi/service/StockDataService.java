package pl.banq.brokerapi.service;

import lombok.RequiredArgsConstructor;
import org.springframework.stereotype.Service;
import pl.banq.brokerapi.dto.StockOfferDto;
import pl.banq.brokerapi.repository.StockDataRepository;
import pl.banq.brokerapi.model.StockData;
import pl.banq.brokerapi.repository.StockRepository;

import java.util.List;

@Service
@RequiredArgsConstructor
public class StockDataService {

    private final StockDataRepository stockDataRepository;
    private final StockRepository stockRepository;

    public List<StockData> getStockData(){
        return stockDataRepository.findAllStockData();
    }

    public void buyStockData(StockOfferDto stockOfferDto) {

        StockData stockData = new StockData();
        stockData.setQuantity(stockOfferDto.getQuantity());

        long stockID = stockOfferDto.getStockID();

        stockRepository.findById(stockID).map(s -> {
            stockData.setStock(s);
            return stockDataRepository.save(stockData);
        }).orElseThrow();
    }

    public void sellStockData(StockOfferDto stockOfferDto) {
        StockData stockData = stockDataRepository.getById(stockOfferDto.getStockDataID());
        int quantityBeforeTransaction = stockData.getQuantity();
        int quantityToSell = stockOfferDto.getQuantity();

        System.out.println(stockData.getDataID());

        if(quantityBeforeTransaction < quantityToSell){
            throw new IllegalArgumentException();
        } else if (quantityBeforeTransaction == quantityToSell) {
            stockDataRepository.deleteById(stockData.getDataID());
        } else {
            stockData.setQuantity(quantityBeforeTransaction - quantityToSell);
            stockDataRepository.save(stockData);
        }
    }
}
