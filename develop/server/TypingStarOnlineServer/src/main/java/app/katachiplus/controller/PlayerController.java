package app.katachiplus.controller;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.security.authentication.AuthenticationManager;
import org.springframework.security.authentication.UsernamePasswordAuthenticationToken;
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
import app.katachiplus.domain.model.player.PlayerResponse;
import app.katachiplus.domain.service.JwtTokenService;
import app.katachiplus.domain.service.PlayerService;

@RestController
@RequestMapping("/player")
public class PlayerController {

	@Autowired
	private PlayerService playerService;

	@Autowired
	private JwtTokenService jwtTokenService;

	@Autowired
	private AuthenticationManager authenticationManager;

	@Autowired
	private PasswordEncoder passwordEncoder;

	@PostMapping("/signup")
	public boolean signup(@RequestBody SignupForm form) {
		return playerService.signup(
				form.getPlayerId(),
				passwordEncoder.encode(form.getPassword()));
	}

	@PostMapping("login")
	public PlayerResponse login(@RequestBody LoginForm form) {
		var authentication = authenticationManager.authenticate(
				new UsernamePasswordAuthenticationToken(
						form.getPlayerId(),
						form.getPassword()));
		
		if (authentication.isAuthenticated() == false)
			return null;
		
		return new PlayerResponse(
				(Player) authentication.getPrincipal(), 
				jwtTokenService.generateToken(authentication));
	}

	@GetMapping("logout")
	public boolean logout(@RequestParam String playerId) {
		return playerService.logout(playerId);
	}
}
