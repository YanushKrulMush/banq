package pl.banq.brokerapi.model;

import lombok.Getter;
import lombok.RequiredArgsConstructor;
import lombok.Setter;

import javax.persistence.*;
import java.util.List;
import java.util.Set;

@Getter
@Setter
@RequiredArgsConstructor
@Entity
public class Stock {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private long stockID;
    private String stockName;

    @OneToMany
    @JoinColumn(name = "stockID")
    private List<StockPrice> stockPrices;

    @OneToMany
    @JoinColumn(name = "stockID")
    private Set<StockData> stockDataSet;

}

