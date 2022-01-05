package pl.banq.brokerapi.data;

import java.io.IOException;
import java.io.PrintWriter;
import java.time.LocalDateTime;

public class DataGenerator {


    private final String path;

    public DataGenerator(String path) {
        this.path = path;
    }

    public void generate() {
        try {
            PrintWriter fileWriter = new PrintWriter(path);

            fileWriter.append("insert into stock(stock_name) values ('Orange Polska SA');\n");
            fileWriter.append("insert into stock(stock_name) values ('Allegro.eu SA');\n");
            fileWriter.append("insert into stock(stock_name) values ('Grupa Lotos SA');\n");

            for (int i = 1; i <= 100; i++) {
                String txt = "insert into stock_price(price, stockid) values (" + (int) Math.ceil(Math.random() * 10000) + "," + (i % 3+1) + ");\n";
                fileWriter.append(txt);
            }


            for (int i = 1; i <= 9; i++) {
                String txt = "insert into stock_data(quantity, stockid) values (" + (int) Math.round(Math.random() * 100) + "," + (i % 3+1) + ");\n";
                fileWriter.append(txt);
            }


            fileWriter.close();
        } catch (IOException e) {
            e.printStackTrace();
        }


    }
}
