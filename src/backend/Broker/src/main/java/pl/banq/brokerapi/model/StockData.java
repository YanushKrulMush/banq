package pl.banq.brokerapi.model;


import lombok.Getter;
import lombok.RequiredArgsConstructor;
import lombok.Setter;

import pl.banq.brokerapi.dto.StockOfferDto;

import javax.persistence.*;


@Getter
@Setter
@RequiredArgsConstructor
@Entity
public class StockData {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private long dataID;
    private int quantity;

    @ManyToOne
    @JoinColumn(name = "stockID")
    private Stock stock;

    public static StockData from(StockOfferDto stockDataDto) {
        StockData stockData = new StockData();
        stockData.setQuantity(stockDataDto.getQuantity());

        return stockData;
    }
}
