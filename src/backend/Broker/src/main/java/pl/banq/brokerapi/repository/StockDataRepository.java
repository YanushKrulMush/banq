package pl.banq.brokerapi.repository;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import pl.banq.brokerapi.model.Stock;
import pl.banq.brokerapi.model.StockData;

import java.util.List;

public interface StockDataRepository extends JpaRepository<StockData, Long> {

    @Query("select s from StockData s")
    List<StockData> findAllStockData();


}
