package ioweyou;

import lombok.Data;

@Data
public class IOweYouRequest {
    private String lender;

    private String borrower;

    private double amount;
}
