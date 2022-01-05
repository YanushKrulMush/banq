package pl.banq.brokerapi.dto;

import lombok.Data;

@Data
public class StockOfferDto {
    private long stockDataID;
    private int quantity;
    private long stockID;
}
