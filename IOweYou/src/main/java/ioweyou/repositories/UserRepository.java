package ioweyou.repositories;

import lombok.AllArgsConstructor;
import org.springframework.stereotype.Repository;

import java.sql.Connection;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Statement;

@AllArgsConstructor
@Repository
public class UserRepository {
    private static final String insert = "INSERT INTO Users values('%s')";
    private static final String query = "SELECT COUNT(*) FROM Users WHERE Name = '%s'";

    private final Connection connection;

    public void addUser(String name) {
        try {
            Statement statement = connection.createStatement();
            statement.setQueryTimeout(1);
            statement.executeUpdate(String.format(insert, name));
        } catch (SQLException e) {
            throw new RuntimeException(e);
        }
    }

    public boolean exists(String name) {
        try {
            Statement statement = connection.createStatement();
            statement.setQueryTimeout(1);
            ResultSet rs = statement.executeQuery(String.format(query, name));

            return rs.getInt(1) != 0;
        } catch (SQLException e) {
            throw new RuntimeException(e);
        }
    }
}
