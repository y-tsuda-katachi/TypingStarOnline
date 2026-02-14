package app.katachiplus.controller;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.security.crypto.password.PasswordEncoder;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.RestController;

import app.katachiplus.domain.model.form.LoginForm;
import app.katachiplus.domain.model.form.SignupForm;
import app.katachiplus.domain.model.player.Player;
import app.katachiplus.domain.service.PlayerService;

@RestController
@RequestMapping("/player")
public class PlayerController {

	@Autowired
	private PlayerService playerService;
	
	@Autowired
	private PasswordEncoder passwordEncoder;

	@PostMapping("/signup")
	public boolean signup(@RequestBody SignupForm signupForm) {
		return playerService.signup(
				signupForm.getPlayerId(), 
				passwordEncoder.encode(signupForm.getPassword()));
	}
	
	@PostMapping("login")
	public Player login(@RequestBody LoginForm loginForm) {
		return playerService.login(
				loginForm.getPlayerId(), 
				passwordEncoder.encode(loginForm.getPassword()));
	}
	
	@GetMapping("logout")
	public boolean logout(@RequestParam String playerId) {
		return playerService.logout(playerId);
	}
}
