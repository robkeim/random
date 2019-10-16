package ioweyou.services;

import ioweyou.IOweYou;
import ioweyou.IOweYouRequest;
import ioweyou.repositories.IOweYouRepository;
import lombok.AllArgsConstructor;
import org.springframework.stereotype.Service;

@AllArgsConstructor
@Service
public class IOweYouService {
    private final IOweYouRepository iOweYouRepository;

    public void CreateIOweYou(IOweYouRequest request) {
        IOweYou iOweYou = IOweYou
                .builder()
                .lender(request.getLender())
                .borrower(request.getBorrower())
                .amount(request.getAmount())
                .build();

        iOweYouRepository.addIOweYou(iOweYou);
    }
}
