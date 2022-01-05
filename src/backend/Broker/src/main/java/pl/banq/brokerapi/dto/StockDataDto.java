package pl.banq.brokerapi.dto;

import lombok.Builder;
import lombok.Data;

@Data
@Builder
public class StockDataDto {
    private long id;
    private int quantity;
    private String name;
    private double value;
    private double total;
}
