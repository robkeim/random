package ioweyou.controllers;

import ioweyou.IOweYouRequest;
import ioweyou.services.IOweYouService;
import lombok.AllArgsConstructor;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.RestController;

@AllArgsConstructor
@RestController
public class IOweYouController {
    private final IOweYouService iOweYouService;

    @RequestMapping(value = "/iou", method = RequestMethod.POST)
    public void createIOweYou(@RequestBody IOweYouRequest request) {
        iOweYouService.CreateIOweYou(request);
    }
}
