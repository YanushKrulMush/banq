package pl.banq.brokerapi.service;

import lombok.RequiredArgsConstructor;
import org.springframework.stereotype.Service;
import pl.banq.brokerapi.model.Stock;
import pl.banq.brokerapi.model.StockData;
import pl.banq.brokerapi.repository.StockDataRepository;
import pl.banq.brokerapi.repository.StockRepository;

@Service
@RequiredArgsConstructor
public class StockService {

    private final StockRepository stockRepository;


    public Stock getSingleStock(long id) {
        return stockRepository.findById(id).orElseThrow();
    }


}
