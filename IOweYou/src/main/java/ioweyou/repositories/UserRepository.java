package ioweyou.repositories;

import org.springframework.stereotype.Repository;

import java.util.Arrays;
import java.util.List;

@Repository
public class UserRepository {
    public List<String> listUsers() {
        return Arrays.asList("foo", "bar4");
    }
}
