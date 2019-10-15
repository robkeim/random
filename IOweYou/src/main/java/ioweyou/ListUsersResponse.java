package ioweyou;

import lombok.Builder;
import lombok.Value;

import java.util.List;

@Builder
@Value
public class ListUsersResponse {
    List<String> users;
}
