package ioweyou.services;

import ioweyou.BadRequestException;
import ioweyou.IOweYou;
import ioweyou.IOweYouRequest;
import ioweyou.repositories.IOweYouRepository;
import ioweyou.repositories.UserRepository;
import lombok.AllArgsConstructor;
import org.springframework.stereotype.Service;

@AllArgsConstructor
@Service
public class IOweYouService {
    private final IOweYouRepository iOweYouRepository;
    private final UserRepository userRepository;

    public void createIOweYou(IOweYouRequest request) {
        if (!userRepository.exists(request.getLender())) {
            throw new BadRequestException("Lender does not exist");
        }

        if (!userRepository.exists(request.getBorrower())) {
            throw new BadRequestException("Borrower does not exist");
        }

        if (request.getAmount() <= 0) {
            throw new BadRequestException("Amount must be strictly positive");
        }

        var iOweYou = IOweYou
                .builder()
                .lender(request.getLender())
                .borrower(request.getBorrower())
                .amount(request.getAmount())
                .build();

        iOweYouRepository.addIOweYou(iOweYou);
    }
}
