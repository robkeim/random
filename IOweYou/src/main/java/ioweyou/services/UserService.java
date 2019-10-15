package ioweyou.services;

import ioweyou.ListUsersResponse;
import ioweyou.repositories.UserRepository;
import lombok.AllArgsConstructor;
import lombok.var;
import org.springframework.stereotype.Service;

import java.util.Arrays;
import java.util.stream.Collectors;

@AllArgsConstructor
@Service
public class UserService {
    private final UserRepository userRepository;

    public ListUsersResponse listUsers() {
        var users = userRepository
                .listUsers()
                .stream()
                .sorted()
                .collect(Collectors.toList());

        return ListUsersResponse
                .builder()
                .users(users)
                .build();
    }

    public void addUser(String name) {
        userRepository.addUser(name);
    }
}
