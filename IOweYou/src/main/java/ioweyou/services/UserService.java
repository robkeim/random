package ioweyou.services;

import ioweyou.BadRequestException;
import ioweyou.IOweYou;
import ioweyou.ListUsersResponse;
import ioweyou.UserDetails;
import ioweyou.repositories.IOweYouRepository;
import ioweyou.repositories.UserRepository;
import lombok.AllArgsConstructor;
import org.springframework.stereotype.Service;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.stream.Collectors;

import static java.util.stream.Collectors.groupingBy;

@AllArgsConstructor
@Service
public class UserService {
    private final IOweYouRepository iOweYouRepository;
    private final UserRepository userRepository;

    // TODO rkeim: see if the database can handle this grouping logic
    public ListUsersResponse listUsers() {
        List<String> names = userRepository
                .listUsers()
                .stream()
                .sorted()
                .collect(Collectors.toUnmodifiableList());

        List<UserDetails> userDetails = new ArrayList<>();

        for (String name : names) {
            HashMap<String, Double> owes = new HashMap<>();
            Double owesTotal = 0d;

            Map<String, List<IOweYou>> owesRaw = iOweYouRepository.getForBorrower(name)
                    .stream()
                    .collect(groupingBy(IOweYou::getLender));

            for (String key : owesRaw.keySet()) {
                Double owesToKey = owesRaw.get(key).stream().mapToDouble(IOweYou::getAmount).sum();
                owes.put(key, owesToKey);
                owesTotal += owesToKey;
            }

            HashMap<String, Double> owedBy = new HashMap<>();
            Double owedByTotal = 0d;

            Map<String, List<IOweYou>> owedByRaw = iOweYouRepository.getForLender(name)
                    .stream()
                    .collect(groupingBy(IOweYou::getBorrower));

            for (String key : owedByRaw.keySet()) {
                Double owedByKey = owedByRaw.get(key).stream().mapToDouble(IOweYou::getAmount).sum();
                owedBy.put(key, owedByKey);
                owedByTotal += owedByKey;
            }

            userDetails.add(UserDetails
                    .builder()
                    .name(name)
                    .owes(owes)
                    .owedBy(owedBy)
                    .balance(owedByTotal - owesTotal)
                    .build()
            );
        }

        return ListUsersResponse
                .builder()
                .users(userDetails)
                .build();
    }

    public void addUser(String name) {
        if (userRepository.exists(name)) {
            throw new BadRequestException("User already exists");
        }

        userRepository.addUser(name);
    }
}
