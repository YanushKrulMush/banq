package pl.banq.brokerapi;

import org.hibernate.Session;
import org.hibernate.SessionFactory;
import org.hibernate.Transaction;
import org.hibernate.cfg.Configuration;
import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import pl.banq.brokerapi.data.DataGenerator;
import pl.banq.brokerapi.model.Stock;

import java.util.LinkedList;
import java.util.List;

@SpringBootApplication
public class BrokerapiApplication {

	private static SessionFactory sessionFactory = null;


	public static void main(String[] args) {
		(new DataGenerator(System.getProperty("user.dir") + "/src/main/resources/data.sql")).generate();
		SpringApplication.run(BrokerapiApplication.class, args);
	}

	private static SessionFactory getSessionFactory() {
		if (sessionFactory == null) {
			Configuration configuration = new Configuration();
			sessionFactory =
					configuration.configure().buildSessionFactory();
		}
		return sessionFactory;
	}

}
