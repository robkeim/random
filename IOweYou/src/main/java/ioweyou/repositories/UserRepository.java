package ioweyou.repositories;

import org.springframework.stereotype.Repository;

import java.util.ArrayList;
import java.util.HashSet;
import java.util.List;

@Repository
public class UserRepository {
    private HashSet<String> users = new HashSet<>();

    public List<String> listUsers() {
        return new ArrayList<>(users);
    }

    public void addUser(String name) {
        users.add(name);
    }
}
