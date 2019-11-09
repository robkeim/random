package ioweyou.controllers;

import ioweyou.CreateUserRequest;
import ioweyou.ListUsersResponse;
import ioweyou.services.UserService;
import lombok.AllArgsConstructor;
import org.springframework.web.bind.annotation.*;

@AllArgsConstructor
@RestController
public class UserController {
    private final UserService userService;

    @RequestMapping(value = "/users", method = RequestMethod.GET)
    public ListUsersResponse listUsers(@RequestParam(value = "user") String[] users) {
        return userService.listUsers(users);
    }

    @RequestMapping(value = "/add", method = RequestMethod.POST)
    public void createUser(@RequestBody CreateUserRequest request) {
        userService.addUser(request.getUser());
    }
}
