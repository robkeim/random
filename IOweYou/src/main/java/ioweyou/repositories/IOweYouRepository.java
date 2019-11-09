package ioweyou.repositories;

import ioweyou.IOweYou;
import lombok.AllArgsConstructor;
import org.springframework.stereotype.Repository;

import java.sql.Connection;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Statement;
import java.util.ArrayList;
import java.util.List;

@AllArgsConstructor
@Repository
public class IOweYouRepository {
    private static final String insert = "INSERT INTO Transactions values('%s', '%s', %f)";
    private static final String queryForLender = "SELECT * FROM Transactions WHERE Lender = '%s'";
    private static final String queryForBorrower = "SELECT * FROM Transactions WHERE Borrowern = '%s'";

    private final Connection connection;

    public void addIOweYou(IOweYou iOweYou) {
        try {
            Statement statement = connection.createStatement();
            statement.setQueryTimeout(1);
            statement.executeUpdate(String.format(
                    insert,
                    iOweYou.getLender(),
                    iOweYou.getBorrower(),
                    iOweYou.getAmount()
            ));
        } catch (SQLException e) {
            throw new RuntimeException(e);
        }
    }

    public List<IOweYou> getForLender(String name) {
        return executeQuery(queryForLender, name);
    }

    public List<IOweYou> getForBorrower(String name) {
        return executeQuery(queryForBorrower, name);
    }

    private List<IOweYou> executeQuery(String query, String param) {
        try {
            Statement statement = connection.createStatement();
            statement.setQueryTimeout(1);
            ResultSet rs = statement.executeQuery(String.format(query, param));

            List<IOweYou> results = new ArrayList<>();
            while (rs.next()) {
                results.add(IOweYou
                        .builder()
                        .borrower(rs.getString("Borrower"))
                        .lender(rs.getString("Lender"))
                        .amount(rs.getDouble("Amount"))
                        .build()
                );
            }

            return results;
        } catch (SQLException e) {
            throw new RuntimeException(e);
        }
    }
}
