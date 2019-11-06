package ioweyou;

import lombok.Builder;
import lombok.Value;

@Builder
@Value
public class IOweYou {
    private String lender;

    private String borrower;

    private double amount;
}
