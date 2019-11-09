package ioweyou;

import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;

import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.SQLException;

@Configuration
public class AppConfiguration {
    @Bean
    public Connection getConnection() throws SQLException {
        return DriverManager.getConnection("jdbc:sqlite:master.db");
    }
}
