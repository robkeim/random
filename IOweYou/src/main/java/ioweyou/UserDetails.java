package ioweyou;

import lombok.Builder;
import lombok.Value;

import java.util.HashMap;

@Builder
@Value
public class UserDetails {
    private String name;

    private HashMap<String, Double> owes;

    private HashMap<String, Double> owedBy;

    private double balance;
}
