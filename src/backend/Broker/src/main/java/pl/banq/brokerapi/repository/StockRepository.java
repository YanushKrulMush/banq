package pl.banq.brokerapi.repository;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;
import pl.banq.brokerapi.model.Stock;

public interface StockRepository extends JpaRepository<Stock, Long> {

    @Query(value = "SELECT s FROM Stock s WHERE s.stockID =: id")
    Stock findStockByStockID(@Param("id") long id);

}
