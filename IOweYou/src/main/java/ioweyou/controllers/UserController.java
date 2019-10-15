package ioweyou.controllers;

import ioweyou.ListUsersResponse;
import ioweyou.services.UserService;
import lombok.AllArgsConstructor;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import java.util.Arrays;

@AllArgsConstructor
@RestController
public class UserController {
    private final UserService userService;

    @RequestMapping("/users")
    public ListUsersResponse listUsers() {
        return userService.listUsers();
    }
}
