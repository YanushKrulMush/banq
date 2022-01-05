package pl.banq.brokerapi.model;

import lombok.Getter;
import lombok.RequiredArgsConstructor;
import lombok.Setter;

import javax.persistence.*;
import java.math.BigDecimal;

@Getter
@Setter
@RequiredArgsConstructor
@Entity
public class StockPrice {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private long stockPriceID;
    private int price;

}
