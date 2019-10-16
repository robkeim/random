package ioweyou.repositories;

import ioweyou.IOweYou;
import org.springframework.stereotype.Repository;

import java.util.ArrayList;
import java.util.List;
import java.util.stream.Collectors;

@Repository
public class IOweYouRepository {
    private final List<IOweYou> iOweYous = new ArrayList<>();

    public void addIOweYou(IOweYou iOweYou) {
        iOweYous.add(iOweYou);
    }

    public List<IOweYou> getForLender(String name) {
        return iOweYous
                .stream()
                .filter(iou -> iou.getLender().equals(name))
                .collect(Collectors.toUnmodifiableList());
    }

    public List<IOweYou> getForBorrower(String name) {
        return iOweYous
                .stream()
                .filter(iou -> iou.getBorrower().equals(name))
                .collect(Collectors.toUnmodifiableList());
    }
}
